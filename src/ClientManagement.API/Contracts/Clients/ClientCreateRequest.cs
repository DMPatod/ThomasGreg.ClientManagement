using ClientManagement.API.Contracts.Names;
using ClientManagement.API.Contracts.PublicThoroughfares;

namespace ClientManagement.API.Contracts.Clients
{
    public record ClientCreateRequest(NameCreateRequest? Name, string? Email, string? Logo, ICollection<PublicThoroughfareCreateRequest>? PublicThoroughfares);
}
