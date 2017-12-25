namespace MingLogic
{
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    public class Program
    {
        public static void Main(string[] args)
        {
            var componentRepository = new Dictionary<string, IComponentFactory>
            {
                { "Nand", new NandFactory() },
                { "Clock", new ClockFactory() },
                { "Probe", new ProbeFactory() },
            };

            string[] components = new string[] { "andGate", "testBenchFactory" };

            foreach (string component in components)
            {
                string componentDefinition = File.ReadAllText(component + ".json");
                CompositeComponentFactory componentFactory = JsonConvert.DeserializeObject<CompositeComponentFactory>(componentDefinition);
                componentRepository.Add(component, componentFactory);
            }

            IComponentFactory testBenchFactory = componentRepository["testBenchFactory"];
            IComponent testBench = testBenchFactory.Build(componentRepository);
            Circuit circuit = new Circuit();
            testBench.Build(new Dictionary<string, int>(), circuit);
            circuit.Run();
        }
    }
}
