namespace MingLogic
{
    using System;
    using System.Collections.Generic;

    public interface IComponent
    {
        ISet<string> Ports { get; }
        void Build(Dictionary<String, int> portMapping, Circuit circuit);
    }
}