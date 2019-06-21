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
        private readonly IDriverModelBuilder _driverModelBuilder;

        public SequenceModelBuilder(IDriverModelBuilder driverModelBuilder)
        {
            _driverModelBuilder = driverModelBuilder;
        }

        public ISequence Build(List<Step> steps)
        {
            return new Sequence
            {
                Driver = _driverModelBuilder.Build(),
                Steps = steps
            };
        }
    }
}
