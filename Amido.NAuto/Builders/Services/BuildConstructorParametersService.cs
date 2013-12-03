using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class BuildConstructorParametersService : IBuildConstructorParametersService
    {
        public object[] Build(ConstructorInfo[] constructors, int depth, Func<int, string, Type, object, PropertyInfo, object> populateFunction)
        {
            var constructorParameters = new List<object>();

            foreach (var parameter in constructors.First().GetParameters())
            {
                constructorParameters.Add(populateFunction(depth + 1, parameter.Name, parameter.ParameterType, null, null));
            }

            return constructorParameters.ToArray();
        } 
    }
}
