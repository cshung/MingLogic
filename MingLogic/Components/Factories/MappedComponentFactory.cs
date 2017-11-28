namespace MingLogic
{
    using System.Collections.Generic;

    public class MappedComponentFactory
    {
        public string ComponentFactoryName { get; set; }

        public Dictionary<string, string> PortMapping { get; set; }
    }
}