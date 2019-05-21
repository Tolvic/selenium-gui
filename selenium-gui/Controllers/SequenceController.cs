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
            if (string.IsNullOrWhiteSpace(sequenceData))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(sequenceData));
            }

            List<string> jsonSteps = JsonConvert.DeserializeObject<List<string>>(sequenceData);

            List<Step> steps = new List<Step>();


            foreach (var step in jsonSteps)
            {
                steps.Add(JsonConvert.DeserializeObject<Step>(step));
            }

            var sequence = new Sequence
            {
                Steps = steps
            };

            var driver = new ChromeDriver(@"C:\Projects\selenium-gui\drivers");

            foreach (Step step in sequence.Steps)
            {
                driver.Navigate().GoToUrl(step.Parameters[0]);
            }

            return Ok();
        }
    }
}
