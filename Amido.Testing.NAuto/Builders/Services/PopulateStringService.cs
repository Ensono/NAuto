namespace Amido.Testing.NAuto.Builders.Services
{
    public class PopulateStringService : PopulateProperty<string>
    {
        public override string Populate(string propertyName, string currentValue)
        {
            if (currentValue != null)
            {
                return currentValue;
            }
            return GetStringValue(propertyName);
        }

        private string GetStringValue(string propertyName)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(string)))
            {
                return (string)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(string));
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