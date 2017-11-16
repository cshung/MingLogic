using System;
using System.Collections.Generic;
using System.Linq;

namespace MingLogic
{
    abstract class ScheduledEvent
    {
        public int Time { get; set; }
        public abstract void Process(Circuit circuit);
    }
}