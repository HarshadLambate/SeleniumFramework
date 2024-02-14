using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumDemo.PageObject;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumDemo.StepDefinations
{
    [Binding]
    sealed class UI_TestSteps
    {
        TestHelper helper = new TestHelper();
        private IWebDriver driver;
        LoginPage loginPage;
        DashboardPage dashboardPage;

        public UI_TestSteps(IWebDriver _driver)
        {
            this.driver = _driver;
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);
        }

        [Given(@"I launch browser")]
        public async Task GivenILaunchBrowserAsync()
        {
            var env = await helper.GetEnvironmentDataAsync();
            driver.Navigate().GoToUrl(env.url);
        }

        [Given(@"I enter credentials and click submit")]
        public void GivenIEnterCredentialsAndClickSubmit()
        {
            loginPage.LoginToApp();
        }
        
        [Then(@"I land on home page")]
        public void ThenILandOnHomePage()
        {
            Assert.AreEqual(true, dashboardPage.IsDashboardPageLoaded());
        }
    }
}
