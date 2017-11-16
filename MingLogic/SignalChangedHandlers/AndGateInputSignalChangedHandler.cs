namespace MingLogic
{
    public class AndGateInputSignalChangedHandler : SignalChangedHandler
    {
        private int a;
        private int b;
        private int o;

        public AndGateInputSignalChangedHandler(int a, int b, int o)
        {
            this.a = a;
            this.b = b;
            this.o = o;
        }

        public override void Run(Circuit circuit, int time)
        {
            circuit.PropagateAndGateInputChanged(this.a, this.b, this.o, time);
        }
    }
}