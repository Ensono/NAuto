using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateNullableDecimalService : PopulateProperty<decimal?>
    {
        public override decimal? Populate(string propertyName, decimal? currentValue, PropertyInfo propertyInfo = null)
        {
            if (currentValue != default(decimal?))
            {
                return currentValue;
            }

            return GetDecimalValue(propertyName);
        }

        private decimal GetDecimalValue(string propertyName)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(decimal?)))
            {
                return (decimal)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(decimal?), AutoBuilderConfiguration);
            }

            return (decimal)NAuto.GetRandomDouble(AutoBuilderConfiguration.DoubleMinimum, AutoBuilderConfiguration.DoubleMaximum);
        }
    }
}