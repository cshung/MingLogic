namespace MingLogic
{
    public class ProbeFactory : IComponentFactory
    {
        public IComponent Build()
        {
            return new Probe();
        }
    }
}
