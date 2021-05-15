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

        [When("the group is created")]
        public async Task WhenTheGroupIsCreated()
        {
            _scenarioContext["response"] = await _groupDriver.CreateGroup(_group.Name, _group.Capacity); ;
        }

        [Then("the group should be created successfully")]
        public async Task ThenTheGroupShouldBeCreatedSuccessfully()
        {
            await _groupDriver.ShouldCreateAGroupSuccessfully((HttpResponseMessage)_scenarioContext["response"]);            
        }

        [Then("the group should not be created")]
        public async Task ThenTheGroupShouldNotBeCreated()
        {
            await _groupDriver.ShouldNotCreateAGroupSuccessfully((HttpResponseMessage)_scenarioContext["response"]);
        }
    }
}
