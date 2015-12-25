using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Automation;

namespace YouTubeTests
{

    [TestFixture()]
    public class YouTubeTest
    {
        private IWebDriver wd;

        [SetUp]
        public void StartBrowser()
        {
            string url = TestConfig.GetTestVariableValue("SourceURL");
            string dname = TestConfig.GetTestVariableValue("DriverWaitTimeMilliSec");        

            //wd = new ChromeDriver();
            wd = TestDriver.Run(TestConfig.GetTestVariableValue("Browser"), TimeSpan.FromMilliseconds(int.Parse(TestConfig.GetTestVariableValue("DriverWaitTimeMilliSec"))));
           // wd.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));
            wd.Manage().Window.Maximize();
            wd.Navigate().GoToUrl(url);
        }

        [Test()]
        public void YouTubeUploadTest()
        {
            string eMail = TestConfig.GetTestVariableValue("UserEMail");
            string passw = TestConfig.GetTestVariableValue("UserPassword");
            string pathToVideoFile = TestConfig.GetTestVariableValue("UploadFilePath");
            //Log in at the YouTube home page
            YouTubeLogin(eMail, passw); 
            //Check authorization succeded
            if (!IsSignedIn()) Assert.Fail("Authorization failed!");
            //Upload video file         
            YouTubeUploadVideo(pathToVideoFile); 
        }

        [TearDown]
        public void StopBrowser()
        {
            if (wd != null)
            {
                wd.Quit();
                wd = null;
            }
        }

        private void YouTubeLogin(string email, string paswd)
        {
            ClickOnSignInButton();
            EnterEmail(email);
            EnterPasswordAndSignIn(paswd);
        }

        private void YouTubeUploadVideo(string pathToVideoFile)
        {
            wd.FindElement(By.Id("upload-btn")).Click();
            Actions action = new Actions(wd);
            action.MoveToElement(wd.FindElement(By.XPath("//button[@data-upload-button-id='main']"))).Click().Build().Perform();
            Thread.Sleep(2000);
            EnterUploadFilePath(pathToVideoFile);
            WaitUploadComplete(300000);
        }

        private void EnterUploadFilePath(string path)
        {
            AutomationElement root = AutomationElement.RootElement;
            Condition nameCondition = new PropertyCondition(AutomationElement.ClassNameProperty, "#32770");
            AutomationElement mainWin = root.FindFirst(TreeScope.Subtree, nameCondition);
            Condition locator = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit);
            AutomationElement edit = mainWin.FindFirst(TreeScope.Subtree, locator);
            Thread.Sleep(100);
            SendKeys.SendWait(path);
            Condition buttonLocator = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button);
            AutomationElement button = mainWin.FindFirst(TreeScope.Children, buttonLocator);
            InvokePattern ipClickOpen = (InvokePattern)button.GetCurrentPattern(InvokePattern.Pattern);
            ipClickOpen.Invoke();
            wd.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(300));
        }

        private void ClickOnSignInButton()
        {
            wd.FindElement(By.XPath("//div[@id='yt-masthead-signin']//button")).Click();
        }

        private void EnterEmail(string emailAddress)
        {
            wd.FindElement(By.Id("Email")).Click();
            wd.FindElement(By.Id("Email")).Clear();
            wd.FindElement(By.Id("Email")).SendKeys(emailAddress); 
            wd.FindElement(By.Id("next")).Click();
        }

        private void EnterPasswordAndSignIn(string paswd)
        {
            WebDriverWait wait = new WebDriverWait(wd, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Passwd")));
            wd.FindElement(By.Id("Passwd")).Click();
            wd.FindElement(By.Id("Passwd")).Clear();
            wd.FindElement(By.Id("Passwd")).SendKeys(paswd); 
            //Do not store password
            if (wd.FindElement(By.Id("PersistentCookie")).Selected)
            {
                wd.FindElement(By.Id("PersistentCookie")).Click();
            }
            wd.FindElement(By.Id("signIn")).Click();
        }

        private bool IsSignedIn()
        {
            if (wd.FindElements(By.Id("yt-masthead-account-picker")).Count > 0)
                return true;
            else
                return false;
        }

        private void WaitUploadComplete(long waitTimeInMilliSeconds)
        {
            int shift = 1000;
            bool uploadComplete = false;
            
                while (waitTimeInMilliSeconds >= 0 && !uploadComplete)
                {

                    wd.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(shift));
                    try
                    {
                        wd.FindElement(By.XPath("//input[@name='title']")).Click();
                        wd.FindElement(By.XPath("//div[(@class='progress-bar-processing') and (@aria-valuenow = '100')]"));
                        uploadComplete = true;
                    }
                    catch (Exception e) { }

                    waitTimeInMilliSeconds = waitTimeInMilliSeconds - shift;
                }
                    wd.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(10000));
        }
    }
}