using System.Collections.Generic;

namespace GreenFlux.SmartCharging.Matheus.API.Resources
{
    public class SaveChargeStationResource
    {
        public string Name { get; set; }

        public ICollection<SaveConnectorResource> Connectors { get; set; }
    }
}
