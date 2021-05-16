using GreenFlux.SmartCharging.Matheus.API.Controllers;
using GreenFlux.SmartCharging.Matheus.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.Tests.Integration.Drivers
{
    public class ChargeStationDriver :BaseDriver
    {
        private Func<string,string> ChargeStationUrl = groupId => Routes.GroupsRoute + "/" + groupId + Routes.ChargeStationBaseRoute;
        ChargeStationDriver() : base()
        {            
        }

        public async Task<HttpResponseMessage> CreateChargeStation(Guid groupId, string name, IEnumerable<SaveConnectorResource> connectors)
        {
            SaveChargeStationResource saveChargeStationResource = new SaveChargeStationResource()
            {
                Name = name,
                Connectors = connectors
            };

            return await Client.PostAsync(ChargeStationUrl(groupId.ToString()), ConvertToJsonData<SaveChargeStationResource>(saveChargeStationResource));
        }
    }
}
