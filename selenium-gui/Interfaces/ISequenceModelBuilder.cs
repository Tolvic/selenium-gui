using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using selenium_gui.Models;

namespace selenium_gui.Interfaces
{
    public interface ISequenceModelBuilder
    {
        ISequence Build(List<Step> steps);
    }
}
