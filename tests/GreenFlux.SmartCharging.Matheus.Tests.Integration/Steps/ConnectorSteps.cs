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

            _scenarioContext["groups"] = new List<HttpResponseMessage>();
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
        public void GivenTheWrongChargeStationIsProvided()
        {
            _scenarioContext["createdChargeStation"] = new ChargeStationResource() { Id = new Guid() };
        }

        [When("a specific set of actions is executed sequencially")]
        public async Task WhenASpecificSetOfActionsIsExecutedSequencially(Table setOfActions)
        {
            _scenarioContext["actions"] = setOfActions;
            _scenarioContext["createdChargeStationResponse"].Should().NotBeNull();
            ((SaveChargeStationResource)_scenarioContext["createChargeStation"]).Connectors.Should().NotBeEmpty();

            foreach (var action in setOfActions.Rows)
            {
                if(action["action"] == "create")
                {
                    float maxCurrentAmp;
                    SaveConnectorResource saveConnector = new SaveConnectorResource();
                    float.TryParse(action["maxCurrentAmp"], out maxCurrentAmp).Should().BeTrue();
                    saveConnector.MaxCurrentAmp = maxCurrentAmp;
                    _scenarioContext["createConnector"] = saveConnector;
                    if (setOfActions.Header.Contains("chargeStationId"))
                    {
                        int chargeStationIndex;
                        int.TryParse(action["chargeStationId"], out chargeStationIndex).Should().BeTrue();

                        _scenarioContext["createdChargeStation"] = await _connectorDriver.ParseFromResponse<ChargeStationResource>(((List<HttpResponseMessage>)_scenarioContext["chargeStations"])[chargeStationIndex]);
                    }
                    if (setOfActions.Header.Contains("groupId"))
                    {
                        int groupIndex;
                        int.TryParse(action["groupId"], out groupIndex).Should().BeTrue();

                        _scenarioContext["createdGroupResponse"] = ((List<HttpResponseMessage>)_scenarioContext["groups"])[groupIndex];
                    }
                    await WhenTheConnectorIsCreated();
                    ((List<HttpResponseMessage>)_scenarioContext["connectors"]).Add((HttpResponseMessage)_scenarioContext["createdConnector"]);
                }
                else
                {
                    int connectorId;
                    int.TryParse(action["expectedConnectorId"], out connectorId).Should().BeTrue();
                    await WhenTheConnectorIsDeleted(connectorId);
                    ((List<HttpResponseMessage>)_scenarioContext["connectors"]).Add((HttpResponseMessage)_scenarioContext["deletedConnectorResponse"]);
                }               
            }
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

        [When("a connector with id (.*) is updated")]
        public async Task WhenAConnectorIsUpdated(int connectorId)
        {
            _scenarioContext.Should().ContainKey("createdChargeStationResponse");
            _scenarioContext.Should().ContainKey("createdGroupResponse");
            _scenarioContext["createdGroupResponse"].Should().NotBeNull();
            _scenarioContext["createdChargeStationResponse"].Should().NotBeNull();


            GroupResource groupResponse = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
            ChargeStationResource chargeStationResource =  await _connectorDriver.ParseFromResponse<ChargeStationResource>((HttpResponseMessage)_scenarioContext["createdChargeStationResponse"]);
            
            _scenarioContext["updatedConnector"] = await _connectorDriver.UpdateConnector(groupResponse.Id, chargeStationResource.Id, connectorId, ((SaveConnectorResource)_scenarioContext["createConnector"]).MaxCurrentAmp.Value);
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

        [Then(@"the connector should be updated successfully")]
        public async Task ThenTheConnectorShouldBeUpdatedSuccessfully()
        {
            PatchConnectorResource patchConnectorResource = new PatchConnectorResource()
            {
                MaxCurrentAmp = ((SaveConnectorResource)_scenarioContext["createConnector"]).MaxCurrentAmp
            };

            await _connectorDriver.ShouldUpdateConnectorSuccessfully((HttpResponseMessage)_scenarioContext["updatedConnector"], patchConnectorResource);
        }

        [Then(@"the connector should not be updated successfully")]
        public void ThenTheConnectorShoulNotdBeUpdatedSuccessfully()
        {
            PatchConnectorResource patchConnectorResource = new PatchConnectorResource()
            {
                MaxCurrentAmp = ((SaveConnectorResource)_scenarioContext["createConnector"]).MaxCurrentAmp
            };

            _connectorDriver.ShouldNotUpdateConnectorSuccessfully((HttpResponseMessage)_scenarioContext["updatedConnector"]);
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
                _scenarioContext["deletedConnectorResponse"] = connectorsResponse[i];
                _scenarioContext["deletedConnectorId"] = expectedConnectorId;

                await ThenTheExpectedResultShouldBe(row["action"]);
                if(row["action"] == "created")
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
            await _connectorDriver.ShouldDeleteSuccessfully((HttpResponseMessage)_scenarioContext["deletedConnectorResponse"]);
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
            if (expectedResult.ToLower() == "created")
                await ThenTheConnectorShouldBeCreatedSuccessfully();
            if (expectedResult.ToLower() == "failtocreate")
                ThenTheConnectorShouldNotBeCreatedSuccessfully();
            if (expectedResult.ToLower() == "deleted")
                await ThenTheConnectorShouldBeDeletedSuccessfully();
            if (expectedResult.ToLower() == "failtodelete")
                ThenTheConnectorShouldNotBeDeletedSuccessfully();
        }

        [Then(@"the expected connector Id should be (.*)")]
        public async Task ThenTheExpectedConnectorIdShouldBe(int connectorId)
        {
            ConnectorResource createdConnector = await _connectorDriver.ParseFromResponse<ConnectorResource>((HttpResponseMessage)_scenarioContext["createdConnector"]);
            createdConnector.Id.Should().Be(connectorId);
        }

        [Then(@"save the last created charge station id")]
        public void ThenSaveTheLastCreatedChargeStationId()
        {
            _scenarioContext.Should().ContainKey("chargeStations");
            ((List<HttpResponseMessage>)_scenarioContext["chargeStations"]).Add((HttpResponseMessage)_scenarioContext["createdChargeStationResponse"]);

        }

        [Then(@"save the last created Group id")]
        public void ThenSaveTheLastCreatedGroupId()
        {
            _scenarioContext.Should().ContainKey("groups");
            ((List<HttpResponseMessage>)_scenarioContext["groups"]).Add((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
        }

        [Then(@"all actions should be executed successfully")]
        public async Task ThenAllActionsShouldBeExecutedSuccessfully()
        {
            _scenarioContext.Should().ContainKey("connectors");
            _scenarioContext.Should().ContainKey("actions");

            Table actions = ((Table)_scenarioContext["actions"]);
            List<HttpResponseMessage> connectorsCreated = (List<HttpResponseMessage>)_scenarioContext["connectors"];

            actions.Rows.Count.Should().Be(connectorsCreated.Count);

            for (int i = 0; i < actions.Rows.Count; i++)
            {
                connectorsCreated[i].StatusCode.Should().Be(201);
                ConnectorResource connector = await _connectorDriver.ParseFromResponse<ConnectorResource>(connectorsCreated[i]);

                TableRow row = actions.Rows[i];
                int expectedConnectorId;
                int.TryParse(row["expectedConnectorId"], out expectedConnectorId).Should().BeTrue();
                connector.Id.Should().Be(expectedConnectorId);
            }
        }

        [Then(@"the connector with id (.*) should not exist anymore")]
        public async Task ThenTheConnectorShouldNotExistAnymore(int connectorId)
        {
            GroupResource group = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
            ChargeStationResource chargeStation = await _chargeStationDriver.ParseFromResponse<ChargeStationResource>((HttpResponseMessage)_scenarioContext["createdChargeStationResponse"]);

            await _connectorDriver.ShouldNotExistAnymore(group.Id, chargeStation.Id, connectorId);
        }
    }
}
