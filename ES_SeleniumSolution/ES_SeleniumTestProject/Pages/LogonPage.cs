using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ES_SeleniumTestProject
{
    public class LogonPage
    {
        IWebDriver driver;
        public LogonPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Page locators
        By emailFieldLocator = By.Id("Email");
        By passwordFieldLocator = By.Id("Passwd");
        By stayInCheckboxLocator = By.Id("PersistentCookie");
        By nextButtonLocator = By.Id("next");
        By signInButtonLocator = By.Id("signIn");

        //Enter email
        public LogonPage EnterEmail(string email)
        {
            IWebElement emailFieldElement = driver.FindElement(emailFieldLocator);
            emailFieldElement.Click();
            emailFieldElement.Clear();
            emailFieldElement.SendKeys(email);            
            return this;
        }

        //Press the Next button. No errors
        public LogonPage SubmitEmailSuccess()
        {
            driver.FindElement(nextButtonLocator).Click();
            return new LogonPage(driver);
        }

        //Enter password
        public LogonPage EnterPassword(string password)
        {
            IWebElement pswdElement = driver.FindElement(passwordFieldLocator);
            IWebElement stayInElement = driver.FindElement(stayInCheckboxLocator);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(pswdElement));
            pswdElement.Click();
            pswdElement.Clear();
            pswdElement.SendKeys(password);
            //Do not store password
            if (stayInElement.Selected)
            {
                stayInElement.Click();
            }            
            return this;
        }
        
        //Press the Sign in button. No errors
        public HomePage SignInSuccess()
        {
            driver.FindElement(signInButtonLocator).Click();
            return new HomePage(driver);
        }
    }
}
