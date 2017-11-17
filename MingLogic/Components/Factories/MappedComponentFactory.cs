namespace MingLogic
{
    using System.Collections.Generic;

    public class MappedComponentFactory
    {
        public IComponentFactory ComponentFactory { get; set; }

        public Dictionary<string, string> PortMapping { get; set; }
    }
}