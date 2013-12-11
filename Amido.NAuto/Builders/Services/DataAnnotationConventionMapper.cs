using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class DataAnnotationConventionMapper : IDataAnnotationConventionMapper
    {
        public object TryGetValue(Type type, PropertyInfo propertyInfo, AutoBuilderConfiguration autoBuilderConfiguration)
        {
            if (propertyInfo != null)
            {
                var dataTypeAttribute = propertyInfo.GetCustomAttributes(typeof(DataTypeAttribute)).FirstOrDefault();
                if (dataTypeAttribute != null)
                {
                    var dataType = ((DataTypeAttribute)dataTypeAttribute).DataType;
                    if (type == typeof(string))
                    {
                        if (dataType == DataType.EmailAddress)
                        {
                            return NAuto.GetRandomPropertyType(PropertyType.Email);
                        }

                        if (dataType == DataType.PostalCode)
                        {
                            return NAuto.GetRandomPropertyType(PropertyType.PostalCode);
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

                if (type == typeof(string))
                {
                    return GenerateRandomStringFromDataAnnotations(propertyInfo, autoBuilderConfiguration);
                }

                if (type == typeof(int))
                {
                    return GenerateRandomIntFromDataAnnotations(propertyInfo, autoBuilderConfiguration);
                }

                if (type == typeof(double))
                {
                    return GenerateRandomDoubleFromDataAnnotations(propertyInfo, autoBuilderConfiguration);
                }
            }

            return null;
        }

        private static double GenerateRandomDoubleFromDataAnnotations(PropertyInfo propertyInfo, AutoBuilderConfiguration autoBuilderConfiguration)
        {
            var min = autoBuilderConfiguration.DoubleMinimum;
            var max = autoBuilderConfiguration.DoubleMaximum;

            var rangeAttribute = propertyInfo.GetCustomAttributes(typeof(RangeAttribute)).FirstOrDefault();
            if (rangeAttribute != null)
            {
                min = double.Parse(((RangeAttribute)rangeAttribute).Minimum.ToString());
                max = double.Parse(((RangeAttribute)rangeAttribute).Maximum.ToString());
            }

            return NAuto.GetRandomDouble(min, max);
        }

        private static int GenerateRandomIntFromDataAnnotations(PropertyInfo propertyInfo, AutoBuilderConfiguration autoBuilderConfiguration)
        {
            var min = autoBuilderConfiguration.IntMinimum;
            var max = autoBuilderConfiguration.IntMaximum;

            var rangeAttribute = propertyInfo.GetCustomAttributes(typeof(RangeAttribute)).FirstOrDefault();
            if (rangeAttribute != null)
            {
                min = (int)((RangeAttribute)rangeAttribute).Minimum;
                max = (int)((RangeAttribute)rangeAttribute).Maximum;
            }

            return NAuto.GetRandomInteger(min, max);
        }

        private static string GenerateRandomStringFromDataAnnotations(PropertyInfo propertyInfo, AutoBuilderConfiguration autoBuilderConfiguration)
        {
            var minLength = autoBuilderConfiguration.StringMinLength;
            var maxLength = autoBuilderConfiguration.StringMaxLength;

            var minLengthAttribute = propertyInfo.GetCustomAttributes(typeof(MinLengthAttribute)).FirstOrDefault();
            if (minLengthAttribute != null)
            {
                minLength = ((MinLengthAttribute)minLengthAttribute).Length;
            }

            var maxLengthAttribute = propertyInfo.GetCustomAttributes(typeof(MaxLengthAttribute)).FirstOrDefault();
            if (maxLengthAttribute != null)
            {
                maxLength = ((MaxLengthAttribute)maxLengthAttribute).Length;
            }

            if (minLengthAttribute != null || maxLengthAttribute != null)
            {
                {
                    return NAuto.GetRandomString(
                        minLength,
                        maxLength,
                        autoBuilderConfiguration.DefaultStringCharacterSetType,
                        autoBuilderConfiguration.DefaultStringSpaces,
                        autoBuilderConfiguration.DefaultStringCasing,
                        autoBuilderConfiguration.DefaultLanguage);
                }
            }

            var stringLengthAttribute = propertyInfo.GetCustomAttributes(typeof(StringLengthAttribute)).FirstOrDefault();

            if (stringLengthAttribute != null)
            {
                var minStringLength = ((StringLengthAttribute)stringLengthAttribute).MinimumLength;
                var maxStringLength = ((StringLengthAttribute)stringLengthAttribute).MaximumLength;

                if (maxStringLength == 0)
                {
                    maxStringLength = minStringLength + 50;
                }

                if (maxStringLength < minStringLength)
                {
                    throw new ArgumentException("Property " + propertyInfo.Name + ": the minimum string length cannot be greater than the maximum string length...");
                }

                return NAuto.GetRandomString(
                        minStringLength,
                        maxStringLength,
                        autoBuilderConfiguration.DefaultStringCharacterSetType,
                        autoBuilderConfiguration.DefaultStringSpaces,
                        autoBuilderConfiguration.DefaultStringCasing,
                        autoBuilderConfiguration.DefaultLanguage);
            }

            return null;
        }
    }
}