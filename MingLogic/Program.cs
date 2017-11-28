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
                        ComponentFactoryName = "Nand"
                    },
                    new MappedComponentFactory
                    {
                        PortMapping = new Dictionary<string, string> { { "a", "n" }, { "b", "n" }, { "out", "out" } },
                        ComponentFactoryName = "Nand"
                    }
                }
            };

            CompositeComponentFactory testBenchFactory = new CompositeComponentFactory
            {
                Ports = new HashSet<string> { },
                Signals = new HashSet<string> { "clock", "probe" },
                MappedComponentFactories = new List<MappedComponentFactory>
                {
                    new MappedComponentFactory
                    {
                        PortMapping = new Dictionary<string, string> { { "out", "clock" } },
                        ComponentFactoryName = "Clock",
                    },
                    new MappedComponentFactory
                    {
                        PortMapping = new Dictionary<string, string> { { "a", "clock" }, { "b", "clock" }, { "out", "probe" } },
                        ComponentFactoryName = "And"
                    },
                    new MappedComponentFactory
                    {
                        PortMapping = new Dictionary<string, string> { { "in", "probe" } },
                        ComponentFactoryName = "Probe"
                    }
                }
            };

            var componentRepository = new Dictionary<string, IComponentFactory>
            {
                { "And", andGate },
                { "Nand", new NandFactory() },
                { "Clock", new ClockFactory() },
                { "Probe", new ProbeFactory() },
            };

            IComponent testBench = testBenchFactory.Build(componentRepository);
            Circuit circuit = new Circuit();
            testBench.Build(new Dictionary<string, int>(), circuit);
            circuit.Run();
        }
    }
}
