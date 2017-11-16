namespace MingLogic
{
    using System;
    using System.Collections.Generic;

    public class Probe : IComponent
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
            circuit.RegisterProbe(portMapping["in"]);
        }
    }
}