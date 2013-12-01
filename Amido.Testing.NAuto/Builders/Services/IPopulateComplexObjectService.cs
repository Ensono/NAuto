using System;
using System.Reflection;

namespace Amido.Testing.NAuto.Builders.Services
{
    public interface IPopulateComplexObjectService
    {
        object Populate(
            string propertyName, 
            Type propertyType, 
            object currentValue, 
            int depth,
            Func<ConstructorInfo[], int, Func<int, string, Type, object, object>, object[]> buildConstructorParametersFunction,
            Func<int, string, Type, object, object> populate,
            Action<object, int> populateProperties);

        void SetAutoBuilderConfiguration(AutoBuilderConfiguration autoBuilderConfiguration);
    }
}