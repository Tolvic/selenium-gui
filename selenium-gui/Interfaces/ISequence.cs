using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using selenium_gui.Models;

namespace selenium_gui.Interfaces
{
    interface ISequence
    {
        Sequence Build(string sequenceData);

        void Run();
    }
}
