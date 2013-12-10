using System;
using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateUriService : PopulateProperty<Uri>
    {
        public override Uri Populate(string propertyName, Uri currentValue, PropertyInfo propertyInfo = null)
        {
            if (currentValue == null)
            {
                return GetUriValue(propertyName);    
            }

            return currentValue;
        }

        private Uri GetUriValue(string propertyName)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(Uri)))
            {
                return (Uri)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(Uri), AutoBuilderConfiguration);
            }

            return new Uri(NAuto.GetRandomPropertyType(PropertyType.Url, AutoBuilderConfiguration.DefaultLanguage));
        }
    }
}