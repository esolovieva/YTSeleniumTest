using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;

namespace ES_SeleniumTestProject
{
    public class UploadPage
    {
        IWebDriver driver;
        public UploadPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Page locators
        By uploadFileButtonLocator = By.XPath("//button[@data-upload-button-id='main']");
        By titleLocator = By.XPath("//input[@name='title']");
        By progressBarLocator = By.XPath("//div[(@class='progress-bar-processing') and (@aria-valuenow = '100')]");

        public AddFileWindow UploadVideo()
        {            
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(uploadFileButtonLocator)).Click().Build().Perform();
            Thread.Sleep(2000);
            return new AddFileWindow(driver);         
        }

        public UploadPage WaitUploadComplete(long waitTimeInMilliSeconds)
        {
            int shift = 1000;
            bool uploadComplete = false;

            while (waitTimeInMilliSeconds >= 0 && !uploadComplete)
            {

                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(shift));
                try
                {
                    driver.FindElement(titleLocator).Click();
                    driver.FindElement(progressBarLocator);
                    uploadComplete = true;
                }
                catch (Exception e) { }

                waitTimeInMilliSeconds = waitTimeInMilliSeconds - shift;
            }
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(10000));
            return this;
        }
    }
}
