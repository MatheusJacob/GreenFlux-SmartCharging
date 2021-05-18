@Connector
Feature: Create a Connector
	Create a connector through the API

Background: 
	Given an existing Group with name Group1 and Capacity 100
	Given a charge station name of C1
	And a specific set of connectors
	|MaxCurrentAmp|
	|1 |
	When the Charge Station is created
	Then the Charge Station should be created successfully

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
		|MaxCurrentAmp|
		|2 |
		|3 |
		|4 |
		|5 |
		|6 |
	When the connectors are created sequencially
	Then the expected results should be
		| expectedConnectorId | created |
		| 2                   | true    |
		| 3                   | true    |
		| 4                   | true    |
		| 5                   | true    |
		| 5                   | false   |
		| 5                   | false   |

Scenario Outline: Creating/Deleting a chain of connectors should evaluate the right connector id
	Given a connector max current of <maxCurrentAmp>	
	When the connector is <action>
	Then the expected result should be <created>
	Then the expected connector id should be <expectedConnectorId>
	| MaxCurrentAmp | expectedConnectorId | action  |
	| 1             | 1                   | created |
	| 2             | 2                   | created |
	| 3             | 3                   | created |
	| 4             | 4                   | created |
	| 4             | 4                   | deleted |
	| 3             | 3                   | deleted |
	| 3             | 3                   | created |
	| 4             | 4                   | created |
	| 5             | 5                   | created |
	| 2             | 2                   | deleted |
	| 3             | 3                   | deleted |
	| 2             | 2                   | created |
	| 3             | 3                   | created |

Scenario Outline: Creating connectors for different charge stations should not interfere with their connector id
	Given a connector max current of <maxCurrentAmp>	
	Given an existing charge station <chargeStationId>
	When the connector is <action>
	Then the expected result should be <created>
	Then the expected connector id should be <expectedConnectorId>
	| MaxCurrentAmp | expectedConnectorId | chargeStationId | action  |
	| 1             | 1                   | 1               | created |
	| 2             | 2                   | 1               | created |
	| 3             | 3                   | 1               | created |
	| 4             | 4                   | 1               | created |
	| 4             | 1                   | 2               | deleted |
	| 3             | 2                   | 2               | deleted |
	| 3             | 3                   | 2               | created |
	| 4             | 4                   | 2               | created |
	| 5             | 4                   | 2               | deleted |
	| 2             | 4                   | 1               | deleted |
	| 3             | 4                   | 1               | created |
	| 2             | 4                   | 2               | created |
	| 2             | 5                   | 2               | created |
