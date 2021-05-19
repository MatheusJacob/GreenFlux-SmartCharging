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

        public Suggestion(Guid chargeStationId, int connectorId)
        {
            ConnectorId = connectorId;
            ChargeStationId = chargeStationId;
        }
    }
}
