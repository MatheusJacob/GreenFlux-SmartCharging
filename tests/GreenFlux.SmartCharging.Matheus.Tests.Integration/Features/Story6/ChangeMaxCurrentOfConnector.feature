@Connector
Feature: Change Max Current of an existing Connector
	Update an existing Connector Max Current
Background: 
	Given an existing Group with name Group1 and Capacity 100
	Given a charge station name of C1
	And a specific set of connectors
	|MaxCurrentAmp|
	|5 |
	|3 |
	|4 |
	|5 |
	When the Charge Station is created
	Then the Charge Station should be created successfully

Scenario: Successfully update a Connector
	Given a connector with a max current of 3
	When a connector with id 2 is updated
	Then the connector should be updated successfully

Scenario: Increasing the connector max current
	Given a connector with a max current of 10
	When a connector with id 2 is updated
	Then the connector should be updated successfully

Scenario: Decreasing the connector max current
	Given a connector with a max current of 1
	When a connector with id 2 is updated
	Then the connector should be updated successfully