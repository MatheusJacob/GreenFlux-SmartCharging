using GreenFlux.SmartCharging.Matheus.API.Resources;
using GreenFlux.SmartCharging.Matheus.API.Resources.ProblemDetail;
using GreenFlux.SmartCharging.Matheus.Tests.Integration.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using FluentAssertions;
namespace GreenFlux.SmartCharging.Matheus.Tests.Integration.Steps
{
    [Binding]
    public sealed class SuggestionSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly GroupDriver _groupDriver;
        private readonly ConnectorDriver _connectorDriver;
        private readonly ChargeStationDriver _chargeStationDriver;
        private readonly SuggestionDriver _suggestionDriver;

        public SuggestionSteps(ScenarioContext scenarioContext, ConnectorDriver connectorDriver, GroupDriver groupDriver,
            ChargeStationDriver chargeStationDriver, SuggestionDriver suggestionDriver)
        {
            _scenarioContext = scenarioContext;
            _groupDriver = groupDriver;
            _chargeStationDriver = chargeStationDriver;
            _connectorDriver = connectorDriver;
            _suggestionDriver = suggestionDriver;
        }

        [Then("the create connector response should contain at least a suggestion")]
        public async Task ThenTheCreateConnectorResponseShouldContainAtLeastASuggestion()
        {
            _scenarioContext.ContainsKey("createdConnector");
            HttpResponseMessage response = (HttpResponseMessage)_scenarioContext["createdConnector"];

            CapacityExceededProblemDetail capacityExceeded = await _suggestionDriver.ParseFromResponse<CapacityExceededProblemDetail>(response);
            capacityExceeded.Status.Should().Be(400);
            capacityExceeded.RemoveSuggestions.Should().NotBeNull();
            capacityExceeded.RemoveSuggestions.Count.Should().BeGreaterThan(0);
        }

        [Then(@"the remove suggestion response should have this specific results")]
        public async Task ThenRemoveSuggestionResponseShouldHaveThisSpecificResults(Table table)
        {
            _scenarioContext.ContainsKey("createdConnector");
            _scenarioContext.ContainsKey("chargeStationListResponses");

            HttpResponseMessage response = (HttpResponseMessage)_scenarioContext["createdConnector"];
            List<HttpResponseMessage> createdChargeStations = (List<HttpResponseMessage>)_scenarioContext["chargeStationListResponses"];
            CapacityExceededProblemDetail capacityExceeded = await _suggestionDriver.ParseFromResponse<CapacityExceededProblemDetail>(response);

            foreach (var row in table.Rows)
            {
                int sugestionListPosition, chargeStationId, connectorId;

                int.TryParse(row["suggestionListPosition"], out sugestionListPosition).Should().BeTrue();
                int.TryParse(row["chargeStationId"], out chargeStationId).Should().BeTrue();
                int.TryParse(row["connectorId"], out connectorId).Should().BeTrue();
              
                capacityExceeded.RemoveSuggestions.Count.Should().BeGreaterOrEqualTo(sugestionListPosition);

                var suggestionList = capacityExceeded.RemoveSuggestions[sugestionListPosition - 1];

                bool foundConnector = false;

                foreach (var suggestion in suggestionList)
                {
                    ChargeStationResource chargeStation = await _suggestionDriver.ParseFromResponse<ChargeStationResource>(createdChargeStations[chargeStationId - 1]);
                    if (suggestion.ChargeStationId == chargeStation.Id && suggestion.ConnectorId == connectorId)
                        foundConnector = true;
                }

                foundConnector.Should().BeTrue();
            }
        }


    }
}
