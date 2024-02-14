using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using System;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;

namespace SeleniumDemo.Utilities
{
    public class ExtentReport
    {
        public static ExtentReports extent;
        public static ExtentTest test;
        public static ExtentTest scenario;
        public static ExtentTest feature;
     
        public static void ExtentReportInit()
        {
            string currentDate = DateTime.Now.ToString("dddd, dd MMMM yyyy ");
            string currentTime = DateTime.Now.ToShortTimeString().ToString().Replace(":", ".");
            string currentDateTime = currentDate + currentTime;

            string projectPath = TestHelper.GetProjectPath();
            string reportPath = projectPath + "Reports\\TestResultReport_" + currentDateTime + ".html";
            
            extent = new ExtentReports();
            var sparkReporter = new ExtentSparkReporter(reportPath);
            extent.AttachReporter(sparkReporter);
            extent.AddSystemInfo("Browser", TestHelper.GetAppConfig().browser);
        }    

        public static void EndReport()
        {
            extent.Flush();

            string workingDirectory = Environment.CurrentDirectory;
            System.IO.DirectoryInfo di = new DirectoryInfo(workingDirectory);

            if (di.GetFiles().Count() > Convert.ToInt32(TestHelper.GetAppConfig().maxReports))
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception)
                    {
                        //Existing log will not get deleted as its been used by other process. 
                    }
                }
            }
        }
 
        public string addScreenshot(IWebDriver driver, ScenarioContext scenarioContext)
        {
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            string fileName = scenarioContext.ScenarioInfo.Title + ".png";
            string screenshotPath = TestHelper.GetProjectPath() + @"Reports\" + fileName;  
            screenshot.SaveAsFile(screenshotPath);
            return screenshotPath;
        }

        public static MediaEntityBuilder TakesScreenshot(IWebDriver driver, string screenshotName)
        {
            try
            {
                ITakesScreenshot takeScreenshot = (ITakesScreenshot)driver;
                var screenshot = takeScreenshot.GetScreenshot().AsBase64EncodedString;
                MediaEntityBuilder mediaEntityBuilder = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenshotName);
                return mediaEntityBuilder;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error capturing screenshot: {ex.Message}");
            }
        }
    }
}
