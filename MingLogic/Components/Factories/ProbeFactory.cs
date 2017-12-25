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

        public IComponent Build(Dictionary<string, IComponentFactory> componentRepository)
        {
            return new Probe(this.Name);
        }
    }
}
