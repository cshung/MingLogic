namespace MingLogic
{
    public class AndScheduledEvent : ScheduledEvent
    {
        private int a;
        private int b;
        private int o;

        public AndScheduledEvent(int a, int b, int o, int time)
        {
            this.a = a;
            this.b = b;
            this.o = o;
            this.Time = time;
        }

        public override void Process(Circuit circuit)
        {
            circuit.And(a, b, o, this.Time);
        }
    }
}