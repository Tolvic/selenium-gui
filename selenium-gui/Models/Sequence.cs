using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using selenium_gui.Interfaces;

namespace selenium_gui.Models
{
    public class Sequence : ISequence
    {
        [ExcludeFromCodeCoverage]
        public List<Step> Steps { get; set; }

        [ExcludeFromCodeCoverage]
        public IWebDriver Driver { get; set; }

        public void Run()
        {
            IWebElement element = null;

            foreach (Step step in Steps)
            {
                switch (step.Type)
                {
                    case "Navigate":
                        Navigate(step);
                        break;

                    case "Find Element":
                        element = FindElement(step);
                        break;

                    case "Element Operations":
                        ElementOperations(element, step);
                        break;
                }

            }
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

        private void ElementOperations(IWebElement element, Step step)
        {
            switch (step.Parameters[0])
            {
                case "Click":
                    element?.Click();
                    break;

               case "Clear":
                    element?.Clear();
                    break;

                case "Send Keys":
                    element?.SendKeys(step.Parameters[1]);
                    break;

                case "submit":
                    element?.Clear();
                    break;
            }
        }



    }
}
