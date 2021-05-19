using GreenFlux.SmartCharging.Matheus.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Domain.Exceptions
{
    public class CapacityExceededException :  Exception
    {
        public readonly float ExceededCapacity;
        public readonly RemoveSuggestions RemoveSuggestions;

        public CapacityExceededException(float exceededCapacity, RemoveSuggestions removeSuggestions) : 
            base($"The group capacity has exceeded by {exceededCapacity}A, remove the suggested connectors to free up space")
        {
            ExceededCapacity = exceededCapacity;
            RemoveSuggestions = removeSuggestions;
        }
    }
}
