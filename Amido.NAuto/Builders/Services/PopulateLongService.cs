using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateLongService : PopulateProperty<long>
    {
        public override long Populate(string propertyName, long currentValue, PropertyInfo propertyInfo = null)
        {
            if (currentValue != default(long))
            {
                return currentValue;
            }
            return GetLongValue(propertyName);
        }

        private long GetLongValue(string propertyName)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(long)))
            {
                return (long)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(long), AutoBuilderConfiguration);
            }

            return NAuto.GetRandomInteger(AutoBuilderConfiguration.IntMinimum, AutoBuilderConfiguration.IntMaximum);
        }
    }
}