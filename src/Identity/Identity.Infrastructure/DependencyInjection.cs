using Identity.Domain.Entities;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Repositories.Implementation;
using Identity.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure
{
    public static class DependencyInjection
    {
        private const string MigrationAssemblyName = "Identity.Infrastructure";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly(MigrationAssemblyName))
                    .EnableSensitiveDataLogging()
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking),
                ServiceLifetime.Transient);

            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddIdentity();

            return services;
        }

        private static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<IdentityContext>();

            return services;
        }
    }
}
