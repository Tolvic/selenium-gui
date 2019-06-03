using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace selenium_gui.Interfaces
{
    public interface IDriverModelBuilder
    {
        IWebDriver Build();
    }
}
