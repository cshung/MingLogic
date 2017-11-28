namespace MingLogic
{
    using System.Collections.Generic;

    public interface IComponentFactory
    {
        IComponent Build(Dictionary<string, IComponentFactory> componentRepository);
    }
}
