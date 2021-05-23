@ChargeStation
Feature: Delete a Charge Station
	Delete a Charge Station through the API
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

Scenario: Successfully delete a Charge Station
	When the charge station is deleted
	Then the Charge Station should not exist anymore

Scenario: Try to delete a charge station that doesn't exist
	When the wrong Charge Station is deleted
	Then no Charge Station was deleted
