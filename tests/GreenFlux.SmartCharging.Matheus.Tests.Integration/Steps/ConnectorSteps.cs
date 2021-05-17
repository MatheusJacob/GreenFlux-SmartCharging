using GreenFlux.SmartCharging.Matheus.Domain.Models;
using GreenFlux.SmartCharging.Matheus.Tests.Integration.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;
using FluentAssertions;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Matheus.API.Resources;

namespace GreenFlux.SmartCharging.Matheus.Tests.Integration.Steps
{
    [Binding]
    public sealed class ConnectorSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly GroupDriver _groupDriver;
        private readonly ConnectorDriver _connectorDriver;
        private readonly ChargeStationDriver _chargeStationDriver;
        
        public ConnectorSteps(ScenarioContext scenarioContext, ConnectorDriver connectorDriver, GroupDriver groupDriver,
            ChargeStationDriver chargeStationDriver)
        {
            _scenarioContext = scenarioContext;
            _groupDriver = groupDriver;
            _chargeStationDriver = chargeStationDriver;
            _connectorDriver = connectorDriver;
        }


        [When("the connector with id (.*) is deleted")]
        public async Task WhenTheConnectorIsDeleted(int connectorId)
        {
            GroupResource groupResource = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
            ChargeStationResource chargeStationResource = await _chargeStationDriver.ParseFromResponse<ChargeStationResource>((HttpResponseMessage)_scenarioContext["createdChargeStation"]);

            chargeStationResource.Id.Should().NotBeEmpty();
            chargeStationResource.Name.Should().NotBeEmpty();

            _scenarioContext["deletedConnectorId"] = connectorId;
            _scenarioContext["deletedConnectorResponse"] = await _connectorDriver.DeleteConnector(groupResource.Id, chargeStationResource.Id, connectorId);
        }

        [Then("the connector should be deleted successfully")]
        public async Task ThenTheConnectorShouldBeDeletedSuccessfully()
        {
            GroupResource group = await _connectorDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
            ChargeStationResource chargeStation = await _connectorDriver.ParseFromResponse<ChargeStationResource>((HttpResponseMessage)_scenarioContext["createdChargeStation"]);

            await _connectorDriver.ShouldDeleteSuccessfully((HttpResponseMessage)_scenarioContext["deletedConnectorResponse"],
                group.Id,
                chargeStation.Id,
                (int)_scenarioContext["deletedConnectorId"]);
        }

        [Then("the connector should not be deleted successfully")]
        public void ThenTheConnectorShouldNotBeDeletedSuccessfully()
        {
            ((HttpResponseMessage)_scenarioContext["deletedConnectorResponse"]).StatusCode.Should().Be(404);
        }
    }
}
