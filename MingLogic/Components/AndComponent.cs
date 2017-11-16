namespace MingLogic
{
    using System;
    using System.Collections.Generic;

    public class AndComponent : IComponent
    {
        public AndComponent()
        {
            this.Ports = new HashSet<string> { "a", "b", "out" };
        }

        public ISet<string> Ports
        {
            get;
            private set;
        }

        public void Build(Dictionary<string, int> portMapping, Circuit circuit)
        {
            circuit.RegisterAndGate(portMapping["a"], portMapping["b"], portMapping["out"]);
        }
    }
}