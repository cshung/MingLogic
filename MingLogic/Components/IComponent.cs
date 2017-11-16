using System;
using System.Collections.Generic;
using System.Linq;

namespace MingLogic
{
    interface IComponent
    {
        ISet<string> Ports { get; }
        void Build(Dictionary<String, int> portMapping, Circuit circuit);
    }
}