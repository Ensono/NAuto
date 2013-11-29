using System;

namespace Amido.Testing.NAuto.Builders.Services
{
    public interface IPopulateEnumService
    {
        Enum Populate(string propertyName, Type enumType, object currentValue);
    }
}