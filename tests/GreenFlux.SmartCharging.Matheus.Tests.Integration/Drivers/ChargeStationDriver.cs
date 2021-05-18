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
    public class ChargeStationDriver :BaseDriver
    {
        private Func<string,string> ChargeStationBaseUrl = groupId => Routes.GroupsRoute + "/" + groupId + Routes.ChargeStationBaseRoute;
        private Func<string, string, string> ChargeStationUrl = (groupId, chargeStationId) => Routes.GroupsRoute + "/" + groupId + Routes.ChargeStationBaseRoute + "/" + chargeStationId;
        ChargeStationDriver() : base()
        {            
        }

        public async Task<HttpResponseMessage> GetChargeStation(Guid groupId, Guid chargeStationId)
        {
            var response = await Client.GetAsync(ChargeStationUrl(groupId.ToString(), chargeStationId.ToString()));

            return response;
        }

        public async Task<HttpResponseMessage> GetAll(Guid groupId)
        {
            var response = await Client.GetAsync(ChargeStationBaseUrl(groupId.ToString()));

            return response;
        }

        public async Task<HttpResponseMessage> CreateChargeStation(Guid groupId, string name, ICollection<SaveConnectorResource> connectors)
        {
            SaveChargeStationResource saveChargeStationResource = new SaveChargeStationResource()
            {
                Name = name,
                Connectors = connectors
            };

            return await Client.PostAsync(ChargeStationBaseUrl(groupId.ToString()), ConvertToJsonData<SaveChargeStationResource>(saveChargeStationResource));
        }

        public async Task<HttpResponseMessage> UpdateChargeStation(Guid groupId, Guid chargeStationId, string name)
        {            
            PatchChargeStationResource patchChargeStationResource = new PatchChargeStationResource()
            {
                Name = name
            };

            return await Client.PatchAsync(ChargeStationUrl(groupId.ToString(), chargeStationId.ToString()), ConvertToJsonData<PatchChargeStationResource>(patchChargeStationResource));
        }

        public async Task<HttpResponseMessage> DeleteChargeStation(Guid groupId, Guid chargeStationId)
        {
            var response = await Client.DeleteAsync(ChargeStationUrl(groupId.ToString(), chargeStationId.ToString()));

            return response;
        }

        public async Task ShouldDeleteSuccessfully(HttpResponseMessage response,Guid groupId, Guid deletedChargeStationId)
        {
            response.StatusCode.Should().Be(204);
            var getChargeStationResponse = await this.GetChargeStation(groupId, deletedChargeStationId);
            getChargeStationResponse.StatusCode.Should().Be(404);
        }

        public async Task ShouldUpdateChargeStationSuccessfully(HttpResponseMessage response, string expectedName)
        {
            response.StatusCode.Should().Be(200);

            ChargeStationResource chargeStationResponse = await this.ParseFromResponse<ChargeStationResource>(response);
            if (!string.IsNullOrEmpty(expectedName))
                chargeStationResponse.Name.Should().Be(expectedName);
        }

        public void ShouldNotUpdateChargeStationSuccessfully(HttpResponseMessage response)
        {
            response.StatusCode.Should().Match<int>(c => c == 404 || c == 400 || c == 500);
        }
        public async Task ShouldCreateChargeStationSuccessfully(HttpResponseMessage response)
        {
            response.StatusCode.Should().Be(201);

            ChargeStationResource chargeStationResponse = await this.ParseFromResponse<ChargeStationResource>(response);
            chargeStationResponse.Id.Should().NotBeEmpty();
            chargeStationResponse.Name.Should().NotBeEmpty();
        }

        public void ShouldNotCreateChargeStationSuccessfully(HttpResponseMessage response)
        {
            response.StatusCode.Should().Match<int>(c => c == 404 || c == 400 || c == 500);
        }

        public async Task ShouldNotExistAnymore(Guid groupId, Guid chargeStationId)
        {
            HttpResponseMessage chargeStationResponse = await GetChargeStation(groupId, chargeStationId);
            chargeStationResponse.StatusCode.Should().Be(404);
        }
    }
}
