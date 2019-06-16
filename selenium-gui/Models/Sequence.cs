using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using selenium_gui.Interfaces;

namespace selenium_gui.Models
{
    public class Sequence : ISequence
    {
        [ExcludeFromCodeCoverage]
        public List<Step> Steps { get; set; }

        [ExcludeFromCodeCoverage]
        public IWebDriver Driver { get; set; }

        public string Run()
        {
            IWebElement element = null;
            try
            {
                foreach (Step step in Steps)
                {
                    switch (step.Type)
                    {
                        case "Alert Operations":
                            AlertOperations(step);
                            break;
                        case "Browser Operations":
                            BrowserOperations(step);
                            break;
                        case "Cookie Operations":
                            CookieOperations(step);
                            break;
                        case "Element Operations":
                            ElementOperations(element, step);
                            break;
                        case "Find Element":
                            element = FindElement(step);
                            break;
                        case "Navigate":
                            Navigate(step);
                            break;
                        case "Select And Deselect":
                            SelectAndDeselect(element, step);
                            break;
                        case "Wait":
                            Wait(step);
                            break;
                        case "Wait For":
                            WaitFor(element, step);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            

            return "Sequence ran sucessfully";
        }

        public void AlertOperations(Step step)
        {
            switch (step.Parameters[0])
            {
                case "Accept":
                    Driver.SwitchTo().Alert().Accept();
                    break;
                case "Dismiss":
                    Driver.SwitchTo().Alert().Dismiss();
                    break;
                case "Send Keys":
                    Driver.SwitchTo().Alert().SendKeys(step.Parameters[1]);
                    break;
            }
        }

        public void BrowserOperations(Step step)
        {
            switch (step.Parameters[0])
            {
                case "Maximize":
                    Driver.Manage().Window.Maximize();
                    break;
                case "Quit":
                    Driver.Quit();
                    break;
            }
        }

        public void CookieOperations(Step step)
        {
            switch (step.Parameters[0])
            {
                case "Add Cookie":
                    Cookie cookie = new Cookie(step.Parameters[1], step.Parameters[2]);
                    Driver.Manage().Cookies.AddCookie(cookie);
                    break;
                case "Delete All Cookies":
                    Driver.Manage().Cookies.DeleteAllCookies();
                    break;
                case "Delete Cookie By Name":
                    Driver.Manage().Cookies.DeleteCookieNamed(step.Parameters[1]);
                    break;
            } 
        }

         private void ElementOperations(IWebElement element, Step step)
        {
            switch (step.Parameters[0])
            {
                case "Click":
                    element.Click();
                    break;

                case "Clear":
                    element?.Clear();
                    break;
                case "Drag and Drop":
                    Actions move = new Actions(Driver);
                    var x = Convert.ToInt32(step.Parameters[1]);
                    var y = Convert.ToInt32(step.Parameters[2]);
                    move.DragAndDropToOffset(element, x, y).Perform();
                    break;
                case "Send Keys":
                    element?.SendKeys(step.Parameters[1]);
                    break;

                case "submit":
                    element?.Submit();
                    break;
                case "Scroll To Element":
                    Actions actions = new Actions(Driver);
                    actions.MoveToElement(element);
                    break;
            }
        }

        private IWebElement FindElement(Step step)
        {
            IWebElement element = null;

            switch (step.Parameters[0])
            {
                case "By Class Name":
                    element = Driver.FindElement(By.ClassName(step.Parameters[1]));
                    break;

                case "By CSS Selector":
                    element = Driver.FindElement(By.CssSelector(step.Parameters[1]));
                    break;

                case "By ID":
                    element = Driver.FindElement(By.Id(step.Parameters[1]));
                    break;

                case "By Link text":
                    element = Driver.FindElement(By.LinkText(step.Parameters[1]));
                    break;

                case "By Name":
                    element = Driver.FindElement(By.Name(step.Parameters[1]));
                    break;

                case "By Partial Link":
                    element = Driver.FindElement(By.PartialLinkText(step.Parameters[1]));
                    break;

                case "By Tag Name":
                    element = Driver.FindElement(By.TagName(step.Parameters[1]));
                    break;

                case "By XPath":
                    element = Driver.FindElement(By.XPath(step.Parameters[1]));
                    break;
            }

            return element;
        }

        private void Navigate(Step step)
        {
            switch (step.Parameters[0])
            {
                case "Back":
                    Driver.Navigate().Back();
                    break;
                case "Forward":
                    Driver.Navigate().Forward();
                    break;
                case "Go To URL":
                    Driver.Navigate().GoToUrl(step.Parameters[1]);
                    break;
                case "Refresh":
                    Driver.Navigate().Refresh();
                    break;
            }
        }

        private void SelectAndDeselect(IWebElement element, Step step)
        {
            SelectElement select = new SelectElement(element);

            switch (step.Parameters[0])
            {
                case "Select By Index":
                    var selectIndex = Convert.ToInt32(step.Parameters[1]);
                    select.SelectByIndex(selectIndex);
                    break;
                case "Select By Text":
                    select.SelectByText(step.Parameters[1]);
                    break;
                case "Select By Value":
                    select.SelectByValue(step.Parameters[1]);
                    break;
                case "Deselect All":
                    select.DeselectAll();
                    break;
                case "Deselect By Index":
                    var deselectIndex = Convert.ToInt32(step.Parameters[1]);
                    select.DeselectByIndex(deselectIndex);
                    break;
                case "Deselect By Text":
                    select.DeselectByText(step.Parameters[1]);
                    break;
                case "Deselect By Value":
                    select.DeselectByValue(step.Parameters[1]);
                    break;
            }

        }

        private void Wait(Step step)
        {
            var time = Convert.ToInt32(step.Parameters[0]);
            if (step.Parameters[1] == "Seconds")
            {
                time = time * 1000;
            }
            Thread.Sleep(time);
        }

        private void WaitFor(IWebElement element, Step step)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            switch (step.Parameters[0])
            {
                case "Alert To Be Present":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
                    break;
                case "Element Text To Be":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(element, step.Parameters[1]));
                    break;
                case "Element To Be Clickable":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
                    break;
                case "Element To Be Selected":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeSelected(element));
                    break;
                case "Element To Be Visible":
                    ElementToBeVisible(wait, step);
                    break;
                case "Text To Be Present":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(element, step.Parameters[2]));
                    break;
                case "Element To Exist":
                    ElementToExist(wait, step);
                    break;
            }
        }

