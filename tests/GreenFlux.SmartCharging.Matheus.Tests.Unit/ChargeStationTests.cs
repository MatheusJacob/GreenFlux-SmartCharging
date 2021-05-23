using GreenFlux.SmartCharging.Matheus.Data;
using GreenFlux.SmartCharging.Matheus.Domain.Exceptions;
using GreenFlux.SmartCharging.Matheus.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Tests.Unit
{
    public class ChargeStationTests : BaseTest
    {
        public ChargeStationTests() : base(new DbContextOptionsBuilder<ApplicationDbContext>()
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
        public void  Should_Create_a_new_charge_station_without_errors()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                Connector connector = new Connector(5);
                context.Add(connector);
                ChargeStation chargeStation = new ChargeStation(Guid.NewGuid(), "Charge Station 3", connector);
                
                context.Add(chargeStation);
                
                Groups[0].AppendChargeStation(chargeStation);
                Assert.DoesNotThrowAsync(async () => await context.ChargeStation.AddAsync(chargeStation));
                Assert.AreNotEqual(Guid.Empty, chargeStation.Id);

                Assert.DoesNotThrow(() => context.SaveChanges());
            }
        }

        [Test]
        public void Should_compare_charge_stations_based_on_id()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                ChargeStation chargeStation1 = ChargeStations[0];
                ChargeStation chargeStation2 = ChargeStations[1];


                Assert.IsFalse(chargeStation1.Equals(chargeStation2));

                Guid guid = Guid.NewGuid();
                ChargeStation chargeStationDuplicated = new ChargeStation(guid);
                ChargeStation chargeStationDuplicated2 = new ChargeStation(guid);

                Assert.IsTrue(chargeStationDuplicated.Equals(chargeStationDuplicated2));
                Assert.DoesNotThrow(() => context.SaveChanges());
            }
        }

        [Test]
        public void Should_generate_connector_ids_sequencially()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                ChargeStation chargeStation = ChargeStations[1];

                Connector connector;
                Assert.IsTrue(chargeStation.Connectors.TryGetValue(new Connector(1), out connector));
                Assert.AreEqual(1, connector.Id);
                Connector connector2 = new Connector(5f);
                Connector connector3 = new Connector(5f);
                Connector connector4 = new Connector(5f);
                Connector connector5 = new Connector(5f);
                
                chargeStation.AppendConnector(connector2);
                chargeStation.AppendConnector(connector3);
                chargeStation.AppendConnector(connector4);
                chargeStation.AppendConnector(connector5);

                Assert.AreEqual(2, connector2.Id);
                Assert.AreEqual(3, connector3.Id);
                Assert.AreEqual(4, connector4.Id);
                Assert.AreEqual(5, connector5.Id);

                
                Assert.DoesNotThrow( () => context.SaveChanges());
            }
               
        }

        [Test]
        public void Should_not_let_append_more_than_5_connectors()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                ChargeStation chargeStation = ChargeStations[1];

                Connector connector;
                Assert.IsTrue(chargeStation.Connectors.TryGetValue(new Connector(1), out connector));
                Assert.AreEqual(1, connector.Id);
                Connector connector2 = new Connector(5f);
                Connector connector3 = new Connector(5f);
                Connector connector4 = new Connector(5f);
                Connector connector5 = new Connector(5f);
                Connector connector6 = new Connector(5f);

                chargeStation.AppendConnector(connector2);
                chargeStation.AppendConnector(connector3);
                chargeStation.AppendConnector(connector4);
                chargeStation.AppendConnector(connector5);
                Assert.Throws<NoSlotsAvailableException>(() => chargeStation.AppendConnector(connector6));

                Assert.DoesNotThrow(() => context.SaveChanges());
            }
        }

        [Test]
        public void Should_update_charge_Station_max_current_when_adding_connector()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                ChargeStation chargeStation = ChargeStations[0];

                Assert.AreEqual(60.5f, chargeStation.TotalMaxCurrentAmp);
                Connector connector = new Connector(5, 5);
                chargeStation.AppendConnector(connector);
                Assert.AreEqual(65.5f, chargeStation.TotalMaxCurrentAmp);               

                Assert.DoesNotThrow(() => context.SaveChanges());
            }
        }

        [Test]
        public void Should_init_instance_properly()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                List<Connector> connectors = new List<Connector>();
                connectors.Add(new Connector(1, 1));
                connectors.Add(new Connector(2));
                connectors.Add(new Connector(5f));

                Assert.DoesNotThrow(() => new ChargeStation(Guid.NewGuid(), "new Charge station", connectors));

                Assert.DoesNotThrow(() => new ChargeStation("new Charge station", connectors));

                Assert.DoesNotThrow(() => context.SaveChanges());
            }
        }

        [Test]
        public void Should_sync_connectors_id_properly()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                List<Connector> connectors = new List<Connector>();
                connectors.Add(new Connector(1, 1));
                connectors.Add(new Connector(2));
                connectors.Add(new Connector(5f));

                Assert.DoesNotThrow( () => ChargeStations[1].SyncConnectorIds());

                ChargeStations[1].AppendConnectors(connectors);

                Assert.DoesNotThrow(() => ChargeStations[1].SyncConnectorIds());

                Assert.DoesNotThrow(() => context.SaveChanges());
            }
        }

        [Test]
        public void Should_not_compare_against_null()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                Assert.IsFalse(ChargeStations[0].Equals(null));

                Assert.AreEqual(ChargeStations[0].Id.GetHashCode(), ChargeStations[0].GetHashCode());
               
                Assert.DoesNotThrow(() => context.SaveChanges());
            }
        }
    }
}