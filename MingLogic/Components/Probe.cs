using System;
using System.Collections.Generic;
using System.Linq;

namespace MingLogic
{
    class Probe : IComponent
    {
        public Probe()
        {
            this.Ports = new HashSet<String> { "in" };
        }

        public ISet<string> Ports
        {
            get;
            private set;
        }

        public void Build(Dictionary<string, int> portMapping, Circuit circuit)
        {
            circuit.SetProbeProp(portMapping["in"]);
        }
    }
}