using System;
using System.Reflection;

namespace Amido.Testing.NAuto.Builders.Services
{
    public class PopulateComplexObjectService : IPopulateComplexObjectService
    {
        protected AutoBuilderConfiguration AutoBuilderConfiguration { get; set; }

        public void SetAutoBuilderConfiguration(AutoBuilderConfiguration autoBuilderConfiguration)
        {
            AutoBuilderConfiguration = autoBuilderConfiguration;
        }

        public object Populate(
            string propertyName, 
            Type propertyType, 
            object currentValue, 
            int depth,
            Func<ConstructorInfo[], int, Func<int, string, Type, object, object>, object[]> buildConstructorParametersFunction,
            Func<int, string, Type, object, object> populate,
            Func<object, int, object> populateProperties)
        {
            if (currentValue != null)
            {
                return currentValue;
            }

            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, propertyType))
            {
                return AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, propertyType);
            }
             try
                {
                    var constructorParameters = buildConstructorParametersFunction(propertyType.GetConstructors(), depth + 1, populate);

                    object complexType;
                    complexType = constructorParameters != null && constructorParameters.Length > 0 
                        ? Activator.CreateInstance(propertyType, constructorParameters) 
                        : Activator.CreateInstance(propertyType);
                    populateProperties(complexType, depth + 1);
                    return complexType;
                }
                catch (Exception)
                {
                    // swallow error
                }

             Console.WriteLine("Sorry, unable to fully build this model. Unsupported Type: " + propertyType);
             return null;
        }
    }
}
