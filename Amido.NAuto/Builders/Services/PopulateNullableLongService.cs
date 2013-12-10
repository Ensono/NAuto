using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateNullableLongService : PopulateProperty<long?>
    {
        public override long? Populate(string propertyName, long? currentValue, PropertyInfo propertyInfo = null)
        {
            if (currentValue != default(long?))
            {
                return currentValue;
            }

            return GetValue(propertyName);
        }

        private long? GetValue(string propertyName)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(long?)))
            {
                return (long)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(long?), AutoBuilderConfiguration);
            }

            return NAuto.GetRandomInteger(AutoBuilderConfiguration.IntMinimum, AutoBuilderConfiguration.IntMaximum);
        }
    }
}