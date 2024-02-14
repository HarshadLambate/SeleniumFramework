using OpenQA.Selenium;

namespace SeleniumDemo.PageObject
{
    public class LoginPage
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        
        By username = By.XPath("//*[@id='app']/div[1]/div/div[1]/div/div[2]/div[2]/form/div[1]/div/div[2]/input");
        By password = By.XPath("//*[@id='app']/div[1]/div/div[1]/div/div[2]/div[2]/form/div[2]/div/div[2]/input");
        By submit = By.XPath("//*[@id='app']/div[1]/div/div[1]/div/div[2]/div[2]/form/div[3]/button");

        public void LoginToApp()
        {
            WebAutomation.ExplicitWait(driver, submit);
            driver.FindElement(username).SendKeys("Admin");
            driver.FindElement(password).SendKeys("admin123");
            driver.FindElement(submit).Click();
        }

        public bool IsLoginPageLoaded()
        {
            return WebAutomation.PageContainsElement(driver, submit);
        }
    }
}
