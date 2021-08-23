﻿namespace MingLogic
{
    using System.Collections.Generic;

    public interface IComponentFactory
    {
        ISet<Bus> Ports { get; }

        bool Check(Dictionary<string, IComponentFactory> componentRepository);

        IComponent Build(Dictionary<string, IComponentFactory> componentRepository);
    }
}
