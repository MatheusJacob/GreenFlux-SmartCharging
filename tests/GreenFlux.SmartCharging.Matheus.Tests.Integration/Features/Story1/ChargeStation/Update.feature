@ChargeStation
Feature: Update a Charge Station
	Update a Charge Station through the API
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

Scenario: Successfully update a Charge Station
	Given a charge station name of C2
	When the Charge Station is updated
	Then the Charge Station should be updated successfully

Scenario: Trying to update Charge Station that doesn't exist
	Given a charge station name of C2
	When the wrong Charge Station is updated
	Then the Charge Station should not be updated successfully