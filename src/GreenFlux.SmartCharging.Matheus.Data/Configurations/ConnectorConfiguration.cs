using GreenFlux.SmartCharging.Matheus.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenFlux.SmartCharging.Matheus.Data.Configurations
{
    class ConnectorStationConfiguration : IEntityTypeConfiguration<Connector>
    {
        public void Configure(EntityTypeBuilder<Connector> builder)
        {
            builder.ToTable("Conector");

            builder.HasKey(c => new { c.Id, c.ChargeStationId });

            builder.Property(g => g.Id)
                .ValueGeneratedNever();

            builder.Property(g => g.MaxCurrentAmp)
                .IsRequired();            
        }
    }
}
