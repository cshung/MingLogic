namespace MingLogic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    public class Program
    {
        public static void Main(string[] args)
        {
            string[] components = new string[] { "andGate", "testBenchFactory" };
            string[] inputs = new string[] { "input" };
            string[] probes = new string[] { "probeOut" };

            var componentRepository = new Dictionary<string, IComponentFactory>
            {
                { "Nand", new NandFactory() },
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

            IComponentFactory testBenchFactory = componentRepository["testBenchFactory"];
            IComponent testBench = testBenchFactory.Build(componentRepository);
            Circuit circuit = new Circuit();
            testBench.Build(new Dictionary<string, int>(), circuit);
            circuit.Run();
        }
    }
}
