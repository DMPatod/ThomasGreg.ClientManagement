using BuildBlocks.Core.DomainEvents;

namespace BuildBlocks.Core.DomainObjects
{
    public abstract class Entity<TId> : DomainEventHolder, IEquatable<Entity<TId>>
        where TId : notnull
    {
        protected Entity()
        {
        }
        protected Entity(TId id)
        {
            Id = id;
        }
        public TId Id { get; }
        public bool Equals(Entity<TId>? other)
        {
            return Equals((object?)other);
        }
        public override bool Equals(object? obj)
        {
            return obj is Entity<TId> entity && Id.Equals(entity.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
