﻿Feature: Some aditional scenarios
	Aditional scenarios to cover the suggestions

Scenario: with lots of small connectors
	Given an existing Group with name G1 and Capacity 100
	Given a specific set of Charge Stations
	| name | connectors |
	| A    | 1,1,1,1,1  |
	| B    | 2,2,2,2,2  |
	| C    | 20,10,31  |
	When create all Charge Stations
	Then Should create all charge stations successfully
	When listing all charge stations from a group
	Then Should have 3 charge stations
	Given a connector with a max current of 31	
	When the connector is created
	Then the Connector should not be created successfully
	Then the create connector response should contain at least a suggestion
	Then the remove suggestion response should have this specific results
		| suggestionListPosition | chargeStationId | connectorId |
		| 1                      | 1               | 1           |
		| 1                      | 1               | 2           |
		| 1                      | 1               | 3           |
		| 1                      | 1               | 4           |
		| 1                      | 1               | 5           |
		| 1                      | 2               | 1           |
		| 1                      | 2               | 2           |
		| 1                      | 2               | 3           |
		| 1                      | 2               | 4           |
		| 1                      | 2               | 5           |
		| 1                      | 3               | 2           |

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