namespace MingLogic
{
    using System;
    using System.Collections.Generic;

    public interface IComponent
    {
        ISet<string> Ports { get; }

        void Build(Dictionary<string, int> portMapping, Circuit circuit);
    }
}