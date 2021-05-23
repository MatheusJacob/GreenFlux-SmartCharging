@ChargeStation
Feature: Only one charge station can be added/removed in a single call
	You can only create/remove one charge station at a time	

Scenario: Creating a charge station to an existing group
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
	When listing all charge stations from a group
	Then Should have 1 charge stations