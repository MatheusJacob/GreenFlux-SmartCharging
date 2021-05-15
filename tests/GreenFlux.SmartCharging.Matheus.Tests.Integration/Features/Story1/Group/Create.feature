@Group
Feature: Create Group
	Create a Group through the API

Scenario: Successfully create a Group
	Given a group name Group1
	And a capacity of 5
	When the group is created
	Then the group should be created successfully

Scenario: Missing Capacity to create a group
	Given a group name Group1
	When the group is created
	Then the group should not be created

Scenario: Missing group name to create a group
	Given a capacity of 5
	When the group is created
	Then the group should not be created

Scenario: Trying to create a group with capacity 0
	Given a capacity of 0
	And a group name group1
	When the group is created
	Then the group should not be created