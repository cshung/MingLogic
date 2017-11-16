namespace MingLogic
{
    using System.Collections.Generic;

    public class Program
    {
        static void Main(string[] args)
        {
            CompositeComponent board = new CompositeComponent();
            board.Ports = new HashSet<string>();
            board.Signals = new HashSet<string>
            {
                "c",
                "o"
            };
            board.MappedComponents = new List<MappedComponent>
            {
                new MappedComponent
                {
                    Component = new Clock(),
                    PortMapping = new Dictionary<string, string>
                    {
                        { "out", "c" },
                    }
                },
                new MappedComponent
                {
                    Component = new AndComponent(),
                    PortMapping = new Dictionary<string, string>
                    {
                        { "a", "c" },
                        { "b", "c" },
                        { "out", "o" }
                    }
                },
                new MappedComponent
                {
                    Component = new Probe(),
                    PortMapping = new Dictionary<string, string>
                    {
                        { "in", "o" }
                    }
                }
            };
            Circuit circuit = new Circuit();
            board.Build(new Dictionary<string, int>(), circuit);
            circuit.Run();
        }
    }
}
