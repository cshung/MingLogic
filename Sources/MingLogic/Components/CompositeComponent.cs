namespace MingLogic
{
    using System.Collections.Generic;

    public class CompositeComponent : IComponent
    {
        public ISet<Net> Ports { get; set; }

        public ISet<Net> Signals { get; set; }

        public List<MappedComponent> MappedComponents { get; set; }

        public void Build(Dictionary<Net, int> portMapping, Circuit circuit)
        {
            foreach (var signal in this.Signals)
            {
                portMapping.Add(signal, circuit.GetSignalNumber());
            }

            foreach (var mappedComponent in this.MappedComponents)
            {
                Dictionary<Net, int> componentPortMapping = new Dictionary<Net, int>();
                foreach (var kvp in mappedComponent.PortMapping)
                {
                    componentPortMapping.Add(kvp.Key, portMapping[kvp.Value]);
                }

                mappedComponent.Component.Build(componentPortMapping, circuit);
            }
        }
    }
}