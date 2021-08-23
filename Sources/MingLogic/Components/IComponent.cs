namespace MingLogic
{
    using System.Collections.Generic;

    public interface IComponent
    {
        ISet<Net> Ports { get; }

        void Build(Dictionary<Net, int> portMapping, Circuit circuit);
    }
}