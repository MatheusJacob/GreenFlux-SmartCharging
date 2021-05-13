using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Domain.Models
{
    public class Connector
    {
        public readonly int Id;

        public string Name { get; set; }

        public float MaxCurrentAmp { get; set; }
    }
}
