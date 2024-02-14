using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

public static class WebAutomation
{
    public static void ExplicitWait(this IWebDriver driver, By element)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
        wait.Until(ExpectedConditions.ElementToBeClickable(element));
    }

    public static void ImplicitWait(IWebDriver driver)
    {
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    public static bool PageContainsElement(IWebDriver driver, By element)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(drv => drv.FindElements(element).Count > 0);
            return true;
        }
        catch (TimeoutException)
        {
            return false;
        }
        catch(Exception)
        {
            return false;
        }
    }
}
