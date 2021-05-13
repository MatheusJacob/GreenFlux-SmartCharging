using System;
using System.Collections.Generic;

namespace GreenFlux.SmartCharging.Matheus.Domain.Models
{
    public class Group
    {
        public readonly Guid Id;

        public string Name { get; set; }

        public float Capacity { get; set; }

        public readonly Dictionary<Guid, ChargeStation> ChargeStations;
    }
}
