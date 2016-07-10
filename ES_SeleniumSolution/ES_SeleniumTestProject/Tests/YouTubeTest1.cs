using System;
using OpenQA.Selenium;
using NUnit.Framework;


namespace ES_SeleniumTestProject
{

    [TestFixture()]
    public class YouTubeTest1
    {
        private IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            string url = TestConfig.GetTestVariableValue("SourceURL");
            string dname = TestConfig.GetTestVariableValue("DriverWaitTimeMilliSec");       
            //wd = new ChromeDriver();
            driver = TestDriver.Run(TestConfig.GetTestVariableValue("Browser"), TimeSpan.FromMilliseconds(int.Parse(TestConfig.GetTestVariableValue("DriverWaitTimeMilliSec"))));
           // wd.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
        }

        [Test()]
        public void YouTubeUploadTest()
        {
            string eMail = TestConfig.GetTestVariableValue("UserEMail");
            string passw = TestConfig.GetTestVariableValue("UserPassword");
            string pathToVideoFile = TestConfig.GetTestVariableValue("UploadFilePath");
            HomePage homePage;
            LogonPage logonPage;
            UploadPage uploadPage;
            AddFileWindow addFileWindow;

            //Log in at the YouTube home page
            homePage = new HomePage(driver);            
            logonPage = homePage.PressSignInButton();
            logonPage.EnterEmail(eMail);
            logonPage.SubmitEmailSuccess();
            logonPage.EnterPassword(passw);
            homePage = logonPage.SignInSuccess();  
                      
            //Check authorization succeded
            if (!homePage.UserIsSignedIn()) Assert.Fail("Authorization failed!");

            //Upload video file   
            uploadPage = homePage.PressUploadVideoButton();
            addFileWindow = uploadPage.UploadVideo();
            uploadPage = addFileWindow.EnterUploadFilePath(pathToVideoFile);
            uploadPage.WaitUploadComplete(300000);
        }

        [TearDown]
        public void StopBrowser()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }
    }
}