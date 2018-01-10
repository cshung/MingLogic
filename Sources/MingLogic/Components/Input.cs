namespace MingLogic
{
    using System;
    using System.Collections.Generic;

    public class Input : IComponent
    {
        private List<Tuple<int, bool>> inputs;

        public Input(List<Tuple<int, bool>> inputs)
        {
            this.inputs = inputs;
            this.Ports = new HashSet<string> { "out" };
        }

        public ISet<string> Ports
        {
            get;
            private set;
        }

        public void Build(Dictionary<string, int> portMapping, Circuit circuit)
        {
            circuit.RegisterInput(portMapping["out"], this.inputs);
        }
    }
}