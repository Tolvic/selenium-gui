using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace selenium_gui.Models
{
    [ExcludeFromCodeCoverage]
    public class Step
    {
        public string Type { get; set; }
        public List<string> Parameters { get; set; }
    }
}
