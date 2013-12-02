using System;

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
}