namespace MingLogic
{
    public class ClockFactory : IComponentFactory
    {
        public IComponent Build()
        {
            return new Clock();
        }
    }
}
