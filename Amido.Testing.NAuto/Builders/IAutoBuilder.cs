using System;

namespace Amido.Testing.NAuto.Builders
{
    public interface IAutoBuilder<TModel> where TModel : class
    {
        IAutoBuilderOverrides<TModel> Construct();
        IAutoBuilderOverrides<TModel> ConstructWithSpecificParameters(params object[] constructorArguments);
        IAutoBuilder<TModel> ClearConventions();
        IAutoBuilder<TModel> AddConvention(string nameContains, Type type, Func<Object> result);
        IAutoBuilder<TModel> AddConventions(params ConventionMap[] conventionMaps);
        IAutoBuilder<TModel> Configure(Action<AutoBuilderConfiguration> configureAction);

    }
}