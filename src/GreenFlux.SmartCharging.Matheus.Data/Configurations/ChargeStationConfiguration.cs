using GreenFlux.SmartCharging.Matheus.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenFlux.SmartCharging.Matheus.Data.Configurations
{
    class ChargeStationConfiguration : IEntityTypeConfiguration<ChargeStation>
    {
        public void Configure(EntityTypeBuilder<ChargeStation> builder)
        {
            builder.ToTable("ChargeStation");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .IsRequired();

            builder.HasMany(g => g.Connectors)
                .WithOne(c => c.ChargeStation)
                .HasForeignKey(c => c.ChargeStationId);
        }
    }
}
