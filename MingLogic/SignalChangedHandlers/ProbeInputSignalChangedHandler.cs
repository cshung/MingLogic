namespace MingLogic
{
    public class ProbeInputSignalChangedHandler : SignalChangedHandler
    {
        private int i;

        public ProbeInputSignalChangedHandler(int i)
        {
            this.i = i;
        }

        public override void Run(Circuit circuit, int time)
        {
            circuit.ProbeProp(this.i, time);
        }
    }
}