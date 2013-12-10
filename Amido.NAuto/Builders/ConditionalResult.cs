namespace Amido.NAuto.Builders
{
    using System;

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
                    if (this.conditionalExpression(this.autoBuilder.Entity))
                    {
                        expression(this.autoBuilder.Entity);
                    }
                };
            this.autoBuilder.Actions.Add(action);
            return this.autoBuilder;
        }
    }
}