using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Amido.Testing.NAuto.Builders.Services;
using Amido.Testing.NAuto.Randomizers;

namespace Amido.Testing.NAuto.Builders
{
    public class AutoBuilder<TModel> : IAutoBuilder<TModel>, IAutoBuilderOverrides<TModel> where TModel : class
    {
        private readonly IPropertyPopulationService propertyPopulationService;
        private readonly AutoBuilderConfiguration configuration;
        private TModel entity;

        public AutoBuilder(IPropertyPopulationService propertyPopulationService) : this(propertyPopulationService, new AutoBuilderConfiguration()){}

        public AutoBuilder(IPropertyPopulationService propertyPopulationService, AutoBuilderConfiguration configuration)
        {
            this.propertyPopulationService = propertyPopulationService;
            this.configuration = configuration;
        }

        public IAutoBuilder<TModel> ClearConventions()
        {
            configuration.Conventions.Clear();
            return this;
        }

        public IAutoBuilder<TModel> AddConvention(string nameContains, Type type, Func<Object> result)
        {
            configuration.Conventions.Add(new ConventionMap(nameContains, type, result));
            return this;
        }

        public IAutoBuilder<TModel> AddConvention(ConventionMap conventionMap)
        {
            configuration.Conventions.Add(conventionMap);
            return this;
        }

        public IAutoBuilder<TModel> AddConventions(params ConventionMap[] conventionMaps)
        {
            configuration.Conventions.AddRange(conventionMaps);
            return this;
        }

        public IAutoBuilder<TModel> Configure(Action<AutoBuilderConfiguration> configure)
        {
            configure.Invoke(configuration);
            return this;
        }

        public IAutoBuilderOverrides<TModel> Construct(params object[] constructorArguments)
        {
            if (typeof(TModel).IsInterface)
            {
                throw new ArgumentException("Can't instantiate interfaces");
            }

            if (typeof (TModel).IsAbstract)
            {
                throw new ArgumentException("Can't instantiate abstract classes");
            }

            propertyPopulationService.AddConfiguration(configuration);

            if (constructorArguments.Length > 0)
            {
                entity = (TModel)Activator.CreateInstance(typeof(TModel), constructorArguments);
            }
            else
            {
                var constructors = typeof(TModel).GetConstructors();
                if((typeof(TModel).BaseType == typeof(Array)))
                {
                    entity = (TModel)Activator.CreateInstance(typeof(TModel), configuration.DefaultCollectionItemCount);
                }
                else if (constructors.All(x => x.GetParameters().Count() != 0))
                {
                    var constructorParameters = propertyPopulationService.BuildConstructorParameters(constructors, 1);
                    entity = (TModel)Activator.CreateInstance(typeof(TModel), constructorParameters);
                }
                else
                {
                    entity = (TModel)Activator.CreateInstance(typeof(TModel));
                }
            }

            entity = (TModel)propertyPopulationService.PopulateProperties(entity, 1);
            return this;
        }

        public IAutoBuilderOverrides<TModel> With(Action<TModel> expression)
        {
            expression(entity);
            return this;
        }

