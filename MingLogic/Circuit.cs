namespace MingLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Circuit
    {
        private List<List<SignalChangedHandler>> signalChangedHandlers = new List<List<SignalChangedHandler>>();
        private List<int> clocks = new List<int>();

        private bool[] signals;
        private List<ScheduledEvent> eventQueue;

        public int GetSignalNumber()
        {
            signalChangedHandlers.Add(new List<SignalChangedHandler>());
            return signalChangedHandlers.Count - 1;
        }

        public void RegisterAndGate(int a, int b, int o)
        {
            signalChangedHandlers[a].Add(new AndGateInputSignalChangedHandler(a, b, o));
            signalChangedHandlers[b].Add(new AndGateInputSignalChangedHandler(a, b, o));
        }

        public void RegisterClock(int o)
        {
            this.clocks.Add(o);
        }

        public void RegisterProbe(int i)
        {
            this.signalChangedHandlers[i].Add(new ProbeInputSignalChangedHandler(i));
        }

        public void Run()
        {
            this.signals = new bool[this.signalChangedHandlers.Count];
            this.eventQueue = new List<ScheduledEvent>();
            foreach (var clock in this.clocks)
            {
                eventQueue.Add(new TickScheduledEvent(0, clock));
            }
            for (int i = 0; i < 100; i++)
            {
                ScheduledEvent e = eventQueue[0];
                eventQueue.RemoveAt(0);
                e.Process(this);
            }
        }

        public void Tick(int signalIndex, int time)
        {
            this.signals[signalIndex] = !this.signals[signalIndex];
            foreach (var prop in this.signalChangedHandlers[signalIndex])
            {
                prop.Run(this, time);
            }
            this.eventQueue.Add(new TickScheduledEvent(signalIndex, time + 10));
            this.eventQueue = this.eventQueue.OrderBy(e => e.Time).ToList();
        }

        public void PropagateAndGateInputChanged(int a, int b, int o, int time)
        {
            this.eventQueue.Add(new AndScheduledEvent(a, b, o, time + 2));
            this.eventQueue = this.eventQueue.OrderBy(e => e.Time).ToList();
        }

        public void And(int a, int b, int o, int time)
        {
            this.signals[o] = this.signals[a] && this.signals[b];
            foreach (var prop in this.signalChangedHandlers[o])
            {
                prop.Run(this, time);
            }
        }

        public void ProbeProp(int i, int time)
        {
            Console.WriteLine("Signal " + i + " at time " + time + " is " + signals[i]);
        }
    }
}