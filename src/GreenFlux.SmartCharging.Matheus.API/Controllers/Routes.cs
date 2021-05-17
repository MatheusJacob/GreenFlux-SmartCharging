using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.API.Controllers
{
    public static class Routes
    {
        public const string ApiBaseRoute = "api";
        public const string ChargeStationBaseRoute = "/chargeStations";
        public const string GroupBaseRoute = "/groups";
        public const string ConnectorsBaseRoute = "/connectors";

        public const string GroupsRoute = ApiBaseRoute + GroupBaseRoute;
        public const string ChargeStationsRoute = ApiBaseRoute + GroupBaseRoute  + "/{groupId}" + ChargeStationBaseRoute;
        public const string ConnectorsRoute = ApiBaseRoute + GroupBaseRoute + "/{groupId}" + ChargeStationBaseRoute + "/{chargeStationId}" + ConnectorsBaseRoute;
    }
}
