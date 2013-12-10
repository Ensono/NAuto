using System;
using System.Collections;
using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateDictionaryService : IPopulateDictionaryService
    {
        protected AutoBuilderConfiguration AutoBuilderConfiguration { get; set; }

        public void SetAutoBuilderConfiguration(AutoBuilderConfiguration autoBuilderConfiguration)
        {
            AutoBuilderConfiguration = autoBuilderConfiguration;
        }

        public object Populate(string propertyName, Type propertyType, object currentValue, int depth, Func<int, string, Type, object, PropertyInfo, object> populateKey, Func<int, string, Type, object, PropertyInfo, object> populateValue)
        {
            if (currentValue != null && ((IDictionary)currentValue).Count > 0)
            {
                return currentValue;
            }

            IDictionary newDictionary;

            if (currentValue != null && ((IDictionary)currentValue).Count == 0)
            {
                newDictionary = (IDictionary)currentValue;
            }
            else
            {
                newDictionary = (IDictionary)Activator.CreateInstance(propertyType);
            }

            for (var i = 0; i < AutoBuilderConfiguration.DefaultCollectionItemCount; i++)
            {
                newDictionary.Add(populateKey(depth + 1, propertyName, propertyType.GenericTypeArguments[0], null, null), populateValue(depth + 1, propertyName, propertyType.GenericTypeArguments[1], null, null));
            }

            return newDictionary;
        }
    }
}
