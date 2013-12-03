using System;
using System.Reflection;

namespace Amido.Testing.NAuto.Builders.Services
{
    public interface IDataAnnotationConventionMapper
    {
        object TryGetValue(Type type, PropertyInfo propertyInfo, AutoBuilderConfiguration autoBuilderConfiguration);
    }
}