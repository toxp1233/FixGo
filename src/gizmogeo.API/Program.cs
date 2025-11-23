using gizmogeo.API.Middlewares;
using gizmogeo.Application.Extensions;
using gizmogeo.Domain.Entities;
using gizmogeo.Infrastructure.Extensions;
using gizmogeo.Infrastructure.Persistance;
using gizmogeo.Infrastructure.Persistance.Seeders;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

// Configure initial bootstrap logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting web host");

    var builder = WebApplication.CreateBuilder(args);

    // Configure Serilog as the primary logger
    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "gizmogeo.API")
            .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}");
    });

    // Register middleware early
    builder.Services.AddTransient<ErrorHandlingMiddleware>();

    // Add services to the container
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
            Name = "Authorization",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "Bearer",
                    Name = "Bearer",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header
                },
                new List<string>()
            }
        });
    });


    // Add infrastructure and application services
    builder.Services.AddInfrastructure(builder.Configuration)
                   .AddApplication(builder.Configuration);

    // Configure Cloudinary settings
    builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

    // Add Application Insights telemetry
    builder.Services.AddApplicationInsightsTelemetry();

    var app = builder.Build();

    // Log environment information
    Log.Information("Environment: {Environment}", app.Environment.EnvironmentName);
    Log.Information("Content root path: {ContentRoot}", app.Environment.ContentRootPath);

    // Critical change for Azure deployment
    if (!app.Environment.IsDevelopment())
    {
        // In production, ensure we don't block startup on database initialization
        _ = Task.Run(async () =>
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<Program>>();
            var context = services.GetRequiredService<FixGoDbContext>();

            try
            {
                await InitializeDatabaseWithRetry(context, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Database initialization failed in background task");
            }
        });
    }
    else
    {
        // In development, initialize immediately for better debugging
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();
        var context = services.GetRequiredService<FixGoDbContext>();
        await InitializeDatabaseWithRetry(context, logger);
    }

    // Configure the HTTP request pipeline
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseSerilogRequestLogging();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    // Health check endpoint
    app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

    Log.Information("Application starting up...");
    await app.RunAsync();
}
catch (HostAbortedException)
{
    // Expected during normal shutdown
    Log.Information("Host was aborted during startup");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    throw; // Ensure Azure recognizes the failure
}
finally
{
    Log.CloseAndFlush();
}

async Task InitializeDatabaseWithRetry(FixGoDbContext context, ILogger<Program> logger)
{
    const int maxRetries = 5;
    var retryCount = 0;
    var delay = TimeSpan.FromSeconds(5);

    while (retryCount < maxRetries)
    {
        try
        {
            logger.LogInformation("Attempting database connection (attempt {RetryCount})", retryCount + 1);

            // 1. Ensure database exists
            if (!await context.Database.CanConnectAsync())
            {
                logger.LogWarning("Database not found - attempting to create");
                await context.Database.EnsureCreatedAsync();
            }

            // 2. Apply migrations
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                logger.LogInformation("Applying {Count} migrations", pendingMigrations.Count());
                await context.Database.MigrateAsync();
            }

            // 3. Seed data
            logger.LogInformation("Seeding data...");
            await RoleSeeder.SeedAsync(context);
            await UserSeeder.SeedUsers(context);

            logger.LogInformation("Database initialization completed successfully");
            return;
        }
        catch (Exception ex) when (retryCount < maxRetries - 1)
        {
            retryCount++;
            logger.LogWarning(ex, "Database initialization failed (attempt {RetryCount}). Retrying in {DelaySeconds} seconds...",
                retryCount, delay.TotalSeconds);
            await Task.Delay(delay);
            delay = TimeSpan.FromSeconds(delay.TotalSeconds * 2); // Exponential backoff
        }
    }

    throw new Exception($"Failed to initialize database after {maxRetries} attempts");
}