using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Domain.Exceptions
{
    public class NoSlotsAvailableException : ProblemDetails
    {
        private Guid ChargeStationId { get; set; }
        public NoSlotsAvailableException(Guid chargeStationId)
        {
            Title = "No slots available for the Charge Station";
            Status = 400;
            Type = typeof(NoSlotsAvailableException).ToString();
            Detail = $"No slots available for the Charge Station : {chargeStationId}";
            ChargeStationId = chargeStationId;
        }
    }
}
