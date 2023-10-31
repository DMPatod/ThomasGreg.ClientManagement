namespace ClientManagement.Domain.Clients.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAsync(CancellationToken cancellationToken = default);
        Task<Client?> GetAsync(string id, CancellationToken cancellationToken = default);
        Task<Client?> CreateAsync(Client client, CancellationToken cancellationToken = default);
        Task<Client> UpdateAsync(Client client, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}
