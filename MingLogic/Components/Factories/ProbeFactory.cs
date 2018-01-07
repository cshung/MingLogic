namespace MingLogic
{
    using System.Collections.Generic;

    public class ProbeFactory : IComponentFactory
    {
        public string Name
        {
            get;
            set;
        }

        public ISet<string> Ports
        {
            get
            {
                return new HashSet<string> { "in" };
            }
        }

        public IComponent Build(Dictionary<string, IComponentFactory> componentRepository)
        {
            return new Probe(this.Name);
        }

        public bool Check(Dictionary<string, IComponentFactory> componentRepository)
        {
            return true;
        }
    }
}
