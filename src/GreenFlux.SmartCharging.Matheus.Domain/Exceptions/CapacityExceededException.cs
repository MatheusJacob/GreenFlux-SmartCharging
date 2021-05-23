using GreenFlux.SmartCharging.Matheus.Domain.Models;
using System;

namespace GreenFlux.SmartCharging.Matheus.Domain.Exceptions
{
    public class CapacityExceededException : Exception
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
