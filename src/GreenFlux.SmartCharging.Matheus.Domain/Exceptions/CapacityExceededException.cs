using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Domain.Models
{
    public class CapacityExceededException : ProblemDetails
    {
        public readonly float ExceededCapacity;
        public CapacityExceededException(float exceededCapacity)
        {
            Title = "Capacity exceeded";
            Status = 400;
            Type = typeof(CapacityExceededException).ToString();
            Detail = $"The group capacity has exceeded by {exceededCapacity}A, remove the suggested connectors to free up space";
            ExceededCapacity = exceededCapacity;
        }
    }
}
