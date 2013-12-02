using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Amido.Testing.NAuto.Builders.Services
{
    public static class DataAnnotationConventionMapper
    {
        public static object TryGetValue<TProperty>(PropertyInfo propertyInfo)
        {
            if (propertyInfo != null)
            {
                var dataTypeAttribute = propertyInfo.GetCustomAttributes(typeof (DataTypeAttribute)).FirstOrDefault();
                if (dataTypeAttribute != null)
                {
                    var dataType = ((DataTypeAttribute)dataTypeAttribute).DataType;
                    if (typeof(TProperty) == typeof(string))
                    {
                        if (dataType == DataType.EmailAddress)
                        {
                            return NAuto.GetRandomPropertyType(PropertyType.Email);
                        }

                        if (dataType == DataType.PostalCode)
                        {
                            return NAuto.GetRandomPropertyType(PropertyType.PostCode);
                        }

                        if (dataType == DataType.PhoneNumber)
                        {
                            return NAuto.GetRandomPropertyType(PropertyType.TelephoneNumber);
                        }

                        if (dataType == DataType.Url)
                        {
                            return NAuto.GetRandomPropertyType(PropertyType.Url);
                        }
                    }
                }
            }
            return null;
        }
    }
}