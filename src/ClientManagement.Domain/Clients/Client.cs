using BuildBlocks.Core.DomainObjects;
using ClientManagement.Domain.Clients.Entities;
using ClientManagement.Domain.Clients.ValueObjects;

namespace ClientManagement.Domain.Clients
{
    public class Client : Entity<ClientId>
    {
        private ICollection<PublicThoroughfare> publicThoroughfares = new List<PublicThoroughfare>();
        private Client() { }
        private Client(ClientId id, Name name, string email, string logo, ICollection<PublicThoroughfare> publicThoroughfares)
            : base(id)
        {
            Name = name;
            Email = email;
            Logo = logo;
            foreach (var item in publicThoroughfares)
            {
                this.publicThoroughfares.Add(item);
            }
        }
        public Name Name { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public IReadOnlyCollection<PublicThoroughfare> PublicThoroughfares => publicThoroughfares.ToList().AsReadOnly();
        public static Client Create(Name name, string email, string logo, ICollection<PublicThoroughfare> publicThoroughfares)
        {
            return new Client(ClientId.Create(), name, email, logo, publicThoroughfares);
        }
        public void AddPublicThoroughfare(PublicThoroughfare publicThoroughfare)
        {
            publicThoroughfares.Add(publicThoroughfare);
        }
    }
}
