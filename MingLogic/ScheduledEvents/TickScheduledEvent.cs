using System;
using System.Collections.Generic;
using System.Linq;

namespace MingLogic
{
    class TickScheduledEvent : ScheduledEvent
    {
        private int signalIndex;

        public TickScheduledEvent(int signalIndex, int time)
        {
            this.signalIndex = signalIndex;
            this.Time = time;
        }

        public override void Process(Circuit circuit)
        {
            circuit.Tick(signalIndex, this.Time);
        }
    }
}