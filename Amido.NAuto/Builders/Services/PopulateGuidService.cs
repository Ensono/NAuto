using System;
using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateGuidService : PopulateProperty<Guid>
    {
        public override Guid Populate(string propertyName, Guid currentValue, PropertyInfo propertyInfo = null)
        {
            if (currentValue != Guid.Empty)
            {
                return currentValue;
            }

            return Guid.NewGuid();
        }
    }
}