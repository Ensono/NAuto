using System.Collections.Generic;
using System.Reflection;

namespace Amido.Testing.NAuto.Builders.Services
{
    public interface IPropertyPopulationService
    {
        void PopulateProperties(object objectToPopulate, int depth);
        List<object> BuildConstructorParameters(ConstructorInfo[] constructors, int depth);
        void AddConfiguration(AutoBuilderConfiguration autoBuilderConfiguration);
    }
}