namespace MingLogic
{
    using System;
    using System.Collections.Generic;

    public class Clock : IComponent
    {
        public Clock()
        {
            this.Ports = new HashSet<string> { "out" };
        }

        public ISet<string> Ports
        {
            get;
            private set;
        }

        public void Build(Dictionary<string, int> portMapping, Circuit circuit)
        {
            circuit.RegisterClock(portMapping["out"]);
        }
    }
}