using gizmogeo.API.Middlewares;
using gizmogeo.Application.Extensions;
using gizmogeo.Domain.Entities;
using gizmogeo.Infrastructure.Extensions;
using gizmogeo.Infrastructure.Persistance;
using gizmogeo.Infrastructure.Persistance.Seeders;
using Microsoft.EntityFrameworkCore;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        //c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        //{
        //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        //    Name = "Authorization",
        //    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        //    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        //    Scheme = "Bearer"
        //});

        //c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        //{
        //{
        //    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        //    {
        //        Reference = new Microsoft.OpenApi.Models.OpenApiReference
        //        {
        //            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
        //            Id = "Bearer"
        //        },
        //        Scheme = "Bearer",
        //        Name = "Bearer",
        //        In = Microsoft.OpenApi.Models.ParameterLocation.Header
        //    },
        //    new List<string>()
        //}
        //});
    });


    builder.Services.AddTransient<ErrorHandlingMiddleware>();

    builder.Services.AddInfrastructure(builder.Configuration)
        .AddApplication(builder.Configuration);


    builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

    builder.Services.AddApplicationInsightsTelemetry();

    builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration)
    );
    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<FixGoDbContext>();

        try
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                await context.Database.MigrateAsync();
            }

            // Seed roles first
            await RoleSeeder.SeedAsync(context);

            // Then seed users after roles exist
            await UserSeeder.SeedUsers(context);

            Console.WriteLine("Seeding complete.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seeding or migration failed: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            throw;
        }
    }



    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseSerilogRequestLogging();


    
    app.UseSwagger();
    app.UseSwaggerUI();
    

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application Startup Failed");
}
finally
{
    Log.CloseAndFlush();
}