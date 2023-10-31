using BuildBlocks.Core.DomainObjects;

namespace ClientManagement.Domain.Clients.ValueObjects
{
    public class PublicThoroughfareId : ValueObject
    {
        private PublicThoroughfareId(Guid value)
        {
            Value = value;
        }
        public Guid Value { get; set; }
        public static PublicThoroughfareId Create()
        {
            return new PublicThoroughfareId(Guid.NewGuid());
        }
        public static PublicThoroughfareId Create(Guid val)
        {
            return new PublicThoroughfareId(val);
        }
        public static PublicThoroughfareId Create(string val)
        {
            var guid = Guid.Parse(val);
            return new PublicThoroughfareId(guid);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
