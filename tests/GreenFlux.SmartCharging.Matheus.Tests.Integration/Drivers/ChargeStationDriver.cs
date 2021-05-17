﻿using FluentAssertions;
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

        public async Task<HttpResponseMessage> CreateChargeStation(Guid groupId, string name, ICollection<SaveConnectorResource> connectors)
        {
            SaveChargeStationResource saveChargeStationResource = new SaveChargeStationResource()
            {
                Name = name,
                Connectors = connectors
            };

            return await Client.PostAsync(ChargeStationUrl(groupId.ToString()), ConvertToJsonData<SaveChargeStationResource>(saveChargeStationResource));
        }

        public async Task ShouldCreateChargeStationSuccessfully(HttpResponseMessage response)
        {
            response.StatusCode.Should().Be(201);

            ChargeStationResource chargeStationResponse = await this.ParseChargeStationFromResponse(response);
            chargeStationResponse.Id.Should().NotBeEmpty();
            chargeStationResponse.Name.Should().NotBeEmpty();
        }

        public void ShouldNotCreateChargeStationSuccessfully(HttpResponseMessage response)
        {
            response.StatusCode.Should().Match<int>(c => c == 404 || c == 400 || c == 500);
        }

        public async Task<ChargeStationResource> ParseChargeStationFromResponse(HttpResponseMessage response)
        {            
            var content = await response.Content.ReadAsStringAsync();

            Func<ChargeStationResource> chargeStationConverted = () => ConvertToObject<ChargeStationResource>(content);

            chargeStationConverted.Should().NotThrow();

            return chargeStationConverted();            
        }
    }
}
