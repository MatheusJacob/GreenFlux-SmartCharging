using FluentAssertions;
using GreenFlux.SmartCharging.Matheus.API.Resources;
using GreenFlux.SmartCharging.Matheus.Tests.Integration.Drivers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace GreenFlux.SmartCharging.Matheus.Tests.Integration.Steps
{
    [Binding]
    public sealed class ChargeStationSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly GroupDriver _groupDriver;
        private readonly ChargeStationDriver _chargeStationDriver;
        private readonly ConnectorDriver _connectorDriver;

        public ChargeStationSteps(ScenarioContext scenarioContext, GroupDriver groupDriver,
            ChargeStationDriver chargeStationDriver, ConnectorDriver connectorDriver)
        {
            _scenarioContext = scenarioContext;
            _scenarioContext["createChargeStation"] = new SaveChargeStationResource();
            _scenarioContext["chargeStationListResponses"] = new List<HttpResponseMessage>();
            _groupDriver = groupDriver;
            _chargeStationDriver = chargeStationDriver;
            _connectorDriver = connectorDriver;
        }


        [Given("a charge station name of (.*)")]
        public void GivenAnChargeStationName(string name)
        {
            ((SaveChargeStationResource)_scenarioContext["createChargeStation"]).Name = name;
        }

        [Given("a specific set of connectors")]
        public void GivenASpecificSetOfConnectors(Table connectorsTable)
        {
            ICollection<SaveConnectorResource> connectors = (ICollection<SaveConnectorResource>)connectorsTable.CreateSet<SaveConnectorResource>();
            ((SaveChargeStationResource)_scenarioContext["createChargeStation"]).Connectors = connectors;
        }

        [Given("a specific set of Charge Stations")]
        public void GivenASpecificSetOfChargeStations(Table table)
        {
            List<SaveChargeStationResource> listChargeStations = new List<SaveChargeStationResource>();
            foreach (var tableRow in table.Rows)
            {
                List<string> connectorIds = new List<string>(tableRow["connectors"].Split(','));
                List<SaveConnectorResource> connectors = new List<SaveConnectorResource>();
                foreach (var item in connectorIds)
                {
                    float maxCurrent;
                    float.TryParse(item, out maxCurrent).Should().BeTrue();

                    SaveConnectorResource saveConnector = new SaveConnectorResource()
                    {
                        MaxCurrentAmp = maxCurrent
                    };
                    connectors.Add(saveConnector);
                }

                SaveChargeStationResource saveChargeStation = new SaveChargeStationResource()
                {
                    Name = tableRow["name"],
                    Connectors = connectors
                };

                listChargeStations.Add(saveChargeStation);
            }

            _scenarioContext["chargeStationList"] = listChargeStations;
        }

        [When("create all Charge Stations")]
        public async Task WhenCreateAllChargeStations()
        {
            _scenarioContext.Should().ContainKey("chargeStationList");
            _scenarioContext.Should().ContainKey("createdGroupResponse");

            GroupResource groupResponse = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);

            List<SaveChargeStationResource> chargeStationToCreate = (List<SaveChargeStationResource>)_scenarioContext["chargeStationList"];

            foreach (var item in chargeStationToCreate)
            {
                var response = await _chargeStationDriver.CreateChargeStation(groupResponse.Id, item.Name, item.Connectors);
                ((List<HttpResponseMessage>)_scenarioContext["chargeStationListResponses"]).Add(response);
                _scenarioContext["createdChargeStationResponse"] = response;
            }
        }

        [When("the Charge Station is created")]
        public async Task WhenTheChargeStationIsCreated()
        {
            _scenarioContext["createdGroupResponse"].Should().NotBeNull();
            GroupResource groupResponse = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
            SaveChargeStationResource _createChargeStation = ((SaveChargeStationResource)_scenarioContext["createChargeStation"]);
            _scenarioContext["createdChargeStationResponse"] = await _chargeStationDriver.CreateChargeStation(groupResponse.Id, _createChargeStation.Name, _createChargeStation.Connectors);
        }

        [When("the Charge Station is created for the wrong group")]
        public async Task WhenTheChargeStationIsCreatedForTheWrongGroup()
        {
            SaveChargeStationResource _createChargeStation = ((SaveChargeStationResource)_scenarioContext["createChargeStation"]);
            _scenarioContext["createdChargeStationResponse"] = await _chargeStationDriver.CreateChargeStation(new Guid(), _createChargeStation.Name, _createChargeStation.Connectors);
        }

        [When("the Charge Station is updated")]
        public async Task WhenTheChargeStationIsUpdated()
        {
            _scenarioContext["createdGroupResponse"].Should().NotBeNull();
            _scenarioContext["createdChargeStationResponse"].Should().NotBeNull();
            GroupResource groupResponse = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
            ChargeStationResource chargeStation = await _chargeStationDriver.ParseFromResponse<ChargeStationResource>((HttpResponseMessage)_scenarioContext["createdChargeStationResponse"]);
            SaveChargeStationResource _createChargeStation = ((SaveChargeStationResource)_scenarioContext["createChargeStation"]);
            _scenarioContext["updatedChargeStation"] = await _chargeStationDriver.UpdateChargeStation(groupResponse.Id, chargeStation.Id, _createChargeStation.Name);
        }

        [When("the wrong Charge Station is updated")]
        public async Task WhenTheWrongChargeStationIsUpdated()
        {
            _scenarioContext["createdGroupResponse"].Should().NotBeNull();
            _scenarioContext["createdChargeStationResponse"].Should().NotBeNull();
            GroupResource groupResponse = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
            ChargeStationResource chargeStation = await _chargeStationDriver.ParseFromResponse<ChargeStationResource>((HttpResponseMessage)_scenarioContext["createdChargeStationResponse"]);
            SaveChargeStationResource _createChargeStation = ((SaveChargeStationResource)_scenarioContext["createChargeStation"]);
            _scenarioContext["updatedChargeStation"] = await _chargeStationDriver.UpdateChargeStation(groupResponse.Id, new Guid(), _createChargeStation.Name);
        }

        [When("the charge station is deleted")]
        public async Task WhenTheChargeStationIsDeleted()
        {
            GroupResource groupResource = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
            ChargeStationResource chargeStationResource = await _chargeStationDriver.ParseFromResponse<ChargeStationResource>((HttpResponseMessage)_scenarioContext["createdChargeStationResponse"]);

            chargeStationResource.Id.Should().NotBeEmpty();
            chargeStationResource.Name.Should().NotBeEmpty();

            _scenarioContext["deletedChargeStationId"] = chargeStationResource.Id;
            _scenarioContext["deletedChargeStationResponse"] = await _chargeStationDriver.DeleteChargeStation(groupResource.Id, chargeStationResource.Id);
        }

        [When("the wrong Charge Station is deleted")]
        public async Task WhenTheWrongChargeStationIsDeleted()
        {
            GroupResource groupResource = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);

            Guid wrongChargeStationId = new Guid();
            _scenarioContext["deletedChargeStationId"] = wrongChargeStationId;
            _scenarioContext["deletedChargeStationResponse"] = await _chargeStationDriver.DeleteChargeStation(groupResource.Id, wrongChargeStationId);
        }

        [When("listing all charge stations from a group")]
        public async Task WhenListingAllChargeStationsFromAGroup()
        {
            GroupResource groupResource = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);

            _scenarioContext["allChargeStationsResponse"] = await _chargeStationDriver.GetAll(groupResource.Id);
        }
        [Then("the created Charge Station should not exist anymore")]
        public async Task ThenTheCreatedChargeStationShouldNotExistAnymore()
        {
            GroupResource group = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
            ChargeStationResource chargeStation = await _chargeStationDriver.ParseFromResponse<ChargeStationResource>((HttpResponseMessage)_scenarioContext["createdChargeStationResponse"]);

            await _chargeStationDriver.ShouldNotExistAnymore(group.Id, chargeStation.Id);
        }

        [Then("the Charge Station should not exist anymore")]
        public async Task ThenTheChargeStationShouldNotExistAnymore()
        {
            GroupResource group = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);

            await _chargeStationDriver.ShouldDeleteSuccessfully((HttpResponseMessage)_scenarioContext["deletedChargeStationResponse"],
                group.Id,
                (Guid)_scenarioContext["deletedChargeStationId"]);
        }

        [Then("no Charge Station was deleted")]
        public void ThenNoChargeStationShouldBeDeleted()
        {
            ((HttpResponseMessage)_scenarioContext["deletedChargeStationResponse"]).StatusCode.Should().Be(404);
        }

        [Then("the Charge Station should be created successfully")]
        public async Task ThenTheChargeStationShouldBeCreatedSuccessfully()
        {
            await _chargeStationDriver.ShouldCreateChargeStationSuccessfully((HttpResponseMessage)_scenarioContext["createdChargeStationResponse"]);
        }

        [Then("the Charge Station should be updated successfully")]
        public async Task ThenTheChargeStationShouldBeUpdatedSuccessfully()
        {
            SaveChargeStationResource _createChargeStation = ((SaveChargeStationResource)_scenarioContext["createChargeStation"]);
            await _chargeStationDriver.ShouldUpdateChargeStationSuccessfully((HttpResponseMessage)_scenarioContext["updatedChargeStation"], _createChargeStation.Name);
        }

        [Then("the Charge Station should not be updated successfully")]
        public void ThenTheChargeStationShouldNotBeUpdatedSuccessfully()
        {
            _chargeStationDriver.ShouldNotUpdateChargeStationSuccessfully((HttpResponseMessage)_scenarioContext["updatedChargeStation"]);
        }
        [Then("the Charge Station should not be created successfully")]
        public void ThenTheChargeStationShouldNotBeCreatedSuccessfully()
        {
            _chargeStationDriver.ShouldNotCreateChargeStationSuccessfully((HttpResponseMessage)_scenarioContext["createdChargeStationResponse"]);
        }

        [Then("should not find the group")]
        public void ThenShouldNotFindTheGroup()
        {
            _groupDriver.ShouldNotFindTheGroup((HttpResponseMessage)_scenarioContext["createdChargeStationResponse"]);
        }

        [Then("Should have (.*) charge stations")]
        public async Task ThenShouldHaveNChargeStations(int expectedCount)
        {
            _scenarioContext.Should().ContainKey("allChargeStationsResponse");

            List<ChargeStationResource> listChargeStations = await _chargeStationDriver.ParseFromResponse<List<ChargeStationResource>>((HttpResponseMessage)_scenarioContext["allChargeStationsResponse"]);
            listChargeStations.Count.Should().Be(expectedCount);

        }

        [Then("Should create all charge stations successfully")]
        public void ThenShouldCreateAllChargeStationsSuccessfully()
        {
            List<HttpResponseMessage> responses = (List<HttpResponseMessage>)_scenarioContext["chargeStationListResponses"];

            foreach (var response in responses)
            {
                response.StatusCode.Should().Be(201);
            }

        }

        [Then(@"Should create successfully (.*) connectors with (.*) max current for all charge stations")]
        public async Task ThenShouldCreateSuccessfullyConnectorsForAllChargeStations(int connectorsCount, float maxCurrent)
        {
            List<HttpResponseMessage> responses = (List<HttpResponseMessage>)_scenarioContext["chargeStationListResponses"];
            GroupResource groupResponse = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);

            foreach (var response in responses)
            {
                ChargeStationResource chargeStationResource = await _chargeStationDriver.ParseFromResponse<ChargeStationResource>(response);

                for (int i = 0; i < connectorsCount; i++)
                {
                    var connectorResponse = await _connectorDriver.CreateConnector(groupResponse.Id, chargeStationResource.Id, maxCurrent);
                    connectorResponse.StatusCode.Should().Be(201);
                }
            }
        }

        [Then(@"Should update successfully all connectors to (.*) max current for all charge stations")]
        public async Task ThenShouldUpdateSuccessfullyAllConnectorsToMaxCurrentForAllChargeStations(float maxCurrent)
        {
            List<HttpResponseMessage> responses = (List<HttpResponseMessage>)_scenarioContext["chargeStationListResponses"];
            GroupResource groupResponse = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);

            foreach (var response in responses)
            {
                ChargeStationResource chargeStationResource = await _chargeStationDriver.ParseFromResponse<ChargeStationResource>(response);

                var connectorResponse = await _connectorDriver.UpdateConnector(groupResponse.Id, chargeStationResource.Id, 1, maxCurrent);
                connectorResponse.StatusCode.Should().Be(200);
            }
        }

    }
}
