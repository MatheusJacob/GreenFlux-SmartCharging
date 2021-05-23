using System;

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
