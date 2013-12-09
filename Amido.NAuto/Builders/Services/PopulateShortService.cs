using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateShortService : PopulateProperty<short>
    {
        public override short Populate(string propertyName, short currentValue, PropertyInfo propertyInfo = null)
        {
            if (currentValue != default(short))
            {
                return currentValue;
            }

            return GetValue(propertyName);
        }

        private short GetValue(string propertyName)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(short)))
            {
                return (short)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(short), AutoBuilderConfiguration);
            }

            return (short)NAuto.GetRandomInteger(AutoBuilderConfiguration.ShortMinimum, AutoBuilderConfiguration.ShortMaximum);
        }
    }
}