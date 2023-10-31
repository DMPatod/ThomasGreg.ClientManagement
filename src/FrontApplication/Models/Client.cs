namespace FrontApplication.Models
{
    public class Client
    {
        public string Id { get; set; }
        public Name Name { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public List<PublicThoroughfare> PublicThoroughfares { get; set; } = new List<PublicThoroughfare>();
    }
}
