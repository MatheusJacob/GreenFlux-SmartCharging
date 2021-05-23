using GreenFlux.SmartCharging.Matheus.Data;
using GreenFlux.SmartCharging.Matheus.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Tests.Unit
{
    public class ConnectorTests : BaseTest
    {
        public ConnectorTests() : base(new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "SmartChargingMatheusTest")
            .Options)
        {

        }

        [SetUp]
        public void Setup()
        {
            Seed();
        }

        [Test]
        public void  Should_change_max_current_amp_correctly()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                Assert.AreEqual(5, Connectors[0].MaxCurrentAmp);
                Connectors[0].ChangeMaxCurrentAmp(10);
                Assert.AreEqual(10, Connectors[0].MaxCurrentAmp);

                Assert.DoesNotThrow(() => context.SaveChanges());
            }
        }

        [Test]
        public void Should_init_connector()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                Assert.DoesNotThrow(() => new Connector(1,5f));
                Assert.DoesNotThrow(() => new Connector(1));
                Assert.DoesNotThrow(() => new Connector(2, 5f));
            }
        }
    }
}