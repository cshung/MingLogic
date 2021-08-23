namespace MingLogic
{
    using System.Collections.Generic;
    using System.Linq;

    public class CompositeComponentFactory : IComponentFactory
    {
        public ISet<Bus> Ports { get; set; }

        public ISet<Bus> Signals { get; set; }

        public List<MappedComponentFactory> MappedComponentFactories { get; set; }

        public IComponent Build(Dictionary<string, IComponentFactory> componentRepository)
        {
            CompositeComponent result = new CompositeComponent
            {
                Ports = new HashSet<Net>(),
                Signals = new HashSet<Net>(),
                MappedComponents = new List<MappedComponent>()
            };
            foreach (Bus port in this.Ports)
            {
                for (int i = 0; i < port.Width; i++)
                {
                    result.Ports.Add(new Net { Name = port.Name, Index = i });
                }
            }

            foreach (Bus signal in this.Signals)
            {
                for (int i = 0; i < signal.Width; i++)
                {
                    result.Signals.Add(new Net { Name = signal.Name, Index = i });
                }
            }

            foreach (var mappedComponentFactory in this.MappedComponentFactories)
            {
                for (int i = 0; i < mappedComponentFactory.Count; i++)
                {
                    MappedComponent mappedComponent = new MappedComponent();
                    mappedComponent.PortMapping = new Dictionary<Net, Net>();
                    foreach (var mapping in mappedComponentFactory.PortMapping)
                    {
                        // TODO: Implement an expression interpreter to perform the port mapping
                    }

                    IComponentFactory componentFactory = componentRepository[mappedComponentFactory.ComponentFactoryName];
                    mappedComponent.Component = componentFactory.Build(componentRepository);
                    result.MappedComponents.Add(mappedComponent);
                }
            }

            return result;
        }

        public bool Check(Dictionary<string, IComponentFactory> componentRepository)
        {
            foreach (var mappedComponentFactory in this.MappedComponentFactories)
            {
                IComponentFactory componentFactory;
                if (!componentRepository.TryGetValue(mappedComponentFactory.ComponentFactoryName, out componentFactory))
                {
                    return false;
                }

                HashSet<Bus> portSignals = new HashSet<Bus>(this.Ports);
                portSignals.UnionWith(this.Signals);
                if (portSignals.Count != this.Ports.Count + this.Signals.Count)
                {
                    return false;
                }

                if (!componentFactory.Check(componentRepository))
                {
                    return false;
                }
            }

            return true;
        }
    }
}