namespace MingLogic
{
    public abstract class ScheduledEvent
    {
        public float Time { get; set; }

        public abstract void Process(Circuit circuit);
    }
}