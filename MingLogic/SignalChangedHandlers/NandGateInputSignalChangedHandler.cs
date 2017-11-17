namespace MingLogic
{
    public class NandGateInputSignalChangedHandler : SignalChangedHandler
    {
        private int a;
        private int b;
        private int o;

        public NandGateInputSignalChangedHandler(int a, int b, int o)
        {
            this.a = a;
            this.b = b;
            this.o = o;
        }

        public override void Run(Circuit circuit, int time)
        {
            circuit.PropagateNandGateInputChanged(this.a, this.b, this.o, time);
        }
    }
}