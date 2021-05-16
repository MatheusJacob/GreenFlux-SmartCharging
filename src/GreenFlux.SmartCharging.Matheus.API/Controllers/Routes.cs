using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.API.Controllers
{
    public static class Routes
    {
        public const string ApiBaseRoute = "api";

        public const string GroupsRoute = ApiBaseRoute + "/groups";
        public const string ChargeStationsRoute = ApiBaseRoute + "/groups/{groupId}/chargeStations";
        public const string ConnectorsRoute = ApiBaseRoute + "/groups/{groupId}/chargeStations/{chargeStationId}/connectors";
    }
}
