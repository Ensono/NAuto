namespace Amido.NAuto.Compare
{
    public class CompareItem
    {
        public string PropertyPath { get; set; }

        public string PropertyName { get; set; }

        public object ModelValueA { get; set; }

        public object ModelValueB { get; set; }

        public bool PropertyAvailableOnModelBForComparison { get; set; }

        public bool IsEqual
        {
            get
            {
                if (ModelValueA == null)
                {
                    return true;
                }

                return this.ModelValueA.Equals(this.ModelValueB);
            }
        }
    }
}