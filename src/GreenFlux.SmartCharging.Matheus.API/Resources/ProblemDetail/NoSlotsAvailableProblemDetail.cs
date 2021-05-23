using Microsoft.AspNetCore.Mvc;
using System;

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
