namespace ClientManagement.Domain.Clients.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> FindAsync(CancellationToken cancellationToken = default);
        Task<Client?> FindAsync(string id, CancellationToken cancellationToken = default);
        Task<Client?> CreateAsync(Client client, CancellationToken cancellationToken = default);
        Task<Client> UpdateAsync(Client client, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}
