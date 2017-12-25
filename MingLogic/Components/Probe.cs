namespace MingLogic
{
    using System;
    using System.Collections.Generic;

    public class Probe : IComponent
    {
        private string name;

        public Probe(string name)
        {
            this.name = name;
            this.Ports = new HashSet<string> { "in" };
        }

        public ISet<string> Ports
        {
            get;
            private set;
        }

        public void Build(Dictionary<string, int> portMapping, Circuit circuit)
        {
            circuit.RegisterProbe(portMapping["in"], this.name);
        }
    }
}