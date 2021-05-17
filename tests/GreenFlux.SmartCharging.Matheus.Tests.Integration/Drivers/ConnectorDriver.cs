using FluentAssertions;
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
        private Func<string, string, string> ConnectorBaseUrl = (groupId, chargeStationId) => Routes.GroupsRoute + "/" + groupId + Routes.ChargeStationBaseRoute + "/" + chargeStationId + Routes.ConnectorsBaseRoute;
        private Func<string, string, string, string> ConnectorUrl = (groupId, chargeStationId, connectorId) => Routes.GroupsRoute + "/" + groupId + Routes.ChargeStationBaseRoute + "/" + chargeStationId + Routes.ConnectorsBaseRoute + "/" + connectorId;

        ConnectorDriver() : base()
        {            
        }

        public async Task<HttpResponseMessage> GetConnector(Guid groupId, Guid chargeStationId, int connectorId)
        {
            var response = await Client.GetAsync(ConnectorUrl(groupId.ToString(), chargeStationId.ToString(), connectorId.ToString()));

            return response;
        }

        public async Task<HttpResponseMessage> DeleteConnector(Guid groupId, Guid chargeStationId, int connectorId)
        {
            var response = await Client.DeleteAsync(ConnectorUrl(groupId.ToString(), chargeStationId.ToString(), connectorId.ToString()));

            return response;
        }

        public void ShouldNotFindTheConnector(HttpResponseMessage response)
        {
            response.StatusCode.Should().Be(404);
        }

        public async Task ShouldDeleteSuccessfully(HttpResponseMessage response, Guid groupId, Guid chargeStationId, int deletedConnectorId)
        {
            response.StatusCode.Should().Be(204);
            var getConnectorResponse = await this.GetConnector(groupId, chargeStationId, deletedConnectorId);
            getConnectorResponse.StatusCode.Should().Be(404);
        }

    }
}
