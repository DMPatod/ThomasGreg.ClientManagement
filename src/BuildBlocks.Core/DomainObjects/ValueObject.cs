namespace BuildBlocks.Core.DomainObjects
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public bool Equals(ValueObject? other)
        {
            return Equals(other);
        }
        public abstract IEnumerable<object> GetEqualityComponents();
        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObject vo = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(vo.GetEqualityComponents());
        }
        public override int GetHashCode()
        {
            return GetEqualityComponents().Select(x => x?.GetHashCode() ?? 0).Aggregate((x, y) => x ^ y);
        }
    }
}
