namespace MingLogic
{
    using System.Collections.Generic;

    public class MappedComponentFactory
    {
        public string ComponentFactoryName { get; set; }

        public List<PortMapping> PortMapping { get; set; }

        public int Count { get; set; }
    }
}