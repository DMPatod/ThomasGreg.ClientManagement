using BuildBlocks.Core.DomainObjects;

namespace ClientManagement.Domain.Clients.ValueObjects
{
    public class Name : ValueObject
    {
        private Name() { }
        private Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static Name Create(string firstName, string lastName)
        {
            return new Name(firstName, lastName);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
