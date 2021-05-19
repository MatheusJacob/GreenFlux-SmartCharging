Feature: Examples provided on documentation
	All examples provided on the documentation

Scenario: A
	Given an existing Group with name G1 and Capacity 100
	Given a specific set of Charge Stations
	| name | connectors     |
	| A    | 10,10,10,10,10 |
	| B    | 20,20          |
	When create all Charge Stations
	Then Should create all charge stations successfully
	When listing all charge stations from a group
	Then Should have 2 charge stations
	Given a connector with a max current of 15	
	When the connector is created
	Then the Connector should not be created successfully
	Then the create connector response should contain at least a suggestion
	Then the remove suggestion response should have this specific results
		| suggestionListPosition | chargeStationId | connectorId |
		| 1                      | 1               | 1           |
		| 2                      | 1               | 2           |
		| 3                      | 1               | 3           |
		| 4                      | 1               | 4           |
		| 5                      | 1               | 5           |

Scenario: B
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

Scenario: C
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