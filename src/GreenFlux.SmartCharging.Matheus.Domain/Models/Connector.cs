using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Domain.Models
{
    public class Connector
    {
        public int? Id { get; set; }

        public float MaxCurrentAmp { get; set; }

        public ChargeStation ChargeStation { get; set; }

        public Guid ChargeStationId { get; set; }

        private Connector(float maxCurrentAmp)
        {
            MaxCurrentAmp = maxCurrentAmp;
        }

        private Connector(int id, float maxCurrentAmp)
        {
            Id = id;
            MaxCurrentAmp = maxCurrentAmp;
        }
    }
 
    public class ConnectorComparer : IEqualityComparer<Connector>
    {
        public bool Equals(Connector a, Connector b)
        {
            return a.Id.Equals(b.Id);
        }

        public int GetHashCode(Connector obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
