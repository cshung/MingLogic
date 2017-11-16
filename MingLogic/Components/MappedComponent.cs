using System;
using System.Collections.Generic;
using System.Linq;

namespace MingLogic
{
    class MappedComponent
    {
        public IComponent Component { get; set; }
        public Dictionary<string, string> PortMapping { get; set; }
    }
}