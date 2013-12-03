using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public interface IPropertyPopulationService
    {
        object PopulateProperties(object objectToPopulate, int depth);
        object[] BuildConstructorParameters(ConstructorInfo[] constructors, int depth);
        void AddConfiguration(AutoBuilderConfiguration autoBuilderConfiguration);
    }
}