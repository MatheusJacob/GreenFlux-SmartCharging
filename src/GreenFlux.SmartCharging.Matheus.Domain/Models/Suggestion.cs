using System;

namespace GreenFlux.SmartCharging.Matheus.Domain.Models
{
    public class Suggestion
    {
        public readonly int ConnectorId;

        public readonly Guid ChargeStationId;

        public readonly float MaxCurrentAmp;

        public Suggestion(Guid chargeStationId, int connectorId, float maxCurrentAmp)
        {
            ConnectorId = connectorId;
            ChargeStationId = chargeStationId;
            MaxCurrentAmp = maxCurrentAmp;
        }
    }
}
