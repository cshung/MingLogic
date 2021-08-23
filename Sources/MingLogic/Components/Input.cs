namespace MingLogic
{
    using System;
    using System.Collections.Generic;

    public class Input : IComponent
    {
        private List<Tuple<int, bool>> inputs;
        private Net outNet;
        private HashSet<Net> ports;

        public Input(List<Tuple<int, bool>> inputs)
        {
            this.inputs = inputs;
            this.outNet = new Net { Name = "out", Index = 0 };
            this.ports = new HashSet<Net>();
        }

        public ISet<Net> Ports
        {
            get { return this.ports; }
        }

        public void Build(Dictionary<Net, int> portMapping, Circuit circuit)
        {
            circuit.RegisterInput(portMapping[this.outNet], this.inputs);
        }
    }
}