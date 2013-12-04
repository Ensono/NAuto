using System;

namespace Amido.NAuto.Builders
{
    public interface IAutoBuilder<TModel> where TModel : class
    {
        IAutoBuilderOverrides<TModel> Construct(params object[] constructorParameters);
        IAutoBuilder<TModel> ClearConventions();
        IAutoBuilder<TModel> ClearConvention(string nameContains, Type type);
        IAutoBuilder<TModel> AddConvention(string nameContains, Type type, Func<AutoBuilderConfiguration, Object> result);
        IAutoBuilder<TModel> AddConvention(ConventionMap conventionMap);
        IAutoBuilder<TModel> AddConventions(params ConventionMap[] conventionMaps);
        IAutoBuilder<TModel> Configure(Action<AutoBuilderConfiguration> configureAction);

    }
}