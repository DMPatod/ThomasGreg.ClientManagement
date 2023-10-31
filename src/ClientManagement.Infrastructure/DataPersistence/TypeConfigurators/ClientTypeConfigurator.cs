using ClientManagement.Domain.Clients;
using ClientManagement.Domain.Clients.Entities;
using ClientManagement.Domain.Clients.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientManagement.Infrastructure.DataPersistence.TypeConfigurators
{
    public class ClientTypeConfigurator : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            ConfigureClientsTable(builder);
        }
        private void ConfigureClientsTable(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ClientId.Create(value));

            builder.OwnsOne(c => c.Name, np => ConfigureNameSplit(np, builder));

            builder.Property(c => c.Email);

            builder.Property(c => c.Logo);

            builder.OwnsMany(c => c.PublicThoroughfares, pb => ConfigurePublicThoroughfaresTable(pb, builder));

            builder.Ignore(c => c.DomainEvents);
        }

        private void ConfigurePublicThoroughfaresTable(OwnedNavigationBuilder<Client, PublicThoroughfare> pb, EntityTypeBuilder<Client> builder)
        {
            pb.ToTable("PublicThoroughfares");

            pb.HasKey(p => p.Id);

            pb.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => PublicThoroughfareId.Create(value));

            pb.Property(p => p.Street);

            pb.Property(p => p.Number);

            pb.Property(p => p.City);

            pb.Property(p => p.State);

            pb.Property(p => p.AditionalInformation);
        }

        private void ConfigureNameSplit(OwnedNavigationBuilder<Client, Name> np, EntityTypeBuilder<Client> builder)
        {
            np.Property(n => n.FirstName).HasColumnName("FirstName");

            np.Property(n => n.LastName).HasColumnName("LastName");
        }
    }
}
