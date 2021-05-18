Feature: Capacity of group should be always greater than sum of all connectors from all linked charge stations
	The capacity of a group should be greater than sum of all connectors

Background: 
	Given an existing Group with name Group1 and Capacity 100		
	Given a charge station name of C1
	And a specific set of connectors
	|maxCurrentAmp|
	|10 |
	When the Charge Station is created
	Then the Charge Station should be created successfully

Scenario: Add multiple charge stations until capacity is over
	Given a specific set of Charge Stations
	| name | connectors |
	| CS1  | 1,1,1,1,1  |
	| CS2  | 1,1,1,1,1  |
	| CS3  | 1,1,1,1,1  |
	| CS4  | 1,1,1,1,1  |
	| CS5  | 1,1,1,1,1  |
	| CS6  | 1,1,1,1,1  |
	| CS7  | 1,1,1,1,1  |
	| CS8  | 1,1,1,1,1  |
	| CS9  | 1,1,1,1,1  |
	| CS10 | 1,1,1,1,1  |
	| CS11 | 1,1,1,1,1  |
	| CS12 | 1,1,1,1,1  |
	| CS13 | 1,1,1,1,1  |
	| CS14 | 1,1,1,1,1  |
	| CS15 | 1,1,1,1,1  |
	| CS16 | 1,1,1,1,1  |
	| CS17 | 1,1,1,1,1  |
	| CS18 | 1,1,1,1,1  |
	When create all Charge Stations
	Then Should create all charge stations successfully
	When listing all charge stations from a group
	Then Should have 19 charge stations
	Given a charge station name of C19
	And a specific set of connectors
	|maxCurrentAmp|
	|1 |
	When the Charge Station is created
	Then the Charge Station should not be created successfully


Scenario: Add connectors until capacity is over
Given a specific set of Charge Stations
	| name | connectors |
	| CS1  | 1          |
	| CS2  | 1          |
	| CS3  | 1          |
	| CS4  | 1          |
	| CS5  | 1          |
	| CS6  | 1          |
	| CS7  | 1          |
	| CS8  | 1          |
	| CS9  | 1          |
	| CS10 | 1          |
	| CS11 | 1          |
	| CS12 | 1          |
	| CS13 | 1          |
	| CS14 | 1          |
	| CS15 | 1          |
	| CS16 | 1          |
	| CS17 | 1          |
	When create all Charge Stations
	Then Should create all charge stations successfully
	When listing all charge stations from a group
	Then Should have 18 charge stations
	Then Should create successfully 4 connectors with 1 max current for all charge stations
	Given a charge station name of C19
	And a specific set of connectors
	|maxCurrentAmp|
	|6 |
	When the Charge Station is created
	Then the Charge Station should not be created successfully

Scenario: Add connectors until capacity is over with floating data
Given a specific set of Charge Stations
	| name | connectors |
	| CS1  | 1.3        |
	| CS2  | 1.7        |
	| CS3  | 1.9        |
	| CS4  | 1.1        |
	| CS5  | 1.555      |
	| CS6  | 1.455      |
	| CS7  | 1          |
	| CS8  | 1          |
	| CS9  | 1          |
	| CS10 | 1          |
	| CS11 | 1          |
	| CS12 | 1          |
	| CS13 | 1          |
	| CS14 | 1          |
	| CS15 | 1          |
	| CS16 | 1          |
	| CS17 | 1          |
	When create all Charge Stations
	Then Should create all charge stations successfully
	When listing all charge stations from a group
	Then Should have 18 charge stations
	Then Should create successfully 4 connectors with 1 max current for all charge stations
	Given a charge station name of C19
	And a specific set of connectors
	|maxCurrentAmp|
	|6 |
	When the Charge Station is created
	Then the Charge Station should not be created successfully

Scenario: Patch connectors until capacity is over
Given a specific set of Charge Stations
	| name | connectors |
	| CS1  | 1          |
	| CS2  | 1          |
	| CS3  | 1          |
	| CS4  | 1          |
	| CS5  | 1          |
	| CS6  | 1          |
	| CS7  | 1          |
	| CS8  | 1          |
	| CS9  | 1          |
	| CS10 | 1          |
	| CS11 | 1          |
	| CS12 | 1          |
	| CS13 | 1          |
	| CS14 | 1          |
	| CS15 | 1          |
	| CS16 | 1          |
	| CS17 | 1          |
	When create all Charge Stations
	Then Should create all charge stations successfully
	When listing all charge stations from a group
	Then Should have 18 charge stations	
	Then Should update successfully all connectors to 5 max current for all charge stations
	Given a charge station name of C19
	And a specific set of connectors
	|maxCurrentAmp|
	|6 |
	When the Charge Station is created
	Then the Charge Station should not be created successfully

Scenario: You have a group with full capacity, and you try to update the group with a lower capacity

Scenario: You have a group with full capacity, and you try to update the group with a bigger capacity