Feature: Don't automatically remove connectors when trying to add an exceeded capacity connector
	 Don't automatically remove connectors when trying to add an exceeded capacity connector

@mytag
Scenario: Trying to insert a new connector on an exceeded group, should return 400 and should not delete any existing conectors
	Given an existing Group with name G1 and Capacity 100
	Given a specific set of Charge Stations
	| name | connectors |
	| A    | 25, 25, 25 |
	| B    | 15         |
	When create all Charge Stations
	Then Should create all charge stations successfully
	When listing all charge stations from a group
	Then Should have 2 charge stations
	Given a connector with a max current of 20	
	When the connector is created
	Then the Connector should not be created successfully
	Then the create connector response should contain at least a suggestion
	Then the remove suggestion response should have this specific results
		| suggestionListPosition | chargeStationId | connectorId |
		| 1                      | 2               | 1           |	
	Then should have returned 1 suggestions
	Then the connectors from the suggestions should not be automatically deleted
