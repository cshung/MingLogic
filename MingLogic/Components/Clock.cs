using System;
using System.Collections.Generic;
using System.Linq;

namespace MingLogic
{
    class Clock : IComponent
    {
        public Clock()
        {
            this.Ports = new HashSet<String> { "out" };
        }

        public ISet<string> Ports
        {
            get;
            private set;
        }

        public void Build(Dictionary<string, int> portMapping, Circuit circuit)
        {
            circuit.Clock(portMapping["out"]);
        }
    }
}