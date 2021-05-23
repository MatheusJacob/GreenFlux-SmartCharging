using FluentAssertions;
using GreenFlux.SmartCharging.Matheus.API.Controllers;
using GreenFlux.SmartCharging.Matheus.API.Resources.ProblemDetail;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace GreenFlux.SmartCharging.Matheus.Tests.Integration.Drivers
{
    public class SuggestionDriver : BaseDriver
    {
        private Func<string, string, string> ConnectorBaseUrl = (groupId, chargeStationId) => Routes.GroupsRoute + "/" + groupId + Routes.ChargeStationBaseRoute + "/" + chargeStationId + Routes.ConnectorsBaseRoute;
        private Func<string, string, string, string> ConnectorUrl = (groupId, chargeStationId, connectorId) => Routes.GroupsRoute + "/" + groupId + Routes.ChargeStationBaseRoute + "/" + chargeStationId + Routes.ConnectorsBaseRoute + "/" + connectorId;

        private readonly ConnectorDriver _connectorDriver;


        SuggestionDriver(ScenarioContext scenarioContext, ConnectorDriver connectorDriver) : base()
        {
            _connectorDriver = connectorDriver;

        }


        public void ShouldHaveExactNumberOfSuggestions(CapacityExceededProblemDetail capacityProblemDetail, int number)
        {
            capacityProblemDetail.RemoveSuggestions.Count.Should().Be(number);
        }

        public async Task ValidateIfTheConnectorsStillExists(Guid groupId, CapacityExceededProblemDetail capacityProblemDetail)
        {
            foreach (var suggestionList in capacityProblemDetail.RemoveSuggestions)
            {
                foreach (var suggestion in suggestionList)
                {
                    var response = await _connectorDriver.GetConnector(groupId, suggestion.ChargeStationId, suggestion.ConnectorId);
                    response.StatusCode.Should().Be(200);
                }
            }
        }
    }
}
