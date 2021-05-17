using TechTalk.SpecFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Matheus.Tests.Integration.Drivers;
using GreenFlux.SmartCharging.Matheus.Domain.Models;
using TechTalk.SpecFlow.Assist;
using GreenFlux.SmartCharging.Matheus.API.Resources;
using FluentAssertions;
using System.Net.Http;

namespace GreenFlux.SmartCharging.Matheus.Tests.Integration.Steps
{
    [Binding]
    public sealed class ChargeStationSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly GroupDriver _groupDriver;
        private readonly ChargeStationDriver _chargeStationDriver;
        private readonly GroupResource _group;
        private readonly ChargeStationResource _chargeStation;
        private readonly SaveChargeStationResource _createChargeStation;
        public ChargeStationSteps(ScenarioContext scenarioContext, GroupDriver groupDriver,
            ChargeStationDriver chargeStationDriver)
        {
            _scenarioContext = scenarioContext;
            _groupDriver = groupDriver;
            _chargeStationDriver = chargeStationDriver;
            _group = new GroupResource();
            _chargeStation = new ChargeStationResource();
            _createChargeStation = new SaveChargeStationResource();
        }


        [Given("a charge station name of (.*)")]
        public void GivenAnChargeStationName(string name)
        {
            _createChargeStation.Name = name;
        }

        [Given("a specific set of connectors")]
        public void GivenASpecificSetOfConnectors(Table connectorsTable)
        {
            ICollection<SaveConnectorResource> connectors = (ICollection<SaveConnectorResource>)connectorsTable.CreateSet<SaveConnectorResource>();
            _createChargeStation.Connectors = connectors;
        }

        [When("the Charge Station is created")]
        public async Task WhenTheChargeStationIsCreated()
        {
            _scenarioContext["createdGroupResponse"].Should().NotBeNull();
            GroupResource groupResponse = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);

            _scenarioContext["createdChargeStation"] = await _chargeStationDriver.CreateChargeStation(groupResponse.Id, _createChargeStation.Name, _createChargeStation.Connectors);
        }

        [When("the Charge Station is created for the wrong group")]
        public async Task WhenTheChargeStationIsCreatedForTheWrongGroup()
        {
            _scenarioContext["createdChargeStation"] = await _chargeStationDriver.CreateChargeStation(new Guid(), _createChargeStation.Name, _createChargeStation.Connectors);
        }

        [When("the Charge Station is updated")]
        public async Task WhenTheChargeStationIsUpdated()
        {
            _scenarioContext["createdGroupResponse"].Should().NotBeNull();
            _scenarioContext["createdChargeStation"].Should().NotBeNull();
            GroupResource groupResponse = await _groupDriver.ParseFromResponse< GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
            ChargeStationResource chargeStation = await _chargeStationDriver.ParseFromResponse<ChargeStationResource>((HttpResponseMessage)_scenarioContext["createdChargeStation"]);

            _scenarioContext["updatedChargeStation"] = await _chargeStationDriver.UpdateChargeStation(groupResponse.Id,chargeStation.Id, _createChargeStation.Name);
        }

        [When("the wrong Charge Station is updated")]
        public async Task WhenTheWrongChargeStationIsUpdated()
        {
            _scenarioContext["createdGroupResponse"].Should().NotBeNull();
            _scenarioContext["createdChargeStation"].Should().NotBeNull();
            GroupResource groupResponse = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
            ChargeStationResource chargeStation = await _chargeStationDriver.ParseFromResponse<ChargeStationResource>((HttpResponseMessage)_scenarioContext["createdChargeStation"]);

            _scenarioContext["updatedChargeStation"] = await _chargeStationDriver.UpdateChargeStation(groupResponse.Id, new Guid(), _createChargeStation.Name);
        }

        [When("the charge station is deleted")]
        public async Task WhenTheChargeStationIsDeleted()
        {
            GroupResource groupResource = await _groupDriver.ParseFromResponse<GroupResource>((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
            ChargeStationResource chargeStationResource = await _chargeStationDriver.ParseFromResponse<ChargeStationResource>((HttpResponseMessage)_scenarioContext["createdChargeStation"]);

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
            await _chargeStationDriver.ShouldCreateChargeStationSuccessfully((HttpResponseMessage)_scenarioContext["createdChargeStation"]);
        }

        [Then("the Charge Station should be updated successfully")]
        public async Task ThenTheChargeStationShouldBeUpdatedSuccessfully()
        {
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
            _chargeStationDriver.ShouldNotCreateChargeStationSuccessfully((HttpResponseMessage)_scenarioContext["createdChargeStation"]);
        }

        [Then("should not find the group")]
        public async Task ThenShouldNotFindTheGroup()
        {
            await _groupDriver.ShouldNotFindTheGroup((HttpResponseMessage)_scenarioContext["createdChargeStation"]);
        }
    }
}
