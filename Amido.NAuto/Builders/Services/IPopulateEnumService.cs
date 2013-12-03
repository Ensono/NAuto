using System;

namespace Amido.NAuto.Builders.Services
{
    public interface IPopulateEnumService
    {
        void SetAutoBuilderConfiguration(AutoBuilderConfiguration autoBuilderConfiguration);
        Enum Populate(string propertyName, Type enumType, object currentValue);
    }
}