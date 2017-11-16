using System;
using System.Collections.Generic;
using System.Linq;

namespace MingLogic
{
    class TickEvent : ScheduledEvent
    {
        private int signalIndex;

        public TickEvent(int signalIndex, int time)
        {
            this.signalIndex = signalIndex;
            this.Time = time;
        }

        public override void Process(Circuit circuit)
        {
            circuit.Tick(signalIndex, this.Time);
        }
    }

    class Circuit
    {
        private List<List<SignalChangedHandler>> props = new List<List<SignalChangedHandler>>();
        private List<int> clocks = new List<int>();

        private bool[] signals;
        private List<ScheduledEvent> eventQueue;

        internal int GetSignalNumber()
        {
            props.Add(new List<SignalChangedHandler>());
            return props.Count - 1;
        }

        internal void SetAndProp(int a, int b, int o)
        {
            props[a].Add(new AndGateInputSignalChangedHandler(a, b, o));
            props[b].Add(new AndGateInputSignalChangedHandler(a, b, o));
        }

        internal void Clock(int o)
        {
            this.clocks.Add(o);
        }

        internal void SetProbeProp(int i)
        {
            this.props[i].Add(new ProbeInputSignalChangedHandler(i));
        }

        public void Run()
        {
            this.signals = new bool[this.props.Count];
            this.eventQueue = new List<ScheduledEvent>();
            foreach (var clock in this.clocks)
            {
                eventQueue.Add(new TickEvent(0, clock));
            }
            for (int i = 0; i < 100; i++)
            {
                ScheduledEvent e = eventQueue[0];
                eventQueue.RemoveAt(0);
                e.Process(this);
            }
        }

        internal void Tick(int signalIndex, int time)
        {
            this.signals[signalIndex] = !this.signals[signalIndex];
            foreach (var prop in this.props[signalIndex])
            {
                prop.Run(this, time);
            }
            this.eventQueue.Add(new TickEvent(signalIndex, time + 10));
            this.eventQueue = this.eventQueue.OrderBy(e => e.Time).ToList();
        }

        internal void AndProp(int a, int b, int o, int time)
        {
            this.eventQueue.Add(new AndScheduledEvent(a, b, o, time + 2));
            this.eventQueue = this.eventQueue.OrderBy(e => e.Time).ToList();
        }

        internal void And(int a, int b, int o, int time)
        {
            this.signals[o] = this.signals[a] && this.signals[b];
            foreach (var prop in this.props[o])
            {
                prop.Run(this, time);
            }
        }

        internal void ProbeProp(int i, int time)
        {
            Console.WriteLine("Signal " + i + " at time " + time + " is " + signals[i]);
        }
    }

    interface IComponent
    {
        ISet<string> Ports { get; }
        void Build(Dictionary<String, int> portMapping, Circuit circuit);
    }

    class AndComponent : IComponent
    {
        public AndComponent()
        {
            this.Ports = new HashSet<String> { "a", "b", "out" };
        }

        public ISet<string> Ports
        {
            get;
            private set;
        }

        public void Build(Dictionary<string, int> portMapping, Circuit circuit)
        {
            circuit.SetAndProp(portMapping["a"], portMapping["b"], portMapping["out"]);
        }
    }

    class Clock : IComponent
    {
        public Clock()
        {
            this.Ports = new HashSet<String> { "out" };
        }

        public ISet<string> Ports
        {
            get;
            private set;
        }

        public void Build(Dictionary<string, int> portMapping, Circuit circuit)
        {
            circuit.Clock(portMapping["out"]);
        }
    }

    class Probe : IComponent
    {
        public Probe()
        {
            this.Ports = new HashSet<String> { "in" };
        }

        public ISet<string> Ports
        {
            get;
            private set;
        }

        public void Build(Dictionary<string, int> portMapping, Circuit circuit)
        {
            circuit.SetProbeProp(portMapping["in"]);
        }
    }

    class MappedComponent
    {
        public IComponent Component { get; set; }
        public Dictionary<string, string> PortMapping { get; set; }
    }

    class CompositeComponent : IComponent
    {
        public ISet<string> Ports { get; set; }
        public ISet<string> Signals { get; set; }
        public List<MappedComponent> MappedComponents { get; set; }

        public void Build(Dictionary<string, int> portMapping, Circuit circuit)
        {
            foreach (var signal in this.Signals)
            {
                portMapping.Add(signal, circuit.GetSignalNumber());
            }
            foreach (var mappedComponent in this.MappedComponents)
            {
                Dictionary<string, int> componentPortMapping = new Dictionary<string, int>();
                foreach (var kvp in mappedComponent.PortMapping)
                {
                    componentPortMapping.Add(kvp.Key, portMapping[kvp.Value]);
                }
                mappedComponent.Component.Build(componentPortMapping, circuit);
            }
        }
    }

    class Program
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
