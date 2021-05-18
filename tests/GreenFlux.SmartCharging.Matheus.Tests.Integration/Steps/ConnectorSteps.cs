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
            _scenarioContext["createConnector"] = new SaveConnectorResource();

            _scenarioContext["grops"] = new List<HttpResponseMessage>();
            _scenarioContext["chargeStations"] = new List<HttpResponseMessage>();
            _scenarioContext["connectors"] = new List<HttpResponseMessage>();
            _groupDriver = groupDriver;
            _chargeStationDriver = chargeStationDriver;
            _connectorDriver = connectorDriver;
        }

        [Given("a connector with a max current of (.*)")]
        public void GivenAConnectorWithAMaxCurrentOf(float maxCurrentAmp)
        {
            ((SaveConnectorResource)_scenarioContext["createConnector"]).MaxCurrentAmp = maxCurrentAmp;
        }

        [Given("the wrong charge station is provided")]
        public void WhenTheWrongChargeStationIsProvided()
        {
            _scenarioContext["createdChargeStation"] = new ChargeStationResource() { Id = new Guid() };
        }

        [When("the connectors are created sequencially")]
        public async Task WhenTheConnectorsAreCreatedSequencially()
        {
            _scenarioContext.Should().ContainKey("createChargeStation");
            _scenarioContext["createdChargeStationResponse"].Should().NotBeNull();
            ((SaveChargeStationResource)_scenarioContext["createChargeStation"]).Connectors.Should().NotBeEmpty();
            List<SaveConnectorResource> saveConnectors = (List<SaveConnectorResource>)((SaveChargeStationResource)_scenarioContext["createChargeStation"]).Connectors;

            foreach (var saveConnector in saveConnectors)
            {
                _scenarioContext["createConnector"] = saveConnector;
                await WhenTheConnectorIsCreated();
                _scenarioContext.Should().ContainKey("createdConnector");
                _scenarioContext["createdConnector"].Should().NotBeNull();

                HttpResponseMessage connectorResponse = (HttpResponseMessage)_scenarioContext["createdConnector"];
                ((List<HttpResponseMessage>)_scenarioContext["connectors"]).Add(connectorResponse);
            }
        }

        [When("the connector is created")]
        public async Task WhenTheConnectorIsCreated()
        {
            _scenarioContext.Should().ContainKey("createdChargeStationResponse");
            _scenarioContext.Should().ContainKey("createdGroupResponse");
            _scenarioContext["createdGroupResponse"].Should().NotBeNull();
            _scenarioContext["createdChargeStationResponse"].Should().NotBeNull();

            GroupResource groupResponse = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
            ChargeStationResource chargeStationResource = _scenarioContext.ContainsKey("createdChargeStation") ? (ChargeStationResource)_scenarioContext["createdChargeStation"] 
                : await _connectorDriver.ParseFromResponse<ChargeStationResource>((HttpResponseMessage)_scenarioContext["createdChargeStationResponse"]);

            SaveConnectorResource createConnector = ((SaveConnectorResource)_scenarioContext["createConnector"]);
            _scenarioContext["createdConnector"] = await _connectorDriver.CreateConnector(groupResponse.Id, chargeStationResource.Id, createConnector.MaxCurrentAmp.Value);
        }

        [When("the connector is created with required parameters missing")]
        public async Task WhenTheConnectorIsCreatedWithRequiredParametersMissing()
        {
            _scenarioContext.Should().ContainKey("createdChargeStationResponse");
            _scenarioContext.Should().ContainKey("createdGroupResponse");
            _scenarioContext["createdGroupResponse"].Should().NotBeNull();
            _scenarioContext["createdChargeStationResponse"].Should().NotBeNull();

            GroupResource groupResponse = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
            ChargeStationResource chargeStationResource = await _groupDriver.ParseFromResponse<ChargeStationResource>((HttpResponseMessage)_scenarioContext["createdChargeStationResponse"]);

            SaveConnectorResource createConnector = ((SaveConnectorResource)_scenarioContext["createConnector"]);
            _scenarioContext["createdConnector"] = await _connectorDriver.CreateConnectorWithEmptyPayload(groupResponse.Id, chargeStationResource.Id);
        }

        [When("the connector with id (.*) is deleted")]
        public async Task WhenTheConnectorIsDeleted(int connectorId)
        {
            GroupResource groupResource = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
            ChargeStationResource chargeStationResource = await _chargeStationDriver.ParseFromResponse<ChargeStationResource>((HttpResponseMessage)_scenarioContext["createdChargeStationResponse"]);

            chargeStationResource.Id.Should().NotBeEmpty();
            chargeStationResource.Name.Should().NotBeEmpty();

            _scenarioContext["deletedConnectorId"] = connectorId;
            _scenarioContext["deletedConnectorResponse"] = await _connectorDriver.DeleteConnector(groupResource.Id, chargeStationResource.Id, connectorId);
        }

        [Then("the expected results should be")]
        public async Task TheExpectedResultsShouldBe(Table expectedResults)
        {
            _scenarioContext.Should().ContainKey("connectors");
            _scenarioContext["connectors"].Should().NotBeNull();
            List<HttpResponseMessage> connectorsResponse = ((List<HttpResponseMessage>)_scenarioContext["connectors"]);
            connectorsResponse.Count().Should().BePositive();
            connectorsResponse.Count().Should().Be(expectedResults.Rows.Count);

            for (int i = 0; i < expectedResults.RowCount; i++)
            {
                TableRow row = expectedResults.Rows[i];
                int expectedConnectorId;
                int.TryParse(row["expectedConnectorId"], out expectedConnectorId).Should().BeTrue();

                _scenarioContext["createdConnector"] = connectorsResponse[i];
                await ThenTheExpectedResultShouldBe(row["created"]);
                if(row["created"] == "true")
                    await ThenTheExpectedConnectorIdShouldBe(expectedConnectorId);              
            }
        }

        [Then("the Connector should be created successfully")]
        public async Task ThenTheConnectorShouldBeCreatedSuccessfully()
        {
            await _connectorDriver.ShouldCreateConnectorSuccessfully((HttpResponseMessage)_scenarioContext["createdConnector"]);
        }

        [Then("the Connector should not be created successfully")]
        public void ThenTheConnectorShouldNotBeCreatedSuccessfully()
        {
            _connectorDriver.ShouldNotCreateConnectorSuccessfully((HttpResponseMessage)_scenarioContext["createdConnector"]);
        }

        [Then("the connector should be deleted successfully")]
        public async Task ThenTheConnectorShouldBeDeletedSuccessfully()
        {
            GroupResource group = await _connectorDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
            ChargeStationResource chargeStation = await _connectorDriver.ParseFromResponse<ChargeStationResource>((HttpResponseMessage)_scenarioContext["createdChargeStationResponse"]);

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

        [Then("should not find the charge station")]
        public void ThenShouldNotFindTheChargeStation()
        {
            ((HttpResponseMessage)_scenarioContext["createdConnector"]).StatusCode.Should().Be(404);
        }

        [Then(@"the expected result should be (.*)")]
        public async Task ThenTheExpectedResultShouldBe(string expectedResult)
        {
            if (expectedResult.ToLower() == "true")
                await ThenTheConnectorShouldBeCreatedSuccessfully();
            else
                ThenTheConnectorShouldNotBeCreatedSuccessfully();
        }

        [Then(@"the expected connector Id should be (.*)")]
        public async Task ThenTheExpectedConnectorIdShouldBe(int connectorId)
        {
            ConnectorResource createdConnector = await _connectorDriver.ParseFromResponse<ConnectorResource>((HttpResponseMessage)_scenarioContext["createdConnector"]);
            createdConnector.Id.Should().Be(connectorId);
        }


    }
}
