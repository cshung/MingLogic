namespace MingLogic
{
    public class AndGateInputSignalChangedHandler : SignalChangedHandler
    {
        int a;
        int b;
        int o;

        public AndGateInputSignalChangedHandler(int a, int b, int o)
        {
            this.a = a;
            this.b = b;
            this.o = o;
        }

        public override void Run(Circuit circuit, int time)
        {
            circuit.PropagateAndGateInputChanged(a, b, o, time);
        }
    }
}