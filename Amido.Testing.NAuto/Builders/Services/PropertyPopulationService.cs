using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Amido.Testing.NAuto.Builders.Services
{
    public class PropertyPopulationService : IPropertyPopulationService
    {
        private readonly PopulateProperty<string> populateStringService;
        private AutoBuilderConfiguration configuration;

        public PropertyPopulationService(PopulateProperty<string> populateStringService)
        {
            this.populateStringService = populateStringService;
        }

        public void AddConfiguration(AutoBuilderConfiguration autoBuilderConfiguration)
        {
            configuration = autoBuilderConfiguration;
            populateStringService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        public void PopulateProperties(object objectToPopulate, int depth)
        {
            if (depth > configuration.MaxDepth)
            {
                return;
            }

            var properties = objectToPopulate.GetType().GetProperties();

            foreach (var propertyInfo in properties)
            {
                propertyInfo.SetValue(objectToPopulate, Populate(depth, propertyInfo.Name, propertyInfo.PropertyType, propertyInfo.GetValue(objectToPopulate)));
            }
        }

        public List<object> BuildConstructorParameters(ConstructorInfo[] constructors, int depth)
        {
            var constructorParameters = new List<object>();

            foreach (var parameter in constructors.First().GetParameters())
            {
                constructorParameters.Add(Populate(depth + 1, parameter.Name, parameter.ParameterType, null));
            }

            return constructorParameters;
        }

        private object Populate(int depth, string propertyName, Type propertyType, object value)
        {
            if (propertyType.GetInterfaces().Any(x => x == typeof(IList)))
            {
                if (propertyType.FullName.Contains("System.Collections.Generic.List`1"))
                {
                    return PopulateList(depth, propertyName, propertyType, value);
                }
                if (propertyType.BaseType == typeof(Array) && value == null)
                {
                    return PopulateArray(depth, propertyName, propertyType);
                }
            }

            if (propertyType == typeof (string))
            {
                return populateStringService.Populate(propertyName, (string)value);
            }

            if (propertyType == typeof(int) && value != null && (int)value != 0)
            {
                return value;
            }

            if (propertyType == typeof(int?) && value != null)
            {
                return value;
            }

            if (propertyType == typeof(double) && value != null && Math.Abs((double)value) > 0)
            {
                return value;
            }

            if (propertyType == typeof(double?) && value != null)
            {
                return value;
            }

            if (propertyType == typeof(bool) && value != null && (bool)value != default(bool))
            {
                return value;
            }

            if (propertyType == typeof(bool?) && value != null)
            {
                return value;
            }

            if (propertyType == typeof(DateTime) && value != null && (DateTime)value != default(DateTime))
            {
                return value;
            }

            if (propertyType == typeof(DateTime?) && value != null)
            {
                return value;
            }

            if (propertyType == typeof(Uri) && value != null)
            {
                return value;
            }

            if (propertyType.BaseType == typeof(Enum) && value != null)
            {
                var values = Enum.GetValues(propertyType);
                if (values.GetValue(0) != value)
                {
                    return value;    
                }
            }

            //if (propertyType == typeof(string))
            //{
            //   return GetStringValue(propertyName);
            //}
            if (propertyType == typeof(int)
                || propertyType == typeof(int?))
            {
                return GetIntValue(propertyName);
            }

            if (propertyType == typeof(double) ||
                propertyType == typeof(double?))
            {
                return GetDoubleValue(propertyName);
            }

            if (propertyType == typeof(DateTime) ||
                propertyType == typeof(DateTime?))
            {
                return GetDateTimeValue(propertyName);
            }

            if (propertyType == typeof(bool) ||
                propertyType == typeof(bool?))
            {
                return GetBooleanValue(propertyName);
            }

            if (propertyType == typeof(Uri))
            {
                return GetUriValue(propertyName);
            }

            if (propertyType.BaseType == typeof (Enum))
            {
                var values = Enum.GetValues(propertyType);

                if (values.Length <= 1)
                {
                    return null;
                }
                var randomValue = NAuto.GetRandomInteger(0, values.Length - 1);
                return values.GetValue(randomValue);
            }

            if (IsPotentialComplexType(propertyType))
            {
                try
                {
                    var constructorParameters = BuildConstructorParameters(propertyType.GetConstructors(), depth + 1);

                    object complexType;
                    if (constructorParameters.Count > 0)
                    {
                        complexType = Activator.CreateInstance(propertyType, constructorParameters.ToArray());
                    }
                    else
                    {
                        complexType = Activator.CreateInstance(propertyType);
                    }
                    PopulateProperties(complexType, depth + 1);
                    return complexType;
                }
                catch (Exception)
                {
                    // swallow error
                }
            }
            Console.WriteLine("Sorry, unable to fully build this model. Unsupported Type: " + propertyType);
            return null;
        }

        private static bool IsPotentialComplexType(Type propertyType)
        {
            return propertyType.BaseType != typeof(ValueType) && !propertyType.IsPrimitive && propertyType != typeof(string) && propertyType != typeof(Uri);
        }

        private object PopulateArray(int depth, string propertyName, Type propertyType)
        {
            var arrayElementType = propertyType.GetElementType();
            var newArray = Array.CreateInstance(arrayElementType, configuration.DefaultListItemCount);
            for (var i = 0; i < configuration.DefaultListItemCount; i++)
            {
                newArray.SetValue(Populate(depth + 1, propertyName, arrayElementType, null), i);
            }
            return newArray;
        }

        private object PopulateList(int depth, string propertyName, Type propertyType, object value)
        {
            IList newList;

            if (value != null && ((IList) value).Count == 0)
            {
                newList = (IList) value;
            }
            else
            {
                newList = (IList) Activator.CreateInstance(propertyType);
            }

            for (var i = 0; i < configuration.DefaultListItemCount; i++)
            {
                newList.Add(Populate(depth + 1, propertyName, propertyType.GenericTypeArguments[0], null));
            }
            return newList;
        }

        private int GetIntValue(string propertyName)
        {
            if (configuration.Conventions.MatchesConvention(propertyName, typeof(int)))
            {
                return (int)configuration.Conventions.GetConventionResult(propertyName, typeof(int));
            }
            if (configuration.Conventions.MatchesConvention(propertyName, typeof(int?)))
            {
                return (int)configuration.Conventions.GetConventionResult(propertyName, typeof(int?));
            }
            return NAuto.GetRandomInteger(configuration.IntMinimum, configuration.IntMaximum);
        }

        private double GetDoubleValue(string propertyName)
        {
            if (configuration.Conventions.MatchesConvention(propertyName, typeof(double)))
            {
                return (double)configuration.Conventions.GetConventionResult(propertyName, typeof(double));
            }
            if (configuration.Conventions.MatchesConvention(propertyName, typeof(double?)))
            {
                return (double)configuration.Conventions.GetConventionResult(propertyName, typeof(double?));
            }
            return NAuto.GetRandomDouble(configuration.DoubleMinimum, configuration.DoubleMaximum);
        }

        private DateTime GetDateTimeValue(string propertyName)
        {
            if (configuration.Conventions.MatchesConvention(propertyName, typeof(DateTime)))
            {
                return (DateTime)configuration.Conventions.GetConventionResult(propertyName, typeof(DateTime));
            }
            if (configuration.Conventions.MatchesConvention(propertyName, typeof(DateTime?)))
            {
                return (DateTime)configuration.Conventions.GetConventionResult(propertyName, typeof(DateTime?));
            }
            return configuration.DefaultDateTime;
        }

        private bool GetBooleanValue(string propertyName)
        {
            if (configuration.Conventions.MatchesConvention(propertyName, typeof(bool)))
            {
                return (bool)configuration.Conventions.GetConventionResult(propertyName, typeof(bool));
            }
            if (configuration.Conventions.MatchesConvention(propertyName, typeof(bool?)))
            {
                return (bool)configuration.Conventions.GetConventionResult(propertyName, typeof(bool?));
            }
            return configuration.DefaultBoolean;
        }

        private Uri GetUriValue(string propertyName)
        {
            if (configuration.Conventions.MatchesConvention(propertyName, typeof(Uri)))
            {
                return (Uri)configuration.Conventions.GetConventionResult(propertyName, typeof(Uri));
            }
            return new Uri(NAuto.GetRandomPropertyType(PropertyType.Url));
        }
    }
}
