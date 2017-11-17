namespace MingLogic
{
    public class NandFactory : IComponentFactory
    {
        public IComponent Build()
        {
            return new NandGate();
        }
    }
}
