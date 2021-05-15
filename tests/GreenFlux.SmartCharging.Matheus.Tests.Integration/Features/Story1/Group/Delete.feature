@Group
Feature: Delete a group
	Delete a group through the API

Scenario: Successfully delete a group
	Given an existing Group with name Group1 and Capacity 5
	When the group is deleted
	Then the group should not exist anymore

Scenario: Try to delete a group that doesn't exist
	Given an existing Group with name Group1 and Capacity 5
	When the wrong group is deleted
	Then no group was deleted
