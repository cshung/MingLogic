namespace MingLogic
{
    using System.Collections.Generic;

    public class Probe : IComponent
    {
        private string name;
        private Net inNet;
        private HashSet<Net> ports;

        public Probe(string name)
        {
            this.name = name;
            this.inNet = new Net { Name = "in", Index = 0 };
            this.ports = new HashSet<Net>();
            this.ports.Add(this.inNet);
        }

        public ISet<Net> Ports
        {
            get { return this.ports; }
        }

        public void Build(Dictionary<Net, int> portMapping, Circuit circuit)
        {
            circuit.RegisterProbe(portMapping[this.inNet], this.name);
        }
    }
}