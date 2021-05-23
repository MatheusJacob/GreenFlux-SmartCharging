Feature: Connector cannot exist in the domain without a charge station
	When someone deletes a charge station all the corresponding connectors should be deleted also

@Connector
Scenario: Removing a charge station should remove all corresponding connectors
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
	When the charge station is deleted
	Then the Charge Station should not exist anymore
	Then the connector with id 1 should not exist anymore
	Then the connector with id 2 should not exist anymore
	Then the connector with id 3 should not exist anymore
	Then the connector with id 4 should not exist anymore