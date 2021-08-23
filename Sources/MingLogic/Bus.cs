namespace MingLogic
{
    public class Bus
    {
        public string Name { get; set; }

        public int Width { get; set; }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Bus that = obj as Bus;
            if (that == null)
            {
                return false;
            }

            return this.Name.Equals(that.Name);
        }
    }
}
