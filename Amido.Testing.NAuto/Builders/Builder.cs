using System;

namespace Amido.Testing.NAuto.Builders
{
    public class Builder<TTestDataBuilder, TModel>
        where TTestDataBuilder : Builder<TTestDataBuilder, TModel>, new()
        where TModel : new()
    {
        private readonly TModel entity = new TModel();

        protected Builder()
        {
        }

        protected TModel Entity
        {
            get { return entity; }
        }

        public TTestDataBuilder With(Action<TModel> expression)
        {
            expression(entity);
            return (TTestDataBuilder)this;
        }

        public TModel Build()
        {
            return entity;
        }
    }
}