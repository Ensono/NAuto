using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateListService : IPopulateListService
    {
        protected AutoBuilderConfiguration AutoBuilderConfiguration { get; set; }

        public void SetAutoBuilderConfiguration(AutoBuilderConfiguration autoBuilderConfiguration)
        {
            AutoBuilderConfiguration = autoBuilderConfiguration;
        }

        public object Populate(
            string propertyName,
            Type propertyType,
            object currentValue,
            int depth,
            Func<int, string, Type, object, PropertyInfo, object> populate)
        {
            if (currentValue != null && ((IList)currentValue).Count > 0)
            {
                return currentValue;
            }

            var listType = propertyType.GetGenericArguments()[0];

            IList newList;

            if (currentValue != null && ((IList)currentValue).Count == 0)
            {
                newList = (IList)currentValue;
            }
            else
            {
                newList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(listType));
            }

            for (var i = 0; i < AutoBuilderConfiguration.DefaultCollectionItemCount; i++)
            {
                newList.Add(populate(depth + 1, propertyName, listType, null, null));
            }

            return newList;
        }
    }
}
