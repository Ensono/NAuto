using System;

namespace Amido.NAuto.Builders
{
    public interface IConditionalResult<TModel> where TModel : class
    {
        IAutoBuilderOverrides<TModel> Then(Action<TModel> expression);
    }

    public class ConditionalResult<TModel> : IConditionalResult<TModel> where TModel : class
    {
        private readonly IAutoBuilderOverrides<TModel> autoBuilder;
        private readonly TModel entity;
        private readonly bool isTrue;

        public ConditionalResult(IAutoBuilderOverrides<TModel> autoBuilder, TModel entity, bool isTrue)
        {
            this.autoBuilder = autoBuilder;
            this.entity = entity;
            this.isTrue = isTrue;
        }

        public IAutoBuilderOverrides<TModel> Then(Action<TModel> expression)
        {
            if (isTrue)
            {
                expression(entity);
            }

            return autoBuilder;
        }
    }
}