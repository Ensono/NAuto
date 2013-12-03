using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public abstract class PopulateProperty<TProperty>
    {
        protected AutoBuilderConfiguration AutoBuilderConfiguration { get; set; }

        public void SetAutoBuilderConfiguration(AutoBuilderConfiguration autoBuilderConfiguration)
        {
            AutoBuilderConfiguration = autoBuilderConfiguration;
        }

        public abstract TProperty Populate(string propertyName, TProperty currentValue, PropertyInfo propertyInfo = null);
    }
}