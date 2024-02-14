using OpenQA.Selenium;

namespace SeleniumDemo.PageObject
{
    public class DashboardPage
    {
        private IWebDriver driver;

        public DashboardPage(IWebDriver _driver)
        {
            this.driver = _driver;
        }

        By userAccount = By.XPath("//*[@id='app']/div[1]/div[1]/header/div[1]/div[2]/ul/li/span/p");

        public bool IsDashboardPageLoaded()
        {
            return WebAutomation.PageContainsElement(driver, userAccount);
        }
    }
}
