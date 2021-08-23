namespace MingLogic
{
    using System.Collections.Generic;

    public class NandFactory : IComponentFactory
    {
        private Bus a;
        private Bus b;
        private Bus outBus;
        private HashSet<Bus> ports;

        public NandFactory()
        {
            this.a = new Bus { Name = "a", Width = 1 };
            this.b = new Bus { Name = "b", Width = 1 };
            this.outBus = new Bus { Name = "out", Width = 1 };
            this.ports = new HashSet<Bus>();
            this.ports.Add(this.a);
            this.ports.Add(this.b);
            this.ports.Add(this.outBus);
        }

        public ISet<Bus> Ports
        {
            get
            {
                return this.ports;
            }
        }

        public IComponent Build(Dictionary<string, IComponentFactory> componentRepository)
        {
            return new NandGate();
        }

        public bool Check(Dictionary<string, IComponentFactory> componentRepository)
        {
            return true;
        }
    }
}
