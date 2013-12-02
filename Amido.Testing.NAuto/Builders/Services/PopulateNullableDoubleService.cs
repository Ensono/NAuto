using System;
using System.Reflection;

namespace Amido.Testing.NAuto.Builders.Services
{
    public class PopulateNullableDoubleService : PopulateProperty<double?>
    {
        public override double? Populate(string propertyName, double? currentValue, PropertyInfo propertyInfo = null)
        {
            if (currentValue != default(double?))
            {
                return currentValue;
            }
            return GetDoubleValue(propertyName);
        }

        private double? GetDoubleValue(string propertyName)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(double?)))
            {
                return (double?)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(double?));
            }
            return NAuto.GetRandomDouble(AutoBuilderConfiguration.DoubleMinimum, AutoBuilderConfiguration.DoubleMaximum);
        }
    }
}