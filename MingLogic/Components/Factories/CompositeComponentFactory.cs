namespace MingLogic
{
    using System.Collections.Generic;

    public class CompositeComponentFactory : IComponentFactory
    {
        public ISet<string> Ports { get; set; }

        public ISet<string> Signals { get; set; }

        public List<MappedComponentFactory> MappedComponentFactories { get; set; }

        public IComponent Build()
        {
            CompositeComponent result = new CompositeComponent
            {
                Ports = new HashSet<string>(this.Ports),
                Signals = new HashSet<string>(this.Signals),
                MappedComponents = new List<MappedComponent>()
            };
            foreach (var mappedComponentfactory in this.MappedComponentFactories)
            {
                MappedComponent mappedComponent = new MappedComponent();
                mappedComponent.PortMapping = new Dictionary<string, string>(mappedComponentfactory.PortMapping);
                mappedComponent.Component = mappedComponentfactory.ComponentFactory.Build();
                result.MappedComponents.Add(mappedComponent);
            }

            return result;
        }
    }
}
