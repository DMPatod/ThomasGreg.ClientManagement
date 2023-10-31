using BuildBlocks.Core.DataPersistence;
using BuildBlocks.Core.DomainEvents;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ClientManagement.Infrastructure.DataPersistence.SqlServer
{
    public class SqlServerContext : DbContext, IDomainContext
    {
        private readonly IDomainMessageHandler messageHandler;
        public SqlServerContext(DbContextOptions<SqlServerContext> options, IDomainMessageHandler messageHandler)
            : base(options)
        {
            this.messageHandler = messageHandler;
        }
        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await DispatchDomainEvents(cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        private async Task DispatchDomainEvents(CancellationToken cancellationToken = default)
        {
            List<DomainEventHolder> eventHolders = ChangeTracker.Entries()
                .Where(x => x.Entity is DomainEventHolder)
                .Select(x => (DomainEventHolder)x.Entity)
                .ToList();

            foreach (var eventHolder in eventHolders)
            {
                while (eventHolder.TryRemoveDomainEvent(out var domainEvent))
                {
                    await messageHandler.PublishAsync(domainEvent, cancellationToken);
                }
            }
        }
    }
}
