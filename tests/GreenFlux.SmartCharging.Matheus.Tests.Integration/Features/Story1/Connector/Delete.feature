@Connector
Feature: Delete a connector
	Delete a connector through the API

Scenario: Successfully delete a connector
	Given an existing Group with name Group1 and Capacity 100
	Given a charge station name of C1
	Given a specific set of connectors
         | MaxCurrentAmp |
         | 1             |
         | 2             |
         | 3             |
         | 4             |
	When the Charge Station is created
	When the connector with id 2 is deleted
	Then the connector should be deleted successfully

Scenario: Try to delete a connector that doesn't exist
Given an existing Group with name Group1 and Capacity 100
	Given a charge station name of C1
	Given a specific set of connectors
         | MaxCurrentAmp |
         | 1             |
         | 2             |
         | 3             |
         | 4             |
	When the Charge Station is created
	When the connector with id 5 is deleted
	Then the connector should not be deleted successfully