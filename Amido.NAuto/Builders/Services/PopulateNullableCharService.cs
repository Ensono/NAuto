using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateNullableCharService : PopulateProperty<char?>
    {
        public override char? Populate(string propertyName, char? currentValue, PropertyInfo propertyInfo = null)
        {
            if (currentValue != default(char?))
            {
                return currentValue;
            }

            return GetCharValue(propertyName);
        }

        private char GetCharValue(string propertyName)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(char?)))
            {
                return (char)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(char?), AutoBuilderConfiguration);
            }

            return char.Parse(NAuto.GetRandomString(1, AutoBuilderConfiguration.DefaultLanguage));
        }
    }
}
