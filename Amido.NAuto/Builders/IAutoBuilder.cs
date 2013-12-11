using System;

namespace Amido.NAuto.Builders
{
    public interface IAutoBuilder<TModel> where TModel : class
    {
        IAutoBuilderOverrides<TModel> Construct(params object[] constructorParameters);

        IAutoBuilderOverrides<TModel> Empty(); 
            
        IAutoBuilderOverrides<TModel> Load(string relativeFilePath);

        IAutoBuilder<TModel> ClearConventions();

        IAutoBuilder<TModel> ClearConvention(string conventionFilter, Type type);

        IAutoBuilder<TModel> AddConvention(
            ConventionFilterType conventionFilterType,
            string conventionFilter,
            Type type,
            Func<AutoBuilderConfiguration, object> result);

        IAutoBuilder<TModel> AddConvention(ConventionMap conventionMap);

        IAutoBuilder<TModel> AddConventions(params ConventionMap[] conventionMaps);

        IAutoBuilder<TModel> Configure(Action<AutoBuilderConfiguration> configureAction);
    }
}