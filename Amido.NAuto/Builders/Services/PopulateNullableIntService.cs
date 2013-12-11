using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateNullableIntService : PopulateProperty<int?>
    {
        private readonly IDataAnnotationConventionMapper dataAnnotationConventionMapper;

        public PopulateNullableIntService(IDataAnnotationConventionMapper dataAnnotationConventionMapper)
        {
            this.dataAnnotationConventionMapper = dataAnnotationConventionMapper;
        }

        public override int? Populate(string propertyName, int? currentValue, PropertyInfo propertyInfo = null)
        {
            if (currentValue != default(int?))
            {
                return currentValue;
            }

            return GetIntValue(propertyName, propertyInfo);
        }

        private int? GetIntValue(string propertyName, PropertyInfo propertyInfo)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(int?)))
            {
                return (int)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(int?), AutoBuilderConfiguration);
            }

            var annotatedType = dataAnnotationConventionMapper.TryGetValue(typeof(int), propertyInfo, AutoBuilderConfiguration);
            if (annotatedType != null)
            {
                return (int)annotatedType;
            }

            return NAuto.GetRandomInteger(AutoBuilderConfiguration.IntMinimum, AutoBuilderConfiguration.IntMaximum);
        }
    }
}