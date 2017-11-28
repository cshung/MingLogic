namespace MingLogic
{
    using System.Collections.Generic;

    public class ClockFactory : IComponentFactory
    {
        public IComponent Build(Dictionary<string, IComponentFactory> componentRepository)
        {
            return new Clock();
        }
    }
}
