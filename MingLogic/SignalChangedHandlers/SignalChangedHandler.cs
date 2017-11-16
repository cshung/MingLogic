namespace MingLogic
{
    public abstract class SignalChangedHandler
    {
        public abstract void Run(Circuit circuit, int time);
    }
}
