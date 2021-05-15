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
    public sealed class GroupSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly GroupDriver _groupDriver;
        private GroupResource _group;
        public GroupSteps(ScenarioContext scenarioContext, GroupDriver groupDriver)
        {
            _scenarioContext = scenarioContext;
            _groupDriver = groupDriver;
            _group = new GroupResource();
        }

        [Given("a group name (.*)")]
        public void GivenAGroupName(string name)
        {
            _group.Name = name;           
        }

        [Given("a capacity of (.*)")]
        public void GivenACapacity(float capacity)
        {
            _group.Capacity = capacity;       
        }

        [Given("an existing Group with name (.*) and Capacity (.*)")]
        public async Task GivenAnExistingGroup(string name, float capacity)
        {
            _scenarioContext["createdGroupResponse"] = await _groupDriver.CreateGroup(name, capacity);
        }

        [When("the group is created")]
        public async Task WhenTheGroupIsCreated()
        {
            _scenarioContext["createdGroupResponse"] = await _groupDriver.CreateGroup(_group.Name, _group.Capacity);
        }

        [When("the group is updated")]
        public async Task WhenTheGroupIsUpdated()
        {
            GroupResource groupResource = await _groupDriver.ParseGroupFromResponse(((HttpResponseMessage)_scenarioContext["createdGroupResponse"]));
            
            _scenarioContext["updatedGroupResponse"] = await _groupDriver.UpdateGroup(groupResource.Id, _group.Name, _group.Capacity);
        }

        [When("the group is deleted")]
        public async Task WhenTheGroupIsDeleted()
        {
            GroupResource groupResource = await _groupDriver.ParseGroupFromResponse((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);

            groupResource.Id.Should().NotBeEmpty();
            _scenarioContext["deletedGroupId"] = groupResource.Id;
            _scenarioContext["deletedGroupResponse"] = await _groupDriver.DeleteGroup(groupResource.Id);
        }

        [When("the wrong group is deleted")]
        public async Task WhenTheWrongGroupIsDeleted()
        {
            Guid wrongGroupId = new Guid();
            _scenarioContext["deletedGroupId"] = wrongGroupId;
            _scenarioContext["deletedGroupResponse"] = await _groupDriver.DeleteGroup(wrongGroupId);
        }

        [Then("the group should not exist anymore")]
        public async Task ThenTheGroupShouldNotExistAnymore()
        {
            await _groupDriver.ShouldDeleteSuccessfully((HttpResponseMessage)_scenarioContext["deletedGroupResponse"],
                (Guid)_scenarioContext["deletedGroupId"]);
        }

        [Then("no group was deleted")]
        public async Task ThenNoGroupShouldBeDeleted()
        {
            ((HttpResponseMessage)_scenarioContext["deletedGroupResponse"]).StatusCode.Should().Be(404);
        }

        [Then("the group should be created successfully")]
        public async Task ThenTheGroupShouldBeCreatedSuccessfully()
        {
            await _groupDriver.ShouldCreateAGroupSuccessfully((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);            
        }

        [Then("the group should be updated successfully")]
        public async Task ThenTheGroupShouldBeUpdatedSuccessfully()
        {
            await _groupDriver.ShouldUpdateAGroupSuccessfully((HttpResponseMessage)_scenarioContext["updatedGroupResponse"], _group);
        }

        [Then("should not be able to update the group")]
        public async Task ThenShouldNotBeAbleToUpdateTheGroup()
        {
            await _groupDriver.ShouldCreateAGroupSuccessfully((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
        }

        [Then("the group should not be created")]
        public async Task ThenTheGroupShouldNotBeCreated()
        {
            await _groupDriver.ShouldNotCreateAGroupSuccessfully((HttpResponseMessage)_scenarioContext["createdGroupResponse"]);
        }


    }
}
