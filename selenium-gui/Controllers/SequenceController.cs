using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using selenium_gui.Models;

namespace selenium_gui.Controllers
{
    public class SequenceController : Controller
    {

        public IActionResult Run(string sequenceData)
        {
            var sequence = new selenium_gui.Models.Sequence().Build(sequenceData);

            var driver = new ChromeDriver(@"C:\Projects\selenium-gui\drivers");

            foreach (Step step in sequence.Steps)
            {
                driver.Navigate().GoToUrl(step.Parameters[0]);
            }

            return Ok();
        }
    }
}
