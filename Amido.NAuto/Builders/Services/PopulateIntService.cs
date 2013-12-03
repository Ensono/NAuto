using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateIntService : PopulateProperty<int>
    {
        public override int Populate(string propertyName, int currentValue, PropertyInfo propertyInfo = null)
        {
            if (currentValue != default(int))
            {
                return currentValue;
            }
            return GetIntValue(propertyName);
        }

        private int GetIntValue(string propertyName)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(int)))
            {
                return (int)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(int));
            }

            return NAuto.GetRandomInteger(AutoBuilderConfiguration.IntMinimum, AutoBuilderConfiguration.IntMaximum);
        }
    }
}