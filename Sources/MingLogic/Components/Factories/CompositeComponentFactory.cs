namespace MingLogic
{
    using System.Collections.Generic;
    using System.Linq;

    public class CompositeComponentFactory : IComponentFactory
    {
        public ISet<string> Ports { get; set; }

        public ISet<string> Signals { get; set; }

        public List<MappedComponentFactory> MappedComponentFactories { get; set; }

        public IComponent Build(Dictionary<string, IComponentFactory> componentRepository)
        {
            CompositeComponent result = new CompositeComponent
            {
                Ports = new HashSet<string>(this.Ports),
                Signals = new HashSet<string>(this.Signals),
                MappedComponents = new List<MappedComponent>()
            };
            foreach (var mappedComponentFactory in this.MappedComponentFactories)
            {
                MappedComponent mappedComponent = new MappedComponent();
                mappedComponent.PortMapping = new Dictionary<string, string>(mappedComponentFactory.PortMapping);
                IComponentFactory mappedComponentFactory = componentRepository[mappedComponentFactory.ComponentFactoryName];
                mappedComponent.Component = mappedComponentFactory.Build(componentRepository);
                result.MappedComponents.Add(mappedComponent);
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

                ISet<string> requiredPorts = componentFactory.Ports;
                if (!requiredPorts.SetEquals(mappedComponentFactory.PortMapping.Keys))
                {
                    return false;
                }

                HashSet<string> portSignals = new HashSet<string>(this.Ports);
                portSignals.UnionWith(this.Signals);
                if (portSignals.Count != this.Ports.Count + this.Signals.Count)
                {
                    return false;
                }

                foreach (var mappingTarget in mappedComponentFactory.PortMapping.Values)
                {
                    if (!portSignals.Contains(mappingTarget))
                    {
                        return false;
                    }
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