namespace ClientManagement.API.Contracts.PublicThoroughfares
{
    public record PublicThoroughfareCreateRequest(string? Street, int? Number, string? City, string? State, string? AditionalInformation);
}
