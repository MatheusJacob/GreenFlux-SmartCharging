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
    public class ConnectorDriver : BaseDriver
    {
        private Func<string,string, string> ConnectorUrl = (groupId, chargeStationId) => Routes.GroupsRoute + groupId +
        Routes.ChargeStationBaseRoute + chargeStationId + Routes.ConnectorsBaseRoute;

        ConnectorDriver() : base()
        {            
        }
    }
}
