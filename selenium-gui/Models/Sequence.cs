using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

            var sequence = new Sequence
            {
                Steps = steps
            };
        }
    }
}
