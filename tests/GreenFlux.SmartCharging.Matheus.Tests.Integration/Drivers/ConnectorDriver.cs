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

        public async Task<HttpResponseMessage> CreateConnector(Guid groupId, Guid chargeStationId, float maxCurrentAmp)
        {
            SaveConnectorResource saveConnectorResource = new SaveConnectorResource()
            {
                MaxCurrentAmp = maxCurrentAmp
            };

            return await Client.PostAsync(ConnectorBaseUrl(groupId.ToString(), chargeStationId.ToString()), ConvertToJsonData<SaveConnectorResource>(saveConnectorResource));
        }
        public async Task<HttpResponseMessage> CreateConnectorWithEmptyPayload(Guid groupId, Guid chargeStationId)
        {     
            return await Client.PostAsync(ConnectorBaseUrl(groupId.ToString(), chargeStationId.ToString()), new StringContent("", Encoding.UTF8, "application/json"));
        }

        public async Task<HttpResponseMessage> UpdateConnector(Guid groupId, Guid chargeStationId,int connectorId, float maxCurrentAmp)
        {
            PatchConnectorResource patchConnectorResource = new PatchConnectorResource()
            {
                MaxCurrentAmp = maxCurrentAmp
            };

            return await Client.PatchAsync(ConnectorUrl(groupId.ToString(), chargeStationId.ToString(), connectorId.ToString()), ConvertToJsonData<PatchConnectorResource>(patchConnectorResource));
        }

        public async Task ShouldUpdateConnectorSuccessfully(HttpResponseMessage response, PatchConnectorResource expectedValue)
        {
            response.StatusCode.Should().Be(200);

            ConnectorResource connectorResponse = await this.ParseFromResponse<ConnectorResource>(response);

            if (expectedValue.MaxCurrentAmp.HasValue)
                connectorResponse.MaxCurrentAmp.Value.Should().Be(expectedValue.MaxCurrentAmp.Value);

            connectorResponse.Id.Should().BeGreaterOrEqualTo(1);
            connectorResponse.Id.Should().BeLessOrEqualTo(5);
            connectorResponse.MaxCurrentAmp.Should().BePositive();
        }

        public void ShouldNotUpdateConnectorSuccessfully(HttpResponseMessage response)
        {
            response.StatusCode.Should().Match<int>(c => c == 404 || c == 400 || c == 500);
        }


        public async Task<HttpResponseMessage> DeleteConnector(Guid groupId, Guid chargeStationId, int connectorId)
        {
            var response = await Client.DeleteAsync(ConnectorUrl(groupId.ToString(), chargeStationId.ToString(), connectorId.ToString()));

            return response;
        }

        public async Task ShouldCreateConnectorSuccessfully(HttpResponseMessage response)
        {
            response.StatusCode.Should().Be(201);

            ConnectorResource connectorResponse = await this.ParseFromResponse<ConnectorResource>(response);
            connectorResponse.Id.Should().BeGreaterOrEqualTo(1);
            connectorResponse.Id.Should().BeLessOrEqualTo(5);
            connectorResponse.MaxCurrentAmp.Should().BePositive();
        }

        public void ShouldNotCreateConnectorSuccessfully(HttpResponseMessage response)
        {
            response.StatusCode.Should().Match<int>(c => c == 404 || c == 400 || c == 500);
        }

        public async Task ShouldNotExistAnymore(Guid groupId, Guid chargeStationId, int connectorId)
        {
            HttpResponseMessage connectorResponse = await GetConnector(groupId, chargeStationId, connectorId);
            connectorResponse.StatusCode.Should().Be(404);
        }

        public void ShouldNotFindTheConnector(HttpResponseMessage response)
        {
            response.StatusCode.Should().Be(404);
        }

        public async Task ShouldDeleteSuccessfully(HttpResponseMessage response)
        {
            response.StatusCode.Should().Be(204);            
        }

    }
}
