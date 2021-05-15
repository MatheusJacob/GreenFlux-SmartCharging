using GreenFlux.SmartCharging.Matheus.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GreenFlux.SmartCharging.Matheus.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Group> Group { get; set; }

        public DbSet<Connector> Connector { get; set; }

        public DbSet<ChargeStation> ChargeStation { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Group>()
                .HasKey(g => g.Id);

            builder.Entity<Group>()
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(50);


            builder.Entity<ChargeStation>()
                .HasKey(c => c.Id);

            builder.Entity<ChargeStation>()
               .Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(50);

            builder.Entity<Connector>()
                .HasKey(c => c.Id);

        }
    }
}
