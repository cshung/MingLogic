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
            Project project = JsonConvert.DeserializeObject<Project>(File.ReadAllText("project.json"));

            var componentRepository = new Dictionary<string, IComponentFactory>
            {
                { "nand", new NandFactory() },
            };

            foreach (string component in project.Components)
            {
                string componentDefinition = File.ReadAllText(component + ".json");
                CompositeComponentFactory componentFactory = JsonConvert.DeserializeObject<CompositeComponentFactory>(componentDefinition);
                componentRepository.Add(component, componentFactory);
            }

            foreach (string input in project.Inputs)
            {
                string inputDefinition = File.ReadAllText(input + ".json");
                InputFactory inputFactory = JsonConvert.DeserializeObject<InputFactory>(inputDefinition);
                componentRepository.Add(input, inputFactory);
            }

            foreach (string probe in project.Probes)
            {
                componentRepository.Add(probe, new ProbeFactory { Name = probe });
            }

            IComponentFactory testBenchFactory = componentRepository["testBench"];
            if (!testBenchFactory.Check(componentRepository))
            {
                Console.WriteLine("This circuit definition is invalid");
                return;
            }

            IComponent testBench = testBenchFactory.Build(componentRepository);
            Circuit circuit = new Circuit();
            testBench.Build(new Dictionary<string, int>(), circuit);
            circuit.Run();
        }
    }
}
