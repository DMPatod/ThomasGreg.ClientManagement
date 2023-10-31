using BuildBlocks.Core.DomainObjects;
using ClientManagement.Domain.Clients.ValueObjects;

namespace ClientManagement.Domain.Clients.Entities
{
    public class PublicThoroughfare : Entity<PublicThoroughfareId>
    {
        private PublicThoroughfare() { }
        private PublicThoroughfare(PublicThoroughfareId id, string street, int number, string city, string state, string aditionalInformation)
            : base(id)
        {
            Street = street;
            Number = number;
            City = city;
            State = state;
            AditionalInformation = aditionalInformation;
        }
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AditionalInformation { get; set; }

        public static PublicThoroughfare Create(string street, int number, string city, string state, string aditionalInformation)
        {
            return new PublicThoroughfare(PublicThoroughfareId.Create(), street, number, city, state, aditionalInformation);
        }
    }
}
