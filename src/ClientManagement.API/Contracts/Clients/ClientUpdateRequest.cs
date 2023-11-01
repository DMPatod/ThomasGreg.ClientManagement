using ClientManagement.API.Contracts.Names;
using ClientManagement.API.Contracts.PublicThoroughfares;

namespace ClientManagement.API.Contracts.Clients
{
    public record ClientUpdateRequest(string Id, NameRequest? Name, string? Email, string? Logo, IEnumerable<PublicThoroughfareUpdateRequest>? PublicThoroughfares);
}
