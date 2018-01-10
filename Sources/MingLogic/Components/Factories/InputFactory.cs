namespace MingLogic
{
    using System;
    using System.Collections.Generic;

    public class InputFactory : IComponentFactory
    {
        public ISet<string> Ports
        {
            get
            {
                return new HashSet<string> { "out" };
            }
        }

        public List<Tuple<int, bool>> Inputs
        {
            get;
            set;
        }

        public IComponent Build(Dictionary<string, IComponentFactory> componentRepository)
        {
            return new Input(this.Inputs);
        }

        public bool Check(Dictionary<string, IComponentFactory> componentRepository)
        {
            return true;
        }
    }
}
