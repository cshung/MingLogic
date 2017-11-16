using System;
using System.Collections.Generic;
using System.Linq;

namespace MingLogic
{
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
                eventQueue.Add(new TickScheduledEvent(0, clock));
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
            this.eventQueue.Add(new TickScheduledEvent(signalIndex, time + 10));
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
}