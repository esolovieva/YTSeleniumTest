using OpenQA.Selenium;


namespace ES_SeleniumTestProject
{
   public class HomePage
    {
        IWebDriver driver;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Page locators
        By signInButtonLocator = By.XPath("//div[@id='yt-masthead-signin']//button");
        By addVideoButtonLocator = By.Id("upload-btn");
        By accountPickerLocator = By.Id("yt-masthead-account-picker");

        //Press Sign-in button at the Home page
        public LogonPage PressSignInButton()
        {
            driver.FindElement(signInButtonLocator).Click();
            return new LogonPage(driver);
        }

        //Press Upload Video button at the Home page
        public UploadPage PressUploadVideoButton()
        {
            driver.FindElement(addVideoButtonLocator).Click();
            return new UploadPage(driver);
        }

        //Check that user signed in successfully. 
        public bool UserIsSignedIn()
        {
            if (driver.FindElements(accountPickerLocator).Count > 0)
                return true;
            else
                return false;
        }

    }
}
