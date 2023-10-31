using Microsoft.Extensions.DependencyInjection;
using ClientManagement.Infrastructure.DataPersistence;
using Microsoft.Extensions.Configuration;

namespace ClientManagement.Infrastructure
{
    public static class BuildHandler
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MsSql");

            services.AddDataPersistence(connectionString);

            return services;
        }
    }
}
