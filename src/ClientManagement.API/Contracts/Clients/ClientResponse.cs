using ClientManagement.API.Contracts.Names;
using ClientManagement.API.Contracts.PublicThoroughfares;

namespace ClientManagement.API.Contracts.Clients
{
    public record ClientResponse(string? Id, NameResponse? Name, string? Email, string? Logo, ICollection<PublicThoroughfareResponse>? PublicThoroughfares);
}
