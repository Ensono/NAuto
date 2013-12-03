using System;
using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public interface IDataAnnotationConventionMapper
    {
        object TryGetValue(Type type, PropertyInfo propertyInfo, AutoBuilderConfiguration autoBuilderConfiguration);
    }
}