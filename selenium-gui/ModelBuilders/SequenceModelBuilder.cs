using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using selenium_gui.Interfaces;
using selenium_gui.Models;

namespace selenium_gui.ModelBuilders
{
    public class SequenceModelBuilder : ISequenceModelBuilder
    {

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
    }
}
