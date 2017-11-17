namespace MingLogic
{
    using System.Collections.Generic;

    public class Program
    {
        public static void Main(string[] args)
        {
            CompositeComponentFactory andGate = new CompositeComponentFactory
            {
                Ports = new HashSet<string> { "a", "b", "out" },
                Signals = new HashSet<string> { "n" },
                MappedComponentFactories = new List<MappedComponentFactory>
                {
                    new MappedComponentFactory
                    {
                        PortMapping = new Dictionary<string, string> { { "a", "a" }, { "b", "b" }, { "out", "n" } },
                        ComponentFactory = new NandFactory()
                    },
                    new MappedComponentFactory
                    {
                        PortMapping = new Dictionary<string, string> { { "a", "n" }, { "b", "n" }, { "out", "out" } },
                        ComponentFactory = new NandFactory()
                    }
                }
            };

            CompositeComponentFactory boardFactory = new CompositeComponentFactory
            {
                Ports = new HashSet<string> { },
                Signals = new HashSet<string> { "clock", "probe" },
                MappedComponentFactories = new List<MappedComponentFactory>
                {
                    new MappedComponentFactory
                    {
                        PortMapping = new Dictionary<string, string> { { "out", "clock" } },
                        ComponentFactory = new ClockFactory(),
                    },
                    new MappedComponentFactory
                    {
                        PortMapping = new Dictionary<string, string> { { "a", "clock" }, { "b", "clock" }, { "out", "probe" } },
                        ComponentFactory = andGate,
                    },
                    new MappedComponentFactory
                    {
                        PortMapping = new Dictionary<string, string> { { "in", "probe" } },
                        ComponentFactory = new ProbeFactory()
                    }
                }
            };

            IComponent board = boardFactory.Build();
            Circuit circuit = new Circuit();
            board.Build(new Dictionary<string, int>(), circuit);
            circuit.Run();
        }
    }
}
