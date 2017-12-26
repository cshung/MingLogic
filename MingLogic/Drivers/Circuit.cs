﻿namespace MingLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Circuit
    {
        private List<List<SignalChangedHandler>> signalChangedHandlers = new List<List<SignalChangedHandler>>();
        private Dictionary<int, List<Tuple<int, bool>>> inputs = new Dictionary<int, List<Tuple<int, bool>>>();

        private Nullable<bool>[] signals;
        private List<ScheduledEvent> eventQueue;

        public int GetSignalNumber()
        {
            this.signalChangedHandlers.Add(new List<SignalChangedHandler>());
            return this.signalChangedHandlers.Count - 1;
        }

        public void RegisterInput(int v, List<Tuple<int, bool>> inputs)
        {
            this.inputs.Add(v, inputs);
        }

        public void RegisterNandGate(int a, int b, int o)
        {
            this.signalChangedHandlers[a].Add(new NandGateInputSignalChangedHandler(a, b, o));
            this.signalChangedHandlers[b].Add(new NandGateInputSignalChangedHandler(a, b, o));
        }

        public void RegisterProbe(int i, string name)
        {
            this.signalChangedHandlers[i].Add(new ProbeInputSignalChangedHandler(i, name));
        }

        public void Run()
        {
            this.signals = new bool?[this.signalChangedHandlers.Count];
            this.eventQueue = new List<ScheduledEvent>();
            foreach (var input in this.inputs)
            {
                this.eventQueue.Add(new InputScheduledEvent(input.Key, 0, 0));
            }

            while (this.eventQueue.Count > 0)
            {
                ScheduledEvent e = this.eventQueue[0];
                this.eventQueue.RemoveAt(0);
                e.Process(this);
            }
        }

        public void OnInputScheduledEvent(int signalIndex, int index, int time)
        {
            List<Tuple<int, bool>> inputs = this.inputs[signalIndex];
            this.SetSignalValue(signalIndex, inputs[index].Item2, time);
            int nextIndex = index + 1;
            if (nextIndex < inputs.Count)
            {
                this.eventQueue.Add(new InputScheduledEvent(signalIndex, nextIndex, inputs[nextIndex].Item1));
                this.eventQueue = this.eventQueue.OrderBy(e => e.Time).ToList();
            }
        }

        public void PropagateNandGateInputChanged(int a, int b, int o, int time)
        {
            this.eventQueue.Add(new NandScheduledEvent(a, b, o, time + 1));
            this.eventQueue = this.eventQueue.OrderBy(e => e.Time).ToList();
        }

        public void OnNandGatePropagationDelayReached(int a, int b, int o, int time)
        {
            bool? result = null;
            if (this.signals[a].HasValue && this.signals[b].HasValue)
            {
                result = !(this.signals[a].Value && this.signals[b].Value);
            }

            this.SetSignalValue(o, result, time);
        }

        public void OnProbeInputSignalChanged(int i, string name, int time)
        {
            Console.WriteLine("Signal " + name + " at time " + time + " is " + this.signals[i]);
        }

        private void SetSignalValue(int signalIndex, bool? signalValue, int time)
        {
            bool? originalSignalValue = this.signals[signalIndex];
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