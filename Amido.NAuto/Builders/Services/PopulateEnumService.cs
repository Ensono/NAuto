using System;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateEnumService : IPopulateEnumService
    {
        protected AutoBuilderConfiguration AutoBuilderConfiguration { get; set; }

        public void SetAutoBuilderConfiguration(AutoBuilderConfiguration autoBuilderConfiguration)
        {
            AutoBuilderConfiguration = autoBuilderConfiguration;
        }

        public Enum Populate(string propertyName, Type enumType, object currentValue)
        {
            var values = Enum.GetValues(enumType);

            if (currentValue != null && values.GetValue(0) != currentValue)
            {
                return (Enum)currentValue;
            }

            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, enumType))
            {
                return (Enum)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, enumType, AutoBuilderConfiguration);
            }

            if (values.Length <= 1)
            {
                return null;
            }

            var randomValue = NAuto.GetRandomInteger(0, values.Length - 1);
            return (Enum)values.GetValue(randomValue);
        }
    }
}