        public IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            PropertyType propertyType)
        {
            SetStringPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomPropertyType(propertyType), expression);
            return this;
        }

        public IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            int length)
        {
            SetStringPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomString(length), expression);
            return this;
        }

        public IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            int length,
            CharacterSetType characterSetType)
        {
            SetStringPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomString(length, characterSetType), expression);
            return this;
        }

        public IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            int length,
            CharacterSetType characterSetType,
            Spaces spaces)
        {
            SetStringPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomString(length, characterSetType, spaces), expression);
            return this;
        }

        public IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression, 
            int length, 
            CharacterSetType characterSetType, 
            Spaces spaces, 
            Casing casing)
        {
            SetStringPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomString(length, characterSetType, spaces, casing), expression);
            return this;
        }

        public IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression, 
            int minLength, 
            int maxLength, 
            CharacterSetType characterSetType, 
            Spaces spaces, 
            Casing casing)
        {
            SetStringPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomString(minLength, minLength, characterSetType, spaces, casing), expression);
            return this;
        }

        public IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            int minLength,
            int maxLength,
            CharacterSetType characterSetType,
            Spaces spaces)
        {
            SetStringPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomString(minLength, minLength, characterSetType, spaces), expression);
            return this;
        }

        public IAutoBuilderOverrides<TModel> With(
           Expression<Func<TModel, int>> expression,
           int max)
        {
            SetIntegerPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomInteger(max), expression);
            return this;
        }
        public IAutoBuilderOverrides<TModel> With(
           Expression<Func<TModel, int>> expression,
           int min,
           int max)
        {
            SetIntegerPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomInteger(min, max), expression);
            return this;
        }

        public IAutoBuilderOverrides<TModel> With(
           Expression<Func<TModel, int?>> expression,
           int max)
        {
            SetIntegerPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomInteger(max), expression);
            return this;
        }

        public IAutoBuilderOverrides<TModel> With(
           Expression<Func<TModel, int?>> expression,
           int min,
           int max)
        {
            SetIntegerPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomInteger(min, max), expression);
            return this;
        }

        public IAutoBuilderOverrides<TModel> With(
           Expression<Func<TModel, double>> expression,
           double min,
           double max)
        {
            SetDoublePropertyUsingNewRandomizerSetting(() => NAuto.GetRandomDouble(min, max), expression);
            return this;
        }

        public IAutoBuilderOverrides<TModel> With(
           Expression<Func<TModel, double?>> expression,
           double min,
           double max)
        {
            SetDoublePropertyUsingNewRandomizerSetting(() => NAuto.GetRandomDouble(min, max), expression);
            return this;
        }

        public TModel Build()
        {
            return entity;
        }

        private void SetStringPropertyUsingNewRandomizerSetting(
            Func<string> getRandomStringFunction, 
            Expression<Func<TModel, string>> expression)
        {
            var memberExpression = ((MemberExpression) expression.Body);
            var property = memberExpression.Member as PropertyInfo;
            var instanceToUpdate = GetInstance(memberExpression);
            if (property != null)
            {
                property.SetValue(instanceToUpdate, getRandomStringFunction());
            }
        }

        private void SetIntegerPropertyUsingNewRandomizerSetting(
            Func<int> getRandomIntegerFunction,
            Expression<Func<TModel, int>> expression)
        {
            var memberExpression = ((MemberExpression)expression.Body);
            var property = memberExpression.Member as PropertyInfo;
            var instanceToUpdate = GetInstance(memberExpression);
            if (property != null)
            {
                property.SetValue(instanceToUpdate, getRandomIntegerFunction());
            }
        }

        private void SetIntegerPropertyUsingNewRandomizerSetting(
           Func<int> getRandomIntegerFunction,
           Expression<Func<TModel, int?>> expression)
        {
            var memberExpression = ((MemberExpression)expression.Body);
            var property = memberExpression.Member as PropertyInfo;
            var instanceToUpdate = GetInstance(memberExpression);
            if (property != null)
            {
                property.SetValue(instanceToUpdate, getRandomIntegerFunction());
            }
        }

        private void SetDoublePropertyUsingNewRandomizerSetting(
            Func<double> getRandomDoubleFunction,
            Expression<Func<TModel, double>> expression)
        {
            var memberExpression = ((MemberExpression)expression.Body);
            var property = memberExpression.Member as PropertyInfo;
            var instanceToUpdate = GetInstance(memberExpression);
            if (property != null)
            {
                property.SetValue(instanceToUpdate, getRandomDoubleFunction());
            }
        }

        private void SetDoublePropertyUsingNewRandomizerSetting(
            Func<double> getRandomDoubleFunction,
            Expression<Func<TModel, double?>> expression)
        {
            var memberExpression = ((MemberExpression)expression.Body);
            var property = memberExpression.Member as PropertyInfo;
            var instanceToUpdate = GetInstance(memberExpression);
            if (property != null)
            {
                property.SetValue(instanceToUpdate, getRandomDoubleFunction());
            }
        }

        private object GetInstance(MemberExpression expression)
        {
            if (expression.Expression as MemberExpression == null)
            {
                return entity;
            }

            var body = GetInstance(expression.Expression as MemberExpression);

            var property = (expression.Expression as MemberExpression).Member as PropertyInfo;
            if (property != null)
            {
                var nextLevelInstance = property.GetValue(body);
                return nextLevelInstance;
            }
            return null;
        }

        public IConditionalResult<TModel> If(Func<TModel, bool> expression)
        {
            return new ConditionalResult<TModel>(this, entity, expression(entity));
        }
    }
}
