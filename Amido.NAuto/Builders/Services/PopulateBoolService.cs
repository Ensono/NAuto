using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateBoolService : PopulateProperty<bool>
    {
        public override bool Populate(string propertyName, bool currentValue, PropertyInfo propertyInfo = null)
        {
            return currentValue || GetBooleanValue(propertyName);
        }

        private bool GetBooleanValue(string propertyName)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(bool)))
            {
                return (bool)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(bool));
            }
            return AutoBuilderConfiguration.DefaultBoolean;
        }
    }
}