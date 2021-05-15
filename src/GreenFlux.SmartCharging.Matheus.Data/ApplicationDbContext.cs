using GreenFlux.SmartCharging.Matheus.Data.Configurations;
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
            builder.ApplyConfigurationsFromAssembly(typeof(GroupConfiguration).Assembly);

        }
    }
}
