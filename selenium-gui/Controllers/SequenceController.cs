using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;
using selenium_gui.Models;

namespace selenium_gui.Controllers
{
    public class SequenceController : Controller
    {
        private readonly ISequence _sequence;

        public SequenceController(ISequence sequence)
        {
            _sequence = sequence;
        }
        public IActionResult Run(string sequenceData)
        {
            var sequence = new selenium_gui.Models.Sequence().Build(sequenceData);

            sequence.Run();

            return Ok();
        }
    }
}
