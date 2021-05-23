using GreenFlux.SmartCharging.Matheus.Data;
using GreenFlux.SmartCharging.Matheus.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Tests.Unit
{
    public class GroupTests : BaseTest
    {
        public GroupTests() : base(new DbContextOptionsBuilder<ApplicationDbContext>()
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
        public void  Should_Create_a_new_group_without_errors()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                Group group = new Group();
                group.Name = "Group test";
                group.Capacity = 100;

                Assert.AreEqual(Guid.Empty, group.Id);
                Assert.DoesNotThrowAsync(async () => await context.Group.AddAsync(group));
                Assert.AreNotEqual(Guid.Empty, group.Id);

                Assert.DoesNotThrow(() => context.SaveChanges());
            }
        }

        [Test]
        public void Should_exceed_Capacity_when_connectors_current_is_bigger_than_capacity()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                Group group = Groups[0];
                Connector connector = Connectors[0];

                Assert.IsFalse(group.HasExceededCapacity(0));

                connector.ChangeMaxCurrentAmp(50);
                Assert.IsTrue(group.HasExceededCapacity(0));
                Assert.DoesNotThrow(() => context.SaveChanges());
            }

        }

        [Test]
        public void Should_not_exceed_Capacity_when_connectors_current_is_smaller_than_capacity()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                Group group = Groups[0];
                Connector connector = Connectors[0];

                Assert.IsFalse(group.HasExceededCapacity(0));
                connector.ChangeMaxCurrentAmp(20);
                Assert.IsFalse(group.HasExceededCapacity(0));
                Assert.DoesNotThrow( () => context.SaveChanges());
            }
               
        }

        [Test]
        public void Should_calculate_group_sum_correctly()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                Group group = Groups[0];
                Connector connector = Connectors[0];

                Assert.AreEqual(60.5f, group.CalculateGroupSumCurrentAmp());

                connector.ChangeMaxCurrentAmp(25);

                Assert.AreEqual(80.5f, group.CalculateGroupSumCurrentAmp());
              
                Assert.DoesNotThrow(() => context.SaveChanges());
            }
        }

        [Test]
        public void Should_append_charge_Station_correctly()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                Group group = Groups[0];
                ChargeStation chargeStation = new ChargeStation();
                context.Add(chargeStation);
                Connector connector = new Connector(5, 5);
                context.Add(connector);
                chargeStation.AppendConnector(connector);
                group.AppendChargeStation(chargeStation);

                Assert.AreEqual(65.5f, group.CalculateGroupSumCurrentAmp()) ;
                Assert.AreEqual(2, group.ChargeStations.Count);

                Assert.DoesNotThrow(() => context.SaveChanges());
            }
        }

        [Test]
        public void Should_get_abs_exceeded_capacity()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                Group group = Groups[0];
                Connector connector = Connectors[0];

                Assert.IsFalse(group.HasExceededCapacity(0));

                connector.ChangeMaxCurrentAmp(50);
                Assert.IsTrue(group.HasExceededCapacity(0));

                Assert.AreEqual(5.5f, group.GetExceededCapacity());

                Assert.DoesNotThrow(() => context.SaveChanges());
            }
        }

        [Test]
        public void Should_init_group()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                Assert.DoesNotThrow(() => new Group(Guid.NewGuid(), "new group", 105.5f));
            }
        }
    }
}