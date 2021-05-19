using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Domain.Exceptions
{
    public class NoSlotsAvailableException : Exception
    {
        public readonly Guid ChargeStationId;
        public NoSlotsAvailableException(Guid chargeStationId) : 
            base($"No slots available for the Charge Station : {chargeStationId}")
        {           
            ChargeStationId = chargeStationId;
        }
    }
}
