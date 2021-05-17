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

        public float MaxCurrentAmp { get; private set; }

        public ChargeStation ChargeStation { get; set; }

        public Guid ChargeStationId { get; set; }

        public Connector(int id)
        {
            Id = id;
        }

        private Connector(float maxCurrentAmp)
        {
            MaxCurrentAmp = maxCurrentAmp;
        }

        private Connector(int id, float maxCurrentAmp)
        {
            Id = id;
            MaxCurrentAmp = maxCurrentAmp;
        }

        public void ChangeMaxCurrentAmp(float maxCurrentAmp)
        {
            float differenceInCurrent = maxCurrentAmp - this.MaxCurrentAmp;
            bool needsToRecalculate = maxCurrentAmp > this.MaxCurrentAmp;

            this.MaxCurrentAmp = maxCurrentAmp;

            if (needsToRecalculate)
            {
                if(ChargeStation.Group.CalculateGroupSumCurrentAmp() > ChargeStation.Group.Capacity)
                    throw new Exception("Capacity overflow");
            }

            ChargeStation.UpdateTotalMaxCurrentAmp(differenceInCurrent);
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
