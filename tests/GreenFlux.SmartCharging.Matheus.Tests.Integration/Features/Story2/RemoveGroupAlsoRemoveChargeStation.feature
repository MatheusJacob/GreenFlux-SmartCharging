Feature: Removing a group should also remove all charge stations 
	When someone deletes a group, all the corresponding charge stations should be removed also

@Group
Scenario: Removing a group that has charge stations attached to it
	Given a group name G1
	And a capacity of 100
	When the group is created
	Then the group should be created successfully
	Given a charge station name of C1
	And a specific set of connectors
		|MaxCurrentAmp|
		|5 |
		|3 |
		|4 |
		|5 |
	When the Charge Station is created
	Then the Charge Station should be created successfully
	When the group is deleted
	Then the created group should not exist anymore
	Then the created Charge Station should not exist anymore
	Then the connector with id 1 should not exist anymore
	Then the connector with id 2 should not exist anymore
	Then the connector with id 3 should not exist anymore
	Then the connector with id 4 should not exist anymore