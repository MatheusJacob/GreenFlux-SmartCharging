using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenFlux.SmartCharging.Matheus.Domain.Models
{
    public class Group
    {
        public readonly Guid Id;

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(float.Epsilon, float.PositiveInfinity)]
        public float? Capacity { get; set; }

        public readonly HashSet<ChargeStation> ChargeStations;
    }
}
