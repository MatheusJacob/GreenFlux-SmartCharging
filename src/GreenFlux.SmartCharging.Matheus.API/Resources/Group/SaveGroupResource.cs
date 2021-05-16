using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.API.Resources
{
    public class SaveGroupResource
    {
        public string Name { get; set; }

        public float? Capacity { get; set; }
    }
}
