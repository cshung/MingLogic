using System;
using System.Collections.Generic;
using System.Linq;

namespace MingLogic
{
    class AndComponent : IComponent
    {
        public AndComponent()
        {
            this.Ports = new HashSet<String> { "a", "b", "out" };
        }

        public ISet<string> Ports
        {
            get;
            private set;
        }

        public void Build(Dictionary<string, int> portMapping, Circuit circuit)
        {
            circuit.SetAndProp(portMapping["a"], portMapping["b"], portMapping["out"]);
        }
    }
}