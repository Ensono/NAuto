using System;
using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateDateTimeService : PopulateProperty<DateTime>
    {
        public override DateTime Populate(string propertyName, DateTime currentValue, PropertyInfo propertyInfo = null)
        {
            return currentValue != default(DateTime) ? currentValue : GetDateTimeValue(propertyName);
        }

        private DateTime GetDateTimeValue(string propertyName)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(DateTime)))
            {
                return (DateTime)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(DateTime));
            }
            return AutoBuilderConfiguration.DefaultDateTime;
        }
    }
}