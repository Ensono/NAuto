using System;

namespace Amido.NAuto.Builders
{
    public interface IConditionalResult<TModel> where TModel : class
    {
        IAutoBuilderOverrides<TModel> Then(Action<TModel> expression);
    }

    public class ConditionalResult<TModel> : IConditionalResult<TModel> where TModel : class
    {
        private readonly AutoBuilder<TModel> autoBuilder;
        private readonly TModel entity;
        private readonly Func<TModel, bool> conditionalExpression;

        public ConditionalResult(AutoBuilder<TModel> autoBuilder, TModel entity, Func<TModel, bool> conditionalExpression)
        {
            this.autoBuilder = autoBuilder;
            this.entity = entity;
            this.conditionalExpression = conditionalExpression;
        }

        public IAutoBuilderOverrides<TModel> Then(Action<TModel> expression)
        {
            Action action = () =>
                {
                    if (conditionalExpression(autoBuilder.Entity))
                    {
                        expression(autoBuilder.Entity);
                    }
                };
            autoBuilder.Actions.Add(action);
           return autoBuilder;
        }
    }
}