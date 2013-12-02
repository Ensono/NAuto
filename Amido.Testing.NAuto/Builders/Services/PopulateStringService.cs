using System.Reflection;

namespace Amido.Testing.NAuto.Builders.Services
{
    public class PopulateStringService : PopulateProperty<string>
    {
        public override string Populate(string propertyName, string currentValue, PropertyInfo propertyInfo = null)
        {
            if (currentValue != null)
            {
                return currentValue;
            }
            return GetStringValue(propertyName, propertyInfo);
        }

        private string GetStringValue(string propertyName, PropertyInfo propertyInfo)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(string)))
            {
                return (string)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(string));
            }

            var annotatedType = DataAnnotationConventionMapper.TryGetValue<string>(propertyInfo);
            if (annotatedType != null)
            {
                return (string)annotatedType;
            }

            return NAuto.GetRandomString(
                AutoBuilderConfiguration.StringMinLength,
                AutoBuilderConfiguration.StringMaxLength,
                AutoBuilderConfiguration.DefaultStringCharacterSetType,
                AutoBuilderConfiguration.DefaultStringSpaces,
                AutoBuilderConfiguration.DefaultStringCasing);
        }
    }
}