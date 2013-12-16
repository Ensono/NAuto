using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PropertyPopulationService : IPropertyPopulationService
    {
        private readonly PopulateProperty<string> populateStringService;
        private readonly PopulateProperty<int> populateIntService;
        private readonly PopulateProperty<int?> populateNullableIntProperty;
        private readonly PopulateProperty<double> populateDoubleService;
        private readonly PopulateProperty<double?> populateNullableDoubleService;
        private readonly PopulateProperty<bool> populateBoolService;
        private readonly PopulateProperty<bool?> populateNullableBoolService;
        private readonly PopulateProperty<byte> populateByteService;
        private readonly PopulateProperty<byte?> populateNullableByteService;
        private readonly PopulateProperty<DateTime> populateDateTimeService;
        private readonly PopulateProperty<DateTime?> populateNullableDateTimeService;
        private readonly PopulateProperty<Uri> populateUriService;
        private readonly PopulateProperty<Guid> populateGuidService;
        private readonly PopulateProperty<long> populateLongService;
        private readonly PopulateProperty<long?> populateNullableLongService;
        private readonly PopulateProperty<short> populateShortService;
        private readonly PopulateProperty<char> populateCharService;
        private readonly PopulateProperty<char?> populateNullableCharService;
        private readonly PopulateProperty<decimal> populateDecimalService;
        private readonly PopulateProperty<decimal?> populateNullableDecimalService;
        private readonly IPopulateEnumService populateEnumService;
        private readonly IBuildConstructorParametersService buildConstructorParametersService;
        private readonly IPopulateComplexObjectService populateComplexObjectService;
        private readonly IPopulateListService populateListService;
        private readonly IPopulateDictionaryService populateDictionaryService;
        private readonly IPopulateArrayService populateArrayService;
        private AutoBuilderConfiguration configuration;

        public PropertyPopulationService(
            PopulateProperty<string> populateStringService, 
            PopulateProperty<int> populateIntService,
            PopulateProperty<int?> populateNullableIntProperty,
            PopulateProperty<double> populateDoubleService,
            PopulateProperty<double?> populateNullableDoubleService,
            PopulateProperty<bool> populateBoolService,
            PopulateProperty<bool?> populateNullableBoolService,
            PopulateProperty<byte> populateByteService,
            PopulateProperty<byte?> populateNullableByteService,
            PopulateProperty<DateTime> populateDateTimeService,
            PopulateProperty<DateTime?> populateNullableDateTimeService,
            PopulateProperty<Uri> populateUriService,
            PopulateProperty<Guid> populateGuidService,
            PopulateProperty<long> populateLongService,
            PopulateProperty<long?> populateNullableLongService,
            PopulateProperty<short> populateShortService,
            PopulateProperty<char> populateCharService,
            PopulateProperty<char?> populateNullableCharService,
            PopulateProperty<decimal> populateDecimalService,
            PopulateProperty<decimal?> populateNullableDecimalService,
            IPopulateEnumService populateEnumService,
            IBuildConstructorParametersService buildConstructorParametersService,
            IPopulateComplexObjectService populateComplexObjectService,
            IPopulateListService populateListService,
            IPopulateDictionaryService populateDictionaryService,
            IPopulateArrayService populateArrayService)
        {
            this.populateStringService = populateStringService;
            this.populateIntService = populateIntService;
            this.populateNullableIntProperty = populateNullableIntProperty;
            this.populateDoubleService = populateDoubleService;
            this.populateNullableDoubleService = populateNullableDoubleService;
            this.populateBoolService = populateBoolService;
            this.populateNullableBoolService = populateNullableBoolService;
            this.populateByteService = populateByteService;
            this.populateNullableByteService = populateNullableByteService;
            this.populateDateTimeService = populateDateTimeService;
            this.populateNullableDateTimeService = populateNullableDateTimeService;
            this.populateUriService = populateUriService;
            this.populateGuidService = populateGuidService;
            this.populateLongService = populateLongService;
            this.populateNullableLongService = populateNullableLongService;
            this.populateShortService = populateShortService;
            this.populateCharService = populateCharService;
            this.populateNullableCharService = populateNullableCharService;
            this.populateDecimalService = populateDecimalService;
            this.populateNullableDecimalService = populateNullableDecimalService;
            this.populateEnumService = populateEnumService;
            this.buildConstructorParametersService = buildConstructorParametersService;
            this.populateComplexObjectService = populateComplexObjectService;
            this.populateListService = populateListService;
            this.populateDictionaryService = populateDictionaryService;
            this.populateArrayService = populateArrayService;
        }

        public void AddConfiguration(AutoBuilderConfiguration autoBuilderConfiguration)
        {
            configuration = autoBuilderConfiguration;
            populateStringService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateIntService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateNullableIntProperty.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateDoubleService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateNullableDoubleService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateBoolService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateNullableBoolService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateByteService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateNullableByteService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateDateTimeService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateNullableDateTimeService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateUriService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateGuidService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateLongService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateNullableLongService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateShortService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateCharService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateNullableCharService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateDecimalService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateNullableDecimalService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateEnumService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateComplexObjectService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateListService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateDictionaryService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateArrayService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        public object PopulateProperties(object objectToPopulate, int depth)
        {
            if (depth > configuration.MaxDepth)
            {
                return objectToPopulate;
            }

            var objectToPopulateType = objectToPopulate.GetType();

            if (objectToPopulateType.GetInterfaces().Any(x => x == typeof(IList)))
            {
                if (objectToPopulateType.FullName.Contains("System.Collections.Generic.List`1"))
                {
                    populateListService.Populate(string.Empty, objectToPopulateType, objectToPopulate, depth - 1, Populate);
                }
                else if (objectToPopulateType.FullName.Contains("System.Collections.Generic.Dictionary`2"))
                {
                    populateDictionaryService.Populate(string.Empty, objectToPopulateType, objectToPopulate, depth - 1, Populate, Populate);
                }
                else if (objectToPopulateType.BaseType == typeof(Array))
                {
                    objectToPopulate = populateArrayService.Populate(string.Empty, objectToPopulateType, objectToPopulate, depth - 1, Populate);
                }
            }
            else if (objectToPopulateType.GetInterfaces().Any(x => x == typeof(IDictionary)))
            {
                if (objectToPopulateType.FullName.Contains("System.Collections.Generic.Dictionary`2"))
                {
                    return populateDictionaryService.Populate(string.Empty, objectToPopulateType, objectToPopulate, depth, Populate, Populate);
                }
            }
            else
            {
                var properties = objectToPopulate.GetType().GetProperties();

                foreach (var propertyInfo in properties)
                {
                    if (propertyInfo.CanWrite)
                    {
                        propertyInfo.SetValue(
                            objectToPopulate,
                            Populate(depth, propertyInfo.Name, propertyInfo.PropertyType, propertyInfo.GetValue(objectToPopulate, null), propertyInfo), null);
                    }
                } 
            }

            return objectToPopulate;
        }

        public object[] BuildConstructorParameters(ConstructorInfo[] constructors, int depth)
        {
            return buildConstructorParametersService.Build(constructors, depth, Populate);
        }

        private static bool IsPotentialComplexType(Type propertyType)
        {
            return propertyType.BaseType != typeof(ValueType) && !propertyType.IsPrimitive && propertyType != typeof(string) && propertyType != typeof(Uri);
        }

        private object Populate(int depth, string propertyName, Type propertyType, object value, PropertyInfo propertyInfo = null)
        {
            if (propertyInfo != null)
            {
                if (!propertyInfo.CanWrite)
                {
                    return value;
                }    
            }

            if (propertyType.GetInterfaces().Any(x => x == typeof(IList)))
            {
                if (propertyType.FullName.Contains("System.Collections.Generic.List`1"))
                {
                    return populateListService.Populate(propertyName, propertyType, value, depth, Populate);
                }

                if (propertyType.BaseType == typeof(Array) && value == null)
                {
                    return populateArrayService.Populate(propertyName, propertyType, null, depth, Populate);
                }
            }

            if (propertyType.GetInterfaces().Any(x => x == typeof(IDictionary)))
            {
                if (propertyType.FullName.Contains("System.Collections.Generic.Dictionary`2"))
                {
                    return populateDictionaryService.Populate(string.Empty, propertyType, value, depth, Populate, Populate);
                }
            }

            if (propertyType == typeof(string))
            {
                return populateStringService.Populate(propertyName, (string)value, propertyInfo);
            }

            if (propertyType == typeof(int))
            {
                var intValue = value == null ? 0 : (int)value;
                return populateIntService.Populate(propertyName, intValue, propertyInfo);
            }

            if (propertyType == typeof(int?))
            {
                return populateNullableIntProperty.Populate(propertyName, (int?)value, propertyInfo);
            }

            if (propertyType == typeof(double))
            {
                var doubleValue = value == null ? 0 : (double)value;
                return populateDoubleService.Populate(propertyName, doubleValue, propertyInfo);
            }

            if (propertyType == typeof(double?))
            {
                return populateNullableDoubleService.Populate(propertyName, (double?)value, propertyInfo);
            }

            if (propertyType == typeof(bool))
            {
                var boolValue = value != null && (bool)value;
                return populateBoolService.Populate(propertyName, boolValue);
            }

            if (propertyType == typeof(bool?))
            {
                return populateNullableBoolService.Populate(propertyName, (bool?)value);
            }

            if (propertyType == typeof(byte))
            {
                var byteValue = value == null ? (byte)0 : (byte)value;
                return populateByteService.Populate(propertyName, byteValue);
            }

            if (propertyType == typeof(byte?))
            {
                return populateNullableByteService.Populate(propertyName, (byte?)value);
            }

            if (propertyType == typeof(DateTime))
            {
                var dateTimeValue = value == null ? default(DateTime) : (DateTime)value;
                return populateDateTimeService.Populate(propertyName, dateTimeValue);
            }

            if (propertyType == typeof(DateTime?))
            {
                return populateNullableDateTimeService.Populate(propertyName, (DateTime?)value);
            }

            if (propertyType == typeof(Uri))
            {
                return populateUriService.Populate(propertyName, (Uri)value);
            }

            if (propertyType == typeof(Guid))
            {
                return populateGuidService.Populate(propertyName, (Guid)value);
            }

            if (propertyType == typeof(long))
            {
                return populateLongService.Populate(propertyName, (long)value);
            }

            if (propertyType == typeof(long?))
            {
                return populateNullableLongService.Populate(propertyName, (long?)value);
            }

            if (propertyType == typeof(short))
            {
                return populateShortService.Populate(propertyName, (short)value);
            }

            if (propertyType == typeof(char))
            {
                var charValue = value == null ? default(char) : (char)value;
                return populateCharService.Populate(propertyName, charValue);
            }

            if (propertyType == typeof(char?))
            {
                return populateNullableCharService.Populate(propertyName, (char?)value);
            }

            if (propertyType == typeof(decimal))
            {
                var decimalValue = value == null ? 0 : (decimal)value;
                return populateDecimalService.Populate(propertyName, decimalValue);
            }

            if (propertyType == typeof(decimal?))
            {
                return populateNullableDecimalService.Populate(propertyName, (decimal?)value);
            }

            if (propertyType.BaseType == typeof(Enum) && value != null)
            {
                return populateEnumService.Populate(propertyName, propertyType, value);
            }
            
            if (IsPotentialComplexType(propertyType))
            {
                return populateComplexObjectService.Populate(
                    propertyName,
                    propertyType,
                    value,
                    depth,
                    buildConstructorParametersService.Build,
                    Populate,
                    PopulateProperties);
            }

            Console.WriteLine("Sorry, unable to fully build this model. Unsupported Type: " + propertyType);
            return null;
        }
    }
}
