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
            this.signalChangedHandlers.Add(new List<SignalChangedHandler>());
            return this.signalChangedHandlers.Count - 1;
        }

        public void RegisterAndGate(int a, int b, int o)
        {
            this.signalChangedHandlers[a].Add(new AndGateInputSignalChangedHandler(a, b, o));
            this.signalChangedHandlers[b].Add(new AndGateInputSignalChangedHandler(a, b, o));
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
                this.eventQueue.Add(new TickScheduledEvent(0, clock));
            }

            for (int i = 0; i < 100; i++)
            {
                ScheduledEvent e = this.eventQueue[0];
                this.eventQueue.RemoveAt(0);
                e.Process(this);
            }
        }

        public void Tick(int signalIndex, int time)
        {
            this.SetSignalValue(signalIndex, !this.signals[signalIndex], time);
            this.eventQueue.Add(new TickScheduledEvent(signalIndex, time + 10));
            this.eventQueue = this.eventQueue.OrderBy(e => e.Time).ToList();
        }

        public void PropagateAndGateInputChanged(int a, int b, int o, int time)
        {
            this.eventQueue.Add(new AndScheduledEvent(a, b, o, time + 2));
            this.eventQueue = this.eventQueue.OrderBy(e => e.Time).ToList();
        }

        public void OnAndGatePropagationDelayReached(int a, int b, int o, int time)
        {
            this.SetSignalValue(o, this.signals[a] && this.signals[b], time);
        }

        public void OnProbeInputSignalChanged(int i, int time)
        {
            Console.WriteLine("Signal " + i + " at time " + time + " is " + this.signals[i]);
        }

        private void SetSignalValue(int signalIndex, bool signalValue, int time)
        {
            bool originalSignalValue = this.signals[signalIndex];
            this.signals[signalIndex] = signalValue;
            if (originalSignalValue != signalValue)
            {
                foreach (var signalChangedHandler in this.signalChangedHandlers[signalIndex])
                {
                    signalChangedHandler.Run(this, time);
                }
            }
        }
    }
}