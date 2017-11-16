namespace MingLogic
{
    abstract public class ScheduledEvent
    {
        public int Time { get; set; }
        public abstract void Process(Circuit circuit);
    }
}