namespace MingLogic
{
    public class InputScheduledEvent : ScheduledEvent
    {
        private int signalIndex;
        private int index;

        public InputScheduledEvent(int signalIndex, int index, int time)
        {
            this.signalIndex = signalIndex;
            this.index = index;
            this.Time = time;
        }

        public override void Process(Circuit circuit)
        {
            circuit.OnInputScheduledEvent(this.signalIndex, this.index, this.Time);
        }
    }
}