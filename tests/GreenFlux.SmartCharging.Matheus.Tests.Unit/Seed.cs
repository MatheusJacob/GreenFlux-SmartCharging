using GreenFlux.SmartCharging.Matheus.Data;
using GreenFlux.SmartCharging.Matheus.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Tests.Unit
{
    public abstract class BaseTest
    {
        protected BaseTest(DbContextOptions<ApplicationDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Groups = new List<Group>();
            ChargeStations = new List<ChargeStation>();
            Connectors = new List<Connector>();

            Seed();
        }

        protected DbContextOptions<ApplicationDbContext> ContextOptions { get; }

        protected List<Group> Groups { get; set; }

        protected List<ChargeStation> ChargeStations { get; set; }


        protected List<Connector> Connectors { get; set; }


        public void Seed()
        {
            Groups = new List<Group>();
            ChargeStations = new List<ChargeStation>();
            Connectors = new List<Connector>();

            using (var context = new ApplicationDbContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Group groupOne = new Group();
                groupOne.Capacity = 100;
                groupOne.Name = "G1";

                Group groupTwo = new Group();
                groupTwo.Capacity = 20;
                groupTwo.Name = "G2";

                context.AddRange(groupOne, groupTwo);


                ChargeStation chargeStation1 = new ChargeStation() { Name = "ChargeStation 1" };
                ChargeStation chargeStation2 = new ChargeStation() { Name = "ChargeStation 2" };

                context.AddRange(chargeStation1, chargeStation2);

                groupOne.AppendChargeStation(chargeStation1);
                groupTwo.AppendChargeStation(chargeStation2);

                Connector connector1 = new Connector(1,5);
                chargeStation1.AppendConnector(connector1);
                context.Add(connector1);

                Connector connector2 = new Connector(2,10);
                chargeStation1.AppendConnector(connector2);
                context.Add(connector2);

                Connector connector3 = new Connector(3, 30);
                chargeStation1.AppendConnector(connector3);
                context.Add(connector3);

                Connector connector4 = new Connector(4, 15.5f);
                chargeStation1.AppendConnector(connector4);
                context.Add(connector4);

                Connector connector5 = new Connector(1,35.5f);
                chargeStation2.AppendConnector(connector5);
                context.Add(connector5);

                groupOne.HasExceededCapacity(0);
                groupTwo.HasExceededCapacity(0);
                context.SaveChanges();

                Groups.Add(groupOne);
                Groups.Add(groupTwo);
                ChargeStations.Add(chargeStation1);
                ChargeStations.Add(chargeStation2);

                Connectors.Add(connector1);
                Connectors.Add(connector2);
                Connectors.Add(connector3);
                Connectors.Add(connector4);
                Connectors.Add(connector5);
            }
        }
    }
}
