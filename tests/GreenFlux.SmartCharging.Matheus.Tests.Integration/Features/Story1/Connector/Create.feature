@Connector
Feature: Create a Connector
	Create a connector through the API

Background: 
	Given an existing Group with name Group1 and Capacity 100
	Then save the last created Group id
	Given a charge station name of C1
	And a specific set of connectors
	|maxCurrentAmp|
	|1 |
	When the Charge Station is created
	Then the Charge Station should be created successfully
	Then save the last created charge station id

Scenario: Successfully create a Connector
	Given a connector with a max current of 3
	When the connector is created
	Then the Connector should be created successfully

Scenario: Creating a connector without max current
	When the connector is created with required parameters missing
	Then the Connector should not be created successfully

Scenario: Creating a connector with empty max current
	Given a connector with a max current of 0
	When the connector is created
	Then the Connector should not be created successfully

Scenario: Trying to create a connector with a Charge Station that doesn't exist
	Given a connector with a max current of 5
	And the wrong charge station is provided
	When the connector is created
	Then should not find the charge station

Scenario: Trying to create more than 5 connectors for a charge station
	Given a specific set of connectors
		| maxCurrentAmp |
		| 2             |
		| 3             |
		| 4             |
		| 5             |
		| 6             |
		| 7             |
	When the connectors are created sequencially
	Then the expected results should be
		| expectedConnectorId | action       |
		| 2                   | created      |
		| 3                   | created      |
		| 4                   | created      |
		| 5                   | created      |
		| 5                   | failToCreate |
		| 5                   | failToCreate |

Scenario: Creating/Deleting a chain of connectors should evaluate the right connectors id
	When a specific set of actions is executed sequencially
		| maxCurrentAmp | expectedConnectorId | action |
		| 2             | 2                   | create |
		| 3             | 3                   | create |
		| 4             | 4                   | create |
		| 4             | 4                   | delete |
		| 3             | 3                   | delete |
		| 3             | 3                   | create |
		| 4             | 4                   | create |
		| 5             | 5                   | create |
		| 2             | 2                   | delete |
		| 3             | 3                   | delete |
		| 2             | 2                   | create |
		| 3             | 3                   | create |
		| 3             | 5                   | delete |
		| 3             | 5                   | delete |
		| 3             | 5                   | create |
		| 3             | 6                   | create |
	Then the expected results should be
		| expectedConnectorId | action       |
		| 2                   | created      |
		| 3                   | created      |
		| 4                   | created      |
		| 4                   | deleted      |
		| 3                   | deleted      |
		| 3                   | created      |
		| 4                   | created      |
		| 5                   | created      |
		| 2                   | deleted      |
		| 3                   | deleted      |
		| 2                   | created      |
		| 3                   | created      |
		| 5                   | deleted      |
		| 5                   | failToDelete |
		| 5                   | created      |
		| 6                   | failToCreate |
	

Scenario: Creating connectors for different charge stations should not interfere with their connector id
	Given a charge station name of C2
	And a specific set of connectors
	|MaxCurrentAmp|
	|10 |
	When the Charge Station is created
	Then the Charge Station should be created successfully
	Then save the last created charge station id
	Given a group name G2
	And a capacity of 100
	When the group is created
	Then the group should be created successfully
	Then save the last created Group id
	Given a charge station name of C3
	And a specific set of connectors
	|MaxCurrentAmp|
	|1 |
	When the Charge Station is created
	Then the Charge Station should be created successfully
	Then save the last created charge station id
	Given a charge station name of C4
	And a specific set of connectors
	|MaxCurrentAmp|
	|1 |
	When the Charge Station is created
	Then the Charge Station should be created successfully
	Then save the last created charge station id
	When a specific set of actions is executed sequencially
	| maxCurrentAmp | expectedConnectorId | chargeStationId | groupId | action |
	| 1             | 2                   | 0               | 0       | create |
	| 2             | 3                   | 0               | 0       | create |
	| 3             | 4                   | 0               | 0       | create |
	| 4             | 5                   | 0               | 0       | create |
	| 4             | 2                   | 1               | 0       | create |
	| 3             | 3                   | 1               | 0       | create |
	| 3             | 4                   | 1               | 0       | create |
	| 4             | 5                   | 1               | 0       | create |
	| 5             | 2                   | 2               | 1       | create |
	| 2             | 3                   | 2               | 1       | create |
	| 3             | 4                   | 2               | 1       | create |
	| 2             | 5                   | 2               | 1       | create |
	| 5             | 2                   | 3               | 1       | create |
	| 2             | 3                   | 3               | 1       | create |
	| 3             | 4                   | 3               | 1       | create |
	| 2             | 5                   | 3               | 1       | create |
	Then all actions should be executed successfully
