namespace ClientManagement.Application.PublicThoroughfares
{
    public record PublicThoroughfareCreateCommand(string Street, int Number, string City, string State, string AditionalInformation);
}
