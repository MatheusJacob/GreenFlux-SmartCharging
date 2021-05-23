using System;

namespace GreenFlux.SmartCharging.Matheus.API.Resources
{
    public class GroupResource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float? Capacity { get; set; }
    }
}
