using System;

namespace Amido.Testing.NAuto.Builders.Services
{
    public class PopulateDoubleService : PopulateProperty<double>
    {
        public override double Populate(string propertyName, double currentValue)
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
                return (double)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(double));
            }
            return NAuto.GetRandomDouble(AutoBuilderConfiguration.DoubleMinimum, AutoBuilderConfiguration.DoubleMaximum);
        }
    }
}