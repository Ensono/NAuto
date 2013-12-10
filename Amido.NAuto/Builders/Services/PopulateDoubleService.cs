using System;
using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateDoubleService : PopulateProperty<double>
    {
        public override double Populate(string propertyName, double currentValue, PropertyInfo propertyInfo = null)
        {
            if (Math.Abs(currentValue - default(double)) > 0)
            {
                return currentValue;
            }

            return GetDoubleValue(propertyName);
        }

        private double GetDoubleValue(string propertyName)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(double)))
            {
                return (double)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(double), AutoBuilderConfiguration);
            }

            return NAuto.GetRandomDouble(AutoBuilderConfiguration.DoubleMinimum, AutoBuilderConfiguration.DoubleMaximum);
        }
    }
}