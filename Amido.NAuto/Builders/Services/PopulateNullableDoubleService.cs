using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateNullableDoubleService : PopulateProperty<double?>
    {
        private readonly IDataAnnotationConventionMapper dataAnnotationConventionMapper;

        public PopulateNullableDoubleService(IDataAnnotationConventionMapper dataAnnotationConventionMapper)
        {
            this.dataAnnotationConventionMapper = dataAnnotationConventionMapper;
        }

        public override double? Populate(string propertyName, double? currentValue, PropertyInfo propertyInfo = null)
        {
            if (currentValue != default(double?))
            {
                return currentValue;
            }

            return GetDoubleValue(propertyName, propertyInfo);
        }

        private double? GetDoubleValue(string propertyName, PropertyInfo propertyInfo)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(double?)))
            {
                return (double?)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(double?), AutoBuilderConfiguration);
            }

            var annotatedType = dataAnnotationConventionMapper.TryGetValue(typeof(double), propertyInfo, AutoBuilderConfiguration);
            if (annotatedType != null)
            {
                return (double)annotatedType;
            }

            return NAuto.GetRandomDouble(AutoBuilderConfiguration.DoubleMinimum, AutoBuilderConfiguration.DoubleMaximum);
        }
    }
}