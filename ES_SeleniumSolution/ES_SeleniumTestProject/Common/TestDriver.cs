using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace ES_SeleniumTestProject
{
    class TestDriver
    {

        public static IWebDriver Run(String browserName, TimeSpan serverTimeout)
        {
            IWebDriver driver;
            switch (browserName.ToLower())
            {
                case "chrome":
                    driver = new ChromeDriver();
                    break;

                case "ie":
                    driver = new InternetExplorerDriver();
                    break;

                case "firefox":
                    driver = new FirefoxDriver();
                    break;

                default:
                    driver = new FirefoxDriver();
                    break;
            }

            driver.Manage().Timeouts().ImplicitlyWait(serverTimeout);
            return driver;
        }

    }
}
