namespace ClientManagement.Application.PublicThoroughfares
{
    public record PublicThoroughfareUpdateCommand(string Id, string Street, int Number, string City, string State, string AditionalInformation)
    {
    }
}
