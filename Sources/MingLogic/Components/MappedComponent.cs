namespace MingLogic
{
    using System.Collections.Generic;

    public class MappedComponent
    {
        public IComponent Component { get; set; }

        public Dictionary<string, string> PortMapping { get; set; }
    }
}