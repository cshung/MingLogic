namespace MingLogic
{
    public class NandScheduledEvent : ScheduledEvent
    {
        private int a;
        private int b;
        private int o;

        public NandScheduledEvent(int a, int b, int o, float time)
        {
            this.a = a;
            this.b = b;
            this.o = o;
            this.Time = time;
        }

        public override void Process(Circuit circuit)
        {
            circuit.OnNandGatePropagationDelayReached(this.a, this.b, this.o, this.Time);
        }
    }
}