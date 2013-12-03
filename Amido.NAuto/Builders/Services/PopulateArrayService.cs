using System;
using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateArrayService : IPopulateArrayService
    {
        protected AutoBuilderConfiguration AutoBuilderConfiguration { get; set; }
        public void SetAutoBuilderConfiguration(AutoBuilderConfiguration autoBuilderConfiguration)
        {
            AutoBuilderConfiguration = autoBuilderConfiguration;
        }

        public object Populate(string propertyName, Type propertyType, object currentValue, int depth, Func<int, string, Type, object, PropertyInfo, object> populate)
        {
            var arrayElementType = propertyType.GetElementType();
            var newArray = Array.CreateInstance(arrayElementType, AutoBuilderConfiguration.DefaultCollectionItemCount);
            for (var i = 0; i < AutoBuilderConfiguration.DefaultCollectionItemCount; i++)
            {
                newArray.SetValue(populate(depth + 1, propertyName, arrayElementType, null, null), i);
            }
            return newArray;
        }
    }
}