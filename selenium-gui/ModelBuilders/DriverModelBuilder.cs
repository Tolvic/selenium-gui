using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using selenium_gui.Interfaces;

namespace selenium_gui.ModelBuilders
{
    public class DriverModelBuilder : IDriverModelBuilder
    {
        public IWebDriver Build()
        {
            return new ChromeDriver(@"C:\Projects\selenium-gui\drivers");
        }
    }
}
