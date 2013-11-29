using System;

namespace Amido.Testing.NAuto.Builders.Services
{
    public class PopulateNullableDateTimeService : PopulateProperty<DateTime?>
    {
        public override DateTime? Populate(string propertyName, DateTime? currentValue)
        {
            return currentValue != default(DateTime?) ? currentValue : GetDateTimeValue(propertyName);
        }

        private DateTime? GetDateTimeValue(string propertyName)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(DateTime?)))
            {
                return (DateTime?)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(DateTime?));
            }
            return AutoBuilderConfiguration.DefaultDateTime;
        }
    }
}