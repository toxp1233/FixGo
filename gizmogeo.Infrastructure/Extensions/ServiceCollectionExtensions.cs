using gizmogeo.Domain.Interfaces;
using gizmogeo.Infrastructure.Persistance;
using gizmogeo.Infrastructure.Repositories;
using gizmogeo.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace gizmogeo.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("FixGoDb");
        services.AddDbContext<FixGoDbContext>(opt =>
            opt.UseSqlServer(connectionString)
        );
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAttachmentRepository, AttachmentRepository>();
        services.AddScoped<IAcceptedRequestsRepository, AcceptedRequestsRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();
        services.AddScoped<ICompletedOrderRepository, CompletedOrderRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<IPhoneVerificationService, PhoneVerificationService>();



        return services;
    }
}
