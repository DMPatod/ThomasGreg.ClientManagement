using ClientManagement.Domain.Clients;
using ClientManagement.Domain.Clients.Repositories;
using ClientManagement.Domain.Clients.ValueObjects;
using ClientManagement.Infrastructure.DataPersistence.Converter;
using ClientManagement.Infrastructure.DataPersistence.TableTypes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClientManagement.Infrastructure.DataPersistence.SqlServer.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly SqlServerContext context;
        public ClientRepository(SqlServerContext context)
        {
            this.context = context;
        }

        public async Task<Client?> CreateAsync(Client client, CancellationToken cancellationToken = default)
        {
            var output = new SqlParameter("@Output", SqlDbType.UniqueIdentifier)
            {
                Direction = ParameterDirection.Output,
            };
            await context.Database.ExecuteSqlRawAsync(
                "EXEC CreateClient @Client, @PublicThoroughfares, @Output OUTPUT",
                new SqlParameter("@Client", SqlDbType.Structured)
                {
                    Value = client.ToDataTable(),
                    TypeName = "ClientType"
                },
                new SqlParameter("@PublicThoroughfares", SqlDbType.Structured)
                {
                    Value = client.PublicThoroughfares.ToDataTable(),
                    TypeName = "PublicThoroughfareType"
                },
                output);

            // Only for domainEvents propagation
            var entity = await context.Set<Client>().SingleOrDefaultAsync(c => c.Id == ClientId.Create((Guid)output.Value), cancellationToken);
            if (entity is not null)
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveAsync(cancellationToken);
            }
            return entity;
        }

        public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var output = new SqlParameter("@Output", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            await context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteClient @Id, @Output OUTPUT",
                new SqlParameter("@Id", id),
                output);

            return (int)output.Value > 0;
        }

        public async Task<IEnumerable<Client>> FindAsync(CancellationToken cancellationToken = default)
        {
            var clients = await context.Set<Client>().ToListAsync(cancellationToken);
            return clients;
        }

        public async Task<Client?> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            var client = await context.Set<Client>().SingleOrDefaultAsync(ent => ent.Id == ClientId.Create(id), cancellationToken);
            return client;
        }

        public async Task<Client> UpdateAsync(Client client, CancellationToken cancellationToken = default)
        {
            context.Set<Client>().Update(client);
            await context.SaveChangesAsync(cancellationToken);
            return client;
        }
    }
}
