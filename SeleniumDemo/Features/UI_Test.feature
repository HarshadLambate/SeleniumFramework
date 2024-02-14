Feature: UI_Test
	Simple Login Test

@mytag
Scenario: Login To App
	Given I launch browser
	And I enter credentials and click submit
	Then I land on home page