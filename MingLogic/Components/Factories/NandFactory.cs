namespace MingLogic
{
    using System.Collections.Generic;

    public class NandFactory : IComponentFactory
    {
        public IComponent Build(Dictionary<string, IComponentFactory> componentRepository)
        {
            return new NandGate();
        }
    }
}
