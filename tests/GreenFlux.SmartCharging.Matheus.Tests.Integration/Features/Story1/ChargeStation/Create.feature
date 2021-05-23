@ChargeStation
Feature: Create a Charge Station
	Create a charge station through the API

Background: 
	Given an existing Group with name Group1 and Capacity 100

Scenario: Successfully create a Charge Station
	Given a charge station name of C1
	And a specific set of connectors
	|MaxCurrentAmp|
	|5 |
	|3 |
	|4 |
	|5 |
	When the Charge Station is created
	Then the Charge Station should be created successfully

Scenario: Missing Name to create a Charge Station	
	When the Charge Station is created
	Then the Charge Station should not be created successfully

Scenario: Trying to create a charge station with a group that doesn't exist
	Given a charge station name of C2
	And a specific set of connectors
	|MaxCurrentAmp|
	|1 |
	|2 |
	|3 |
	|4 |
	When the Charge Station is created for the wrong group
	Then should not find the group