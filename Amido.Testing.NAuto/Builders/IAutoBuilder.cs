namespace Amido.Testing.NAuto.Builders
{
    public interface IAutoBuilder<TModel> where TModel : class
    {
        IAutoBuilderOverrides<TModel> Construct();
        IAutoBuilderOverrides<TModel> ConstructWithSpecificParameters(params object[] constructorArguments);
    }
}