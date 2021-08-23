namespace MingLogic
{
    using System;
    using System.Collections.Generic;

    public class NandGate : IComponent
    {
        private Net a;
        private Net b;
        private Net outNet;
        private HashSet<Net> ports;

        public NandGate()
        {
            this.ports = new HashSet<Net>();
            this.a = new Net { Name = "a", Index = 0 };
            this.b = new Net { Name = "b", Index = 0 };
            this.outNet = new Net { Name = "out", Index = 0 };
            this.ports.Add(this.a);
            this.ports.Add(this.b);
            this.Ports.Add(this.outNet);
        }

        public ISet<Net> Ports
        {
            get { return this.ports; }
        }

        public void Build(Dictionary<Net, int> portMapping, Circuit circuit)
        {
            circuit.RegisterNandGate(portMapping[this.a], portMapping[this.b], portMapping[this.outNet]);
        }
    }
}