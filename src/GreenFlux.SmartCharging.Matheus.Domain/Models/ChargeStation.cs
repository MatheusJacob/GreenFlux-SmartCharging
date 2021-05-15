using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Domain.Models
{
    public class ChargeStation
    {
        private const int MaxConnectors = 5;
        private const int MinConnectors = 1;
        private readonly HashSet<int> _availableSlots;

        public readonly Guid Id;

        public String Name { get; set; }

        public readonly HashSet<Connector> Connectors;
    }
}
