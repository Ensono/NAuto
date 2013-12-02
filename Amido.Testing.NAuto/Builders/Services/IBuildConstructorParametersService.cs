using System;
using System.Reflection;

namespace Amido.Testing.NAuto.Builders.Services
{
    public interface IBuildConstructorParametersService
    {
        object[] Build(ConstructorInfo[] constructors, int depth, Func<int, string, Type, object, PropertyInfo, object> populateFunction);
    }
}