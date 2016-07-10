using OpenQA.Selenium;
using System;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;

namespace ES_SeleniumTestProject
{
    public class AddFileWindow
    {    
            IWebDriver driver;
            public AddFileWindow(IWebDriver driver)
            {
                this.driver = driver;
            }

        //Enter path to file
        public UploadPage EnterUploadFilePath(string path)
        {
            AutomationElement root = AutomationElement.RootElement;
            Condition nameCondition = new PropertyCondition(AutomationElement.ClassNameProperty, "#32770");
            AutomationElement mainWin = root.FindFirst(TreeScope.Subtree, nameCondition);
            Condition locator = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit);
            AutomationElement edit = mainWin.FindFirst(TreeScope.Subtree, locator);
            Thread.Sleep(100);
            SendKeys.SendWait(path);
            Condition buttonLocator = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button);            
            AutomationElementCollection buttons = mainWin.FindAll(TreeScope.Children, buttonLocator);            
            AutomationElement button = mainWin.FindFirst(TreeScope.Children, buttonLocator);           
            InvokePattern ipClickOpen = (InvokePattern)button.GetCurrentPattern(InvokePattern.Pattern);
            ipClickOpen.Invoke();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(300));
            return new UploadPage(driver);
        }
    }
    }
