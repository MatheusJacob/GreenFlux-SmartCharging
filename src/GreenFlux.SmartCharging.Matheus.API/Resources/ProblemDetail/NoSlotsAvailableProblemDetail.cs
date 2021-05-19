using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.API.Resources.ProblemDetail
{
    public class NoSlotsAvailableProblemDetail : ProblemDetails
    {
        private Guid ChargeStationId { get; set; }
        public NoSlotsAvailableProblemDetail(Guid chargeStationId)
        {
            Title = "No slots available for the Charge Station";
            Status = 400;
            Type = typeof(NoSlotsAvailableProblemDetail).ToString();
            Detail = $"No slots available for the Charge Station : {chargeStationId}";
            ChargeStationId = chargeStationId;
        }
    }
}
