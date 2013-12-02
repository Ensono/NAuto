using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace Amido.Testing.NAuto.Builders.Services
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
        private readonly IPopulateEnumService populateEnumService;
        private readonly IBuildConstructorParametersService buildConstructorParametersService;
        private readonly IPopulateComplexObjectService populateComplexObjectService;
        private readonly IPopulateListService populateListService;
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
            IPopulateEnumService populateEnumService,
            IBuildConstructorParametersService buildConstructorParametersService,
            IPopulateComplexObjectService populateComplexObjectService,
            IPopulateListService populateListService,
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
            this.populateEnumService = populateEnumService;
            this.buildConstructorParametersService = buildConstructorParametersService;
            this.populateComplexObjectService = populateComplexObjectService;
            this.populateListService = populateListService;
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
            populateEnumService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateComplexObjectService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateListService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
            populateArrayService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
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

        public object[] BuildConstructorParameters(ConstructorInfo[] constructors, int depth)
        {
            return buildConstructorParametersService.Build(constructors, depth, Populate);
        }

        private object Populate(int depth, string propertyName, Type propertyType, object value)
        {
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

            if (propertyType == typeof (string))
            {
                return populateStringService.Populate(propertyName, (string)value);
            }

            if (propertyType == typeof(int))
            {
                var intValue = value == null ? 0 : (int) value;
                return populateIntService.Populate(propertyName, intValue);
            }

            if (propertyType == typeof(int?))
            {
                return populateNullableIntProperty.Populate(propertyName, (int?)value);
            }

            if (propertyType == typeof(double))
            {
                var doubleValue = value == null ? 0 : (double) value;
                return populateDoubleService.Populate(propertyName, doubleValue);
            }

            if (propertyType == typeof(double?))
            {
                return populateNullableDoubleService.Populate(propertyName, (double?)value);
            }

            if (propertyType == typeof(bool))
            {
                var boolValue = value != null && (bool) value;
                return populateBoolService.Populate(propertyName, boolValue);
            }

            if (propertyType == typeof(bool?))
            {
                return populateNullableBoolService.Populate(propertyName, (bool?) value);
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
                var dateTimeValue = value == null ? default(DateTime) : (DateTime) value;
                return populateDateTimeService.Populate(propertyName, dateTimeValue);
            }

            if (propertyType == typeof(DateTime?))
            {
                return populateNullableDateTimeService.Populate(propertyName, (DateTime?) value);
            }

            if (propertyType == typeof(Uri))
            {
                return populateUriService.Populate(propertyName, (Uri)value);
            }

            if (propertyType.BaseType == typeof(Enum) && value != null)
            {
                return populateEnumService.Populate(propertyName, propertyType, value);
            }
            
            if (IsPotentialComplexType(propertyType))
            {
                return populateComplexObjectService.Populate(propertyName, propertyType, value, depth,
                    buildConstructorParametersService.Build, Populate, PopulateProperties);
            }
            Console.WriteLine("Sorry, unable to fully build this model. Unsupported Type: " + propertyType);
            return null;
        }

        private static bool IsPotentialComplexType(Type propertyType)
        {
            return propertyType.BaseType != typeof(ValueType) && !propertyType.IsPrimitive && propertyType != typeof(string) && propertyType != typeof(Uri);
        }
    }
}
