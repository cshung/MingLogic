namespace MingLogic
{
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    public class Program
    {
        public static void Main(string[] args)
        {
            string[] components = new string[] { "and", "or", "not", "xor", "3nand", "dff", "counter", "divider", "testBench" };
            string[] inputs = new string[] { "input", "clock" };
            string[] probes = new string[] { "out" };

            var componentRepository = new Dictionary<string, IComponentFactory>
            {
                { "nand", new NandFactory() },
            };

            foreach (string component in components)
            {
                string componentDefinition = File.ReadAllText(component + ".json");
                CompositeComponentFactory componentFactory = JsonConvert.DeserializeObject<CompositeComponentFactory>(componentDefinition);
                componentRepository.Add(component, componentFactory);
            }

            foreach (string input in inputs)
            {
                string inputDefinition = File.ReadAllText(input + ".json");
                InputFactory inputFactory = JsonConvert.DeserializeObject<InputFactory>(inputDefinition);
                componentRepository.Add(input, inputFactory);
            }

            foreach (string probe in probes)
            {
                componentRepository.Add(probe, new ProbeFactory { Name = probe });
            }

            IComponentFactory testBenchFactory = componentRepository["testBench"];
            IComponent testBench = testBenchFactory.Build(componentRepository);
            Circuit circuit = new Circuit();
            testBench.Build(new Dictionary<string, int>(), circuit);
            circuit.Run();
        }
    }
}
