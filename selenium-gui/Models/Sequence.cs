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
            foreach (Step step in Steps)
            {
                Driver.Navigate().GoToUrl(step.Parameters[0]);
            }
        }
    }
}
