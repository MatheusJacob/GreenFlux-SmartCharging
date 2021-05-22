using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
