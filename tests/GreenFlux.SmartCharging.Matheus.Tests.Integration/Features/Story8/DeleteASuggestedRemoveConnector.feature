Feature: DeleteASuggestedRemoveConnector
	When deleting all suggestions from a remove suggestion response,then should be able to create the connector

@suggestion
Scenario: Creating a connector after deleting all connectors from a remove suggestion
	Given an existing Group with name G1 and Capacity 100
	Given a specific set of Charge Stations
	| name | connectors |
	| A    | 27, 27, 27 |
	| B    | 19         |
	When create all Charge Stations
	Then Should create all charge stations successfully
	When listing all charge stations from a group
	Then Should have 2 charge stations
	Given a connector with a max current of 30	
	When the connector is created
	Then the Connector should not be created successfully
	Then the create connector response should contain at least a suggestion
	Then the remove suggestion response should have this specific results
		| suggestionListPosition | chargeStationId | connectorId |
		| 1                      | 1               | 1           |
		| 1                      | 2               | 1           |
		| 2                      | 1               | 2           |
		| 2                      | 2               | 1           |
		| 3                      | 1               | 3           |
		| 3                      | 2               | 1           |
	Then should have returned 3 suggestions
	When deleting all connectors in the suggestion list of number 1
	When the connector is created
	Then the Connector should be created successfully