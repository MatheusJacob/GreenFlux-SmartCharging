using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.API.Resources
{
    public class GroupResource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float? Capacity { get; set; }
    }
}
