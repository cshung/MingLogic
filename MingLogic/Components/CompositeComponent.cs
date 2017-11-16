namespace MingLogic
{
    using System.Collections.Generic;

    public class CompositeComponent : IComponent
    {
        public ISet<string> Ports { get; set; }
        public ISet<string> Signals { get; set; }
        public List<MappedComponent> MappedComponents { get; set; }

        public void Build(Dictionary<string, int> portMapping, Circuit circuit)
        {
            foreach (var signal in this.Signals)
            {
                portMapping.Add(signal, circuit.GetSignalNumber());
            }
            foreach (var mappedComponent in this.MappedComponents)
            {
                Dictionary<string, int> componentPortMapping = new Dictionary<string, int>();
                foreach (var kvp in mappedComponent.PortMapping)
                {
                    componentPortMapping.Add(kvp.Key, portMapping[kvp.Value]);
                }
                mappedComponent.Component.Build(componentPortMapping, circuit);
            }
        }
    }
}