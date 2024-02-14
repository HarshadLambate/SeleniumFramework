using AventStack.ExtentReports.Gherkin.Model;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SeleniumDemo.Utilities;
using System;
using TechTalk.SpecFlow;

namespace SeleniumDemo.Hooks
{
    [Binding]
    public sealed class Hooks : ExtentReport
    {
        private readonly IObjectContainer _container;

        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            ExtentReportInit();
            feature = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            scenario = feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);

            string browser = TestHelper.GetAppConfig().browser;                               
            IWebDriver driver;
            switch (browser?.ToLower())
            {
                case "chrome":
                    driver = new ChromeDriver();
                    break;
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                default:
                    throw new NotSupportedException($"Browser is not supported.");
            }

            driver.Manage().Window.Maximize();
            _container.RegisterInstanceAs<IWebDriver>(driver);
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType;
            var stepName = scenarioContext.StepContext.StepInfo.Text;
            var stepStatus = scenarioContext.TestError;
            var driver = _container.Resolve<IWebDriver>();

            if (scenarioContext.TestError == null)
            {
                switch(stepType.ToString())
                {
                    case "Given":
                        scenario.CreateNode<Given>(stepName).Pass("Pass");
                        break;
                    case "When":
                        scenario.CreateNode<When>(stepName).Pass("Pass");
                        break;
                    case "And":
                        scenario.CreateNode<And>(stepName).Pass("Pass");
                        break;
                    case "Then":
                        scenario.CreateNode<Then>(stepName).Pass("Pass");
                        break;
                }
            }
            else
            {
                switch (stepType.ToString())
                {
                    case "Given":
                        scenario.CreateNode<Given>(stepName).Fail(stepStatus.Message, TakesScreenshot(driver, stepName).Build());
                        break;
                    case "When":
                        scenario.CreateNode<When>(stepName).Fail(stepStatus.Message, TakesScreenshot(driver, stepName).Build());
                        break;
                    case "And":
                        scenario.CreateNode<And>(stepName).Fail(stepStatus.Message, TakesScreenshot(driver, stepName).Build());
                        break;
                    case "Then":
                        scenario.CreateNode<Then>(stepName).Fail(stepStatus.Message, TakesScreenshot(driver, stepName).Build());
                        break;
                }
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var driver = _container.Resolve<IWebDriver>();

            if (driver != null)
            {
                driver.Quit();
            }
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            EndReport();
        }
    }
}