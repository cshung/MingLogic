namespace MingLogic
{
    using System;
    using System.Collections.Generic;

    public class InputFactory : IComponentFactory
    {
        private Bus outBus;
        private HashSet<Bus> ports;

        public InputFactory()
        {
            this.outBus = new Bus { Name = "out", Width = 1 };
            this.ports = new HashSet<Bus>();
            this.ports.Add(this.outBus);
        }

        public ISet<Bus> Ports
        {
            get
            {
                return this.ports;
            }
        }

        public List<Tuple<int, bool>> Inputs
        {
            get;
            set;
        }

        public IComponent Build(Dictionary<string, IComponentFactory> componentRepository)
        {
            return new Input(this.Inputs);
        }

        public bool Check(Dictionary<string, IComponentFactory> componentRepository)
        {
            return true;
        }
    }
}
