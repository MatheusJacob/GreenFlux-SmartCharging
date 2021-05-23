using GreenFlux.SmartCharging.Matheus.Data;
using GreenFlux.SmartCharging.Matheus.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Tests.Unit
{
    public class SuggestionTests : BaseTest
    {
        public SuggestionTests() : base(new DbContextOptionsBuilder<ApplicationDbContext>()
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
        public void Should_init_suggestion()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                Assert.DoesNotThrow(() => new Suggestion(Guid.NewGuid(),1,5f));
                Guid guid = Guid.NewGuid();
                Suggestion suggestion = new Suggestion(guid, 1, 5f);

                Assert.AreEqual(guid, suggestion.ChargeStationId);
                Assert.AreEqual(1, suggestion.ConnectorId);
                Assert.AreEqual(5f, suggestion.MaxCurrentAmp);
            }
        }
    }
}