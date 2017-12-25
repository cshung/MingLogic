namespace MingLogic
{
    public class ProbeInputSignalChangedHandler : SignalChangedHandler
    {
        private int i;
        private string name;

        public ProbeInputSignalChangedHandler(int i, string name)
        {
            this.i = i;
            this.name = name;
        }

        public override void Run(Circuit circuit, int time)
        {
            circuit.OnProbeInputSignalChanged(this.i, this.name, time);
        }
    }
}