namespace ClientManagement.API.Contracts.PublicThoroughfares
{
    public record PublicThoroughfareUpdateRequest(string? Id, string? Street, int? Number, string? City, string? State, string? AditionalInformation);
}
