using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace selenium_gui.Controllers
{
    public class SequenceController : Controller
    {

        public IActionResult Run(string json)
        {
            var driver = new ChromeDriver(@"C:\Projects\selenium-gui\drivers");

            driver.Navigate().GoToUrl("https://google.com");

            return Ok();
        }
    }
}
