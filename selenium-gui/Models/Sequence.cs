using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;

namespace selenium_gui.Models
{
    public class Sequence
    {
        public List<Step> Steps { get; set; }

        public Sequence Build(string sequenceData)
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

            return new Sequence
            {
                Steps = steps
            };
        }

        public void Run()
        {
            var driver = new ChromeDriver(@"C:\Projects\selenium-gui\drivers");

            foreach (Step step in Steps)
            {
                driver.Navigate().GoToUrl(step.Parameters[0]);
            }
        }
    }
}
