namespace MingLogic
{
    using System.Collections.Generic;

    public class ProbeFactory : IComponentFactory
    {
        public IComponent Build(Dictionary<string, IComponentFactory> componentRepository)
        {
            return new Probe();
        }
    }
}
