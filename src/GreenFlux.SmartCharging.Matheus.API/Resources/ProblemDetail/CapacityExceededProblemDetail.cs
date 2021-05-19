using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Matheus.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GreenFlux.SmartCharging.Matheus.API.Resources.ProblemDetail
{
    public class CapacityExceededProblemDetail : ProblemDetails
    {
        public readonly float ExceededCapacity;
        public readonly RemoveSuggestions RemoveSuggestions;

        public CapacityExceededProblemDetail(float exceededCapacity, RemoveSuggestions  removeSuggestions)
        {
            Title = "Capacity exceeded";
            Status = 400;
            Type = typeof(CapacityExceededProblemDetail).ToString();
            Detail = $"The group capacity has exceeded by {exceededCapacity}A, remove the suggested connectors to free up space";
            ExceededCapacity = exceededCapacity;
            RemoveSuggestions = removeSuggestions;
        }
    }
}
