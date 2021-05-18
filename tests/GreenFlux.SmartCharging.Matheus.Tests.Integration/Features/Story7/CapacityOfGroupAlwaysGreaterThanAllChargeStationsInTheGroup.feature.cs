﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.7.0.0
//      SpecFlow Generator Version:3.7.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace GreenFlux.SmartCharging.Matheus.Tests.Integration.Features.Story7
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.7.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Capacity of group should be always greater than sum of all connectors from all li" +
        "nked charge stations")]
    public partial class CapacityOfGroupShouldBeAlwaysGreaterThanSumOfAllConnectorsFromAllLinkedChargeStationsFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
#line 1 "CapacityOfGroupAlwaysGreaterThanAllChargeStationsInTheGroup.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Story7", "Capacity of group should be always greater than sum of all connectors from all li" +
                    "nked charge stations", "\tThe capacity of a group should be greater than sum of all connectors", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 4
#line hidden
#line 5
 testRunner.Given("an existing Group with name Group1 and Capacity 100", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 6
 testRunner.Given("a charge station name of C1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table21 = new TechTalk.SpecFlow.Table(new string[] {
                        "maxCurrentAmp"});
            table21.AddRow(new string[] {
                        "10"});
#line 7
 testRunner.And("a specific set of connectors", ((string)(null)), table21, "And ");
#line hidden
#line 10
 testRunner.When("the Charge Station is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 11
 testRunner.Then("the Charge Station should be created successfully", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Add multiple charge stations until capacity is over")]
        public virtual void AddMultipleChargeStationsUntilCapacityIsOver()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add multiple charge stations until capacity is over", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 13
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
                TechTalk.SpecFlow.Table table22 = new TechTalk.SpecFlow.Table(new string[] {
                            "name",
                            "connectors"});
                table22.AddRow(new string[] {
                            "CS1",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS2",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS3",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS4",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS5",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS6",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS7",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS8",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS9",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS10",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS11",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS12",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS13",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS14",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS15",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS16",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS17",
                            "1,1,1,1,1"});
                table22.AddRow(new string[] {
                            "CS18",
                            "1,1,1,1,1"});
#line 14
 testRunner.Given("a specific set of Charge Stations", ((string)(null)), table22, "Given ");
#line hidden
#line 34
 testRunner.When("create all Charge Stations", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 35
 testRunner.Then("Should create all charge stations successfully", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 36
 testRunner.When("listing all charge stations from a group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 37
 testRunner.Then("Should have 19 charge stations", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 38
 testRunner.Given("a charge station name of C19", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table23 = new TechTalk.SpecFlow.Table(new string[] {
                            "maxCurrentAmp"});
                table23.AddRow(new string[] {
                            "1"});
#line 39
 testRunner.And("a specific set of connectors", ((string)(null)), table23, "And ");
#line hidden
#line 42
 testRunner.When("the Charge Station is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 43
 testRunner.Then("the Charge Station should not be created successfully", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Add connectors until capacity is over")]
        public virtual void AddConnectorsUntilCapacityIsOver()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add connectors until capacity is over", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 46
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
                TechTalk.SpecFlow.Table table24 = new TechTalk.SpecFlow.Table(new string[] {
                            "name",
                            "connectors"});
                table24.AddRow(new string[] {
                            "CS1",
                            "1"});
                table24.AddRow(new string[] {
                            "CS2",
                            "1"});
                table24.AddRow(new string[] {
                            "CS3",
                            "1"});
                table24.AddRow(new string[] {
                            "CS4",
                            "1"});
                table24.AddRow(new string[] {
                            "CS5",
                            "1"});
                table24.AddRow(new string[] {
                            "CS6",
                            "1"});
                table24.AddRow(new string[] {
                            "CS7",
                            "1"});
                table24.AddRow(new string[] {
                            "CS8",
                            "1"});
                table24.AddRow(new string[] {
                            "CS9",
                            "1"});
                table24.AddRow(new string[] {
                            "CS10",
                            "1"});
                table24.AddRow(new string[] {
                            "CS11",
                            "1"});
                table24.AddRow(new string[] {
                            "CS12",
                            "1"});
                table24.AddRow(new string[] {
                            "CS13",
                            "1"});
                table24.AddRow(new string[] {
                            "CS14",
                            "1"});
                table24.AddRow(new string[] {
                            "CS15",
                            "1"});
                table24.AddRow(new string[] {
                            "CS16",
                            "1"});
                table24.AddRow(new string[] {
                            "CS17",
                            "1"});
#line 47
testRunner.Given("a specific set of Charge Stations", ((string)(null)), table24, "Given ");
#line hidden
#line 66
 testRunner.When("create all Charge Stations", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 67
 testRunner.Then("Should create all charge stations successfully", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 68
 testRunner.When("listing all charge stations from a group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 69
 testRunner.Then("Should have 18 charge stations", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 70
 testRunner.Then("Should create successfully 4 connectors with 1 max current for all charge station" +
                        "s", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 71
 testRunner.Given("a charge station name of C19", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table25 = new TechTalk.SpecFlow.Table(new string[] {
                            "maxCurrentAmp"});
                table25.AddRow(new string[] {
                            "6"});
#line 72
 testRunner.And("a specific set of connectors", ((string)(null)), table25, "And ");
#line hidden
#line 75
 testRunner.When("the Charge Station is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 76
 testRunner.Then("the Charge Station should not be created successfully", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Patch connectors until capacity is over")]
        public virtual void PatchConnectorsUntilCapacityIsOver()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Patch connectors until capacity is over", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 77
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Group already with maximum capacity and you try to append a new charge station")]
        public virtual void GroupAlreadyWithMaximumCapacityAndYouTryToAppendANewChargeStation()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Group already with maximum capacity and you try to append a new charge station", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 79
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Group already with maximum capacity and you try to update a connectors max curren" +
            "t")]
        public virtual void GroupAlreadyWithMaximumCapacityAndYouTryToUpdateAConnectorsMaxCurrent()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Group already with maximum capacity and you try to update a connectors max curren" +
                    "t", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 81
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Group already with maximum capacity but you decrease a conector max current, and " +
            "increase another connector max current")]
        public virtual void GroupAlreadyWithMaximumCapacityButYouDecreaseAConectorMaxCurrentAndIncreaseAnotherConnectorMaxCurrent()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Group already with maximum capacity but you decrease a conector max current, and " +
                    "increase another connector max current", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 83
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("You have a group with maximum capacity, then you add another group and add charge" +
            " stations to the new group")]
        public virtual void YouHaveAGroupWithMaximumCapacityThenYouAddAnotherGroupAndAddChargeStationsToTheNewGroup()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("You have a group with maximum capacity, then you add another group and add charge" +
                    " stations to the new group", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 85
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
