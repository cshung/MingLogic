namespace MingLogic
{
    using System.Collections.Generic;

    public class ProbeFactory : IComponentFactory
    {
        private Bus inBus;
        private HashSet<Bus> ports;

        public ProbeFactory()
        {
            this.inBus = new Bus { Name = "in", Width = 1 };
            this.ports = new HashSet<Bus>();
            this.ports.Add(this.inBus);
        }

        public string Name
        {
            get;
            set;
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
            return new Probe(this.Name);
        }

        public bool Check(Dictionary<string, IComponentFactory> componentRepository)
        {
            return true;
        }
    }
}
