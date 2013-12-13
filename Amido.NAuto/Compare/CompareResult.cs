using System.Collections.Generic;
using System.Linq;

namespace Amido.NAuto.Compare
{
    public class CompareResult : List<CompareItem>
    {
        public CompareResult(IEnumerable<CompareItem> compareItems)
        {
            AddRange(compareItems);
        }

        public bool AreEqual
        {
            get
            {
                return this.All(x => x.IsEqual);
            }
        }
    }

}