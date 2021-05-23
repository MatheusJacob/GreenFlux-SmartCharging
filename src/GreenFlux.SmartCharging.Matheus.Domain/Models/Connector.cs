using System;
using System.Collections.Generic;

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

        public Connector(float maxCurrentAmp)
        {
            MaxCurrentAmp = maxCurrentAmp;
        }

        public Connector(int id, float maxCurrentAmp)
        {
            Id = id;
            MaxCurrentAmp = maxCurrentAmp;
        }

        public void ChangeMaxCurrentAmp(float maxCurrentAmp)
        {
            float differenceInCurrent = maxCurrentAmp - this.MaxCurrentAmp;

            this.MaxCurrentAmp = maxCurrentAmp;

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
