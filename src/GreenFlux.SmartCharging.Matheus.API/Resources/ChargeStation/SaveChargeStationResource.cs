using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.API.Resources
{
    public class SaveChargeStationResource
    {                
        public string Name { get; set; }      
        
        public IEnumerable<SaveConnectorResource> Connectors { get; set; }
    }
}
