using System;

namespace Amido.Testing.NAuto.Builders.Services
{
    public class PopulateEnumService : IPopulateEnumService
    {
        public Enum Populate(string propertyName, Type enumType, object currentValue)
        {
            var values = Enum.GetValues(enumType);

            if (currentValue != null && values.GetValue(0) != currentValue)
            {
                return (Enum)currentValue;
            }

            //TODO: Add convention check

            if (values.Length <= 1)
            {
                return null;
            }
            var randomValue = NAuto.GetRandomInteger(0, values.Length - 1);
            return (Enum)values.GetValue(randomValue);
            
        }
    }
}
