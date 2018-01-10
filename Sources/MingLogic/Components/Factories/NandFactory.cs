namespace MingLogic
{
    using System.Collections.Generic;

    public class NandFactory : IComponentFactory
    {
        public ISet<string> Ports
        {
            get
            {
                return new HashSet<string> { "a", "b", "out" };
            }
        }

        public IComponent Build(Dictionary<string, IComponentFactory> componentRepository)
        {
            return new NandGate();
        }

        public bool Check(Dictionary<string, IComponentFactory> componentRepository)
        {
            return true;
        }
    }
}
