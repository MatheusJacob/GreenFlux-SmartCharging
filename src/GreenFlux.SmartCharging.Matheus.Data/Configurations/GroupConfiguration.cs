using GreenFlux.SmartCharging.Matheus.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Data.Configurations
{
    class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Group");

            builder.Property(g => g.Id)
                .ValueGeneratedNever();

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .IsRequired();

            builder.Property(g => g.Capacity)
                    .IsRequired();

            builder.HasMany(g => g.ChargeStations.Values)
                .WithOne();                         
        }
    }
}
