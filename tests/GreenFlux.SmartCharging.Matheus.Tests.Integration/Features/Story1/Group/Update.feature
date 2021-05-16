@Group
Feature: Update Group
	Update a Group through the API
Background: 
	Given an existing Group with name Group1 and Capacity 100

Scenario: Successfully update a Group
	Given a group name Group2
	And a capacity of 5
	When the group is updated
	Then the group should be updated successfully

Scenario: Updating only a single property
	Given a group name Group1
	When the group is updated
	Then the group should be updated successfully

Scenario: Trying to update group that doesn't exist
	Given a capacity of 5
	When the wrong group is updated
	Then should not be able to update the group

Scenario: Trying to update wrong property
	Given a capacity of 0
	And a group name group1
	When the group is created
	Then the group should not be created