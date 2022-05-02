using Identity.Domain.Entities;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Repositories.Implementation;
using Identity.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure
{
    public static class DependencyInjection
    {
        private const string MigrationAssemblyName = "Identity.Infrastructure";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var server = configuration["DBServer"];
            ValidateConfiguration("DBServer", server);

            var port = configuration["DBPort"];
            ValidateConfiguration("DBPort", port);

            var user = configuration["DBUser"];
            ValidateConfiguration("DBUser", user);

            var password = configuration["DBPassword"];
            ValidateConfiguration("DBPassword", password);

            var databaseName = configuration["DBName"];
            ValidateConfiguration("DBName", databaseName);

            var connectionString =
                $"Server={server},{port};Initial Catalog={databaseName};User ID={user};Password={password}";

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly(MigrationAssemblyName))
                    .EnableSensitiveDataLogging()
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking),
                ServiceLifetime.Transient);

            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddIdentity();

            services.BuildServiceProvider()
                .GetService<IdentityContext>()
                ?.Migrate();
            
            return services;
        }

        private static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<IdentityContext>();

            return services;
        }

        private static void ValidateConfiguration(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException($"{name} was not provided");
            }
        }
    }
}
