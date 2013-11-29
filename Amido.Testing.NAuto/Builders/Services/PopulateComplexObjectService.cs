using System;

namespace Amido.Testing.NAuto.Builders.Services
{
    public class PopulateComplexObjectService
    {
        public object Populate(string propertyName, Type propertyType, object currentValue)
        {
            // try
            //    {
            //        var constructorParameters = BuildConstructorParameters(propertyType.GetConstructors(), depth + 1);

            //        object complexType;
            //        if (constructorParameters.Count > 0)
            //        {
            //            complexType = Activator.CreateInstance(propertyType, constructorParameters.ToArray());
            //        }
            //        else
            //        {
            //            complexType = Activator.CreateInstance(propertyType);
            //        }
            //        PopulateProperties(complexType, depth + 1);
            //        return complexType;
            //    }
            //    catch (Exception)
            //    {
            //        // swallow error
            //    }
            //}
            //Console.WriteLine("Sorry, unable to fully build this model. Unsupported Type: " + propertyType);
            return null;
        }
    }
}
