using BuildBlocks.Core.DataPersistence;
using ClientManagement.Domain.Clients.Repositories;
using ClientManagement.Infrastructure.DataPersistence.SqlServer;
using ClientManagement.Infrastructure.DataPersistence.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClientManagement.Infrastructure.DataPersistence
{
    public static class BuildHandler
    {
        public static IServiceCollection AddDataPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SqlServerContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            services.AddScoped<IDomainContext, SqlServerContext>();

            services.AddTransient<IClientRepository, ClientRepository>();

            return services;
        }
    }
}
