using ClientManagement.Domain.Clients;
using ClientManagement.Domain.Clients.Repositories;
using ClientManagement.Domain.Clients.ValueObjects;
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

        private DataTable ToDataTable<T>(IEnumerable<T> param)
        {
            var table = new DataTable();
            var propInfo = typeof(T).GetProperties();

            foreach (var item in propInfo)
            {
                if (item.Name == "Id" || item.Name == "DomainEvents")
                {
                    continue;
                }
                table.Columns.Add(item.Name, Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType);
            }

            foreach (var item in param)
            {
                var row = table.NewRow();
                foreach (var prop in propInfo)
                {
                    if (prop.Name == "Id" || prop.Name == "DomainEvents")
                    {
                        continue;
                    }
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }

            return table;
        }

        public async Task<Client?> CreateAsync(Client client, CancellationToken cancellationToken = default)
        {
            var table = new DataTable();
            table.Rows.Add(table.NewRow());

            var output = new SqlParameter("@Output", SqlDbType.UniqueIdentifier)
            {
                Direction = ParameterDirection.Output,
            };
            await context.Database.ExecuteSqlRawAsync(
                "EXEC CreateClient @FirstName, @LastName, @Email, @Logo, @PublicThoroughfares, @Output OUTPUT",
                new SqlParameter("@FirstName", client.Name.FirstName),
                new SqlParameter("@LastName", client.Name.LastName),
                new SqlParameter("@Email", client.Email),
                new SqlParameter("@Logo", client.Logo),
                new SqlParameter("@PublicThoroughfares", SqlDbType.Structured)
                {
                    Value = ToDataTable(client.PublicThoroughfares),
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

        public async Task<IEnumerable<Client>> GetAsync(CancellationToken cancellationToken = default)
        {
            var clients = await context.Set<Client>().ToListAsync(cancellationToken);
            return clients;
        }

        public async Task<Client?> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            var client = await context.Set<Client>().SingleOrDefaultAsync(ent => ent.Id == ClientId.Create(id), cancellationToken);
            return client;
        }

        public Task<Client> UpdateAsync(Client client, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
