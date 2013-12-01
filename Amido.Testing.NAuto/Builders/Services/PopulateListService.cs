using System;
using System.Collections;

namespace Amido.Testing.NAuto.Builders.Services
{
    public interface IPopulateListService
    {
        void SetAutoBuilderConfiguration(AutoBuilderConfiguration autoBuilderConfiguration);

        object Populate(
            string propertyName, 
            Type propertyType, 
            object currentValue, 
            int depth,
            Func<int, string, Type, object, object> populate);
    }

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
            Func<int, string, Type, object, object> populate)
        {
            if (currentValue != null)
            {
                return currentValue;
            }

            IList newList;

            if (currentValue != null && ((IList)currentValue).Count == 0)
            {
                newList = (IList)currentValue;
            }
            else
            {
                newList = (IList)Activator.CreateInstance(propertyType);
            }

            for (var i = 0; i < AutoBuilderConfiguration.DefaultListItemCount; i++)
            {
                newList.Add(populate(depth + 1, propertyName, propertyType.GenericTypeArguments[0], null));
            }
            return newList;
        }
    }
}
