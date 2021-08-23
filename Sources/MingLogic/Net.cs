namespace MingLogic
{
    public class Net
    {
        public string Name { get; set; }

        public int Index { get; set; }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() ^ this.Index;
        }

        public override bool Equals(object obj)
        {
            Net that = obj as Net;
            if (that == null)
            {
                return false;
            }

            return this.Name.Equals(that.Name) && this.Index == that.Index;
        }
    }
}
