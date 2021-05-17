@Connector
Feature: Create a Connector
	Create a connector through the API

Background: 
	Given an existing Group with name Group1 and Capacity 100
	Given an existing Charge station with name C1
	
Scenario: Successfully create a Connector
	Given a connector max current of 1	
	When the connector is created
	Then the connector should be created successfully

Scenario: Creating a connector without max current
	When the connector is created
	Then the connector should not be created successfully

Scenario: Trying to create a connector with a Charge Station that doesn't exist
	Given a connector max current of 1	
	When the connector is created for the wrong Charge Station
	Then should not find the charge station

Scenario Outline: Trying to create more than 5 connectors for a charge station
	Given a connector max current of <maxCurrentAmp>	
	When the connector is created
	Then the expected result should be <created>
	Then the expected connector id should be <expectedConnectorId>
	| MaxCurrentAmp | expectedConnectorId | created |
	| 5             | 1                   | true    |
	| 3             | 2                   | true    |
	| 4             | 3                   | true    |
	| 5             | 4                   | true    |
	| 5             | 5                   | true    |
	| 5             | 0                   | false   |
	| 5             | 0                   | false   |
	| 5             | 0                   | false   |

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