        private void ElementToBeVisible(WebDriverWait wait, Step step)
        {
            switch (step.Parameters[1])
            {
                case "By Class Name":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                        .ElementIsVisible(By.ClassName(step.Parameters[2])));
                    break;

                case "By CSS Selector":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                        .ElementIsVisible(By.CssSelector(step.Parameters[2])));
                    break;

                case "By ID":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                        .ElementIsVisible(By.Id(step.Parameters[2])));
                    break;

                case "By Link text":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                        .ElementIsVisible(By.LinkText(step.Parameters[2])));
                    break;

                case "By Name":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                        .ElementIsVisible(By.Name(step.Parameters[2])));
                    break;

                case "By Partial Link":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                        .ElementIsVisible(By.PartialLinkText(step.Parameters[2])));
                    break;

                case "By Tag Name":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                        .ElementIsVisible(By.TagName(step.Parameters[2])));
                    break;

                case "By XPath":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                        .ElementIsVisible(By.XPath(step.Parameters[2])));
                    break;
            } 
        }

        private void ElementToExist(WebDriverWait wait, Step step)
        {
            switch (step.Parameters[1])
            {
                case "By Class Name":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName(step.Parameters[2])));
                    break;

                case "By CSS Selector":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector(step.Parameters[2])));
                    break;

                case "By ID":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id(step.Parameters[2])));
                    break;

                case "By Link text":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.LinkText(step.Parameters[2])));
                    break;

                case "By Name":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Name(step.Parameters[2])));
                    break;

                case "By Partial Link":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.PartialLinkText(step.Parameters[2])));
                    break;

                case "By Tag Name":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.TagName(step.Parameters[2])));
                    break;

                case "By XPath":
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(step.Parameters[2])));
                    break;
            }
        }
    }
}
