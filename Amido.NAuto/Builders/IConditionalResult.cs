using System;

namespace Amido.NAuto.Builders
{
    public interface IConditionalResult<TModel> where TModel : class
    {
        IAutoBuilderOverrides<TModel> Then(Action<TModel> expression);
    }
}