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
	When 
	Then the group should be updated successfully

Scenario: Updating only a single property
	Given a group name Group1
	When the group is updated
	Then the group should be updated successfully

Scenario: Trying to update group that doesn't exist
	Given a capacity of 5
	When the wrong group is updated
	Then should not be able to update the group