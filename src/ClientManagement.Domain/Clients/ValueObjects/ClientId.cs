using BuildBlocks.Core.DomainObjects;

namespace ClientManagement.Domain.Clients.ValueObjects
{
    public class ClientId : ValueObject
    {
        private ClientId(Guid value)
        {
            Value = value;
        }
        public Guid Value { get; set; }
        public static ClientId Create()
        {
            return new ClientId(Guid.NewGuid());
        }
        public static ClientId Create(Guid val)
        {
            return new ClientId(val);
        }
        public static ClientId Create(string val)
        {
            var guid = Guid.Parse(val);
            return new ClientId(guid);
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
