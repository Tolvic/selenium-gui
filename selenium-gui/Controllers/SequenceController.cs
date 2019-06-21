using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using selenium_gui.Interfaces;
using selenium_gui.Models;
using ISequence = Microsoft.EntityFrameworkCore.Metadata.ISequence;

namespace selenium_gui.Controllers
{
    public class SequenceController : Controller
    {
        private readonly ISequenceModelBuilder _sequenceModelBuilder;

        public SequenceController(ISequenceModelBuilder sequenceModelBuilder)
        {
            _sequenceModelBuilder = sequenceModelBuilder;
        }

        [HttpPost]
        public IActionResult Run([FromBody] List<Step> sequenceData)
        {

            var sequence = _sequenceModelBuilder.Build(sequenceData);

            var sequenceResult = sequence.Run();

            return Ok(sequenceResult);
        }

        public IActionResult GetStepTemplate()
        {
            return PartialView("_step");
        }
    }
}
