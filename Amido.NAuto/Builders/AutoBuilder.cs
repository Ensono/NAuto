using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using Amido.NAuto.Builders.Services;
using Amido.NAuto.Randomizers;

using Amido.NAuto.Serializers;

namespace Amido.NAuto.Builders
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// AutoBuild models.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class AutoBuilder<TModel> : IAutoBuilder<TModel>, IAutoBuilderOverrides<TModel> where TModel : class
    {
        private readonly IPropertyPopulationService propertyPopulationService;
        private readonly AutoBuilderConfiguration configuration;
        private object[] constructorParameters;

        private bool isLoadedModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="propertyPopulationService">The property population service.</param>
        public AutoBuilder(IPropertyPopulationService propertyPopulationService)
            : this(propertyPopulationService, new AutoBuilderConfiguration())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="propertyPopulationService">The property population service.</param>
        /// <param name="configuration">The configuration.</param>
        public AutoBuilder(IPropertyPopulationService propertyPopulationService, AutoBuilderConfiguration configuration)
        {
            this.propertyPopulationService = propertyPopulationService;
            this.configuration = configuration;

            this.Actions = new List<Action>();
            isLoadedModel = false;
        }

        internal TModel Entity { get; set; }

        internal List<Action> Actions { get; set; }

        /// <summary>
        /// Clears the conventions.
        /// </summary>
        /// <returns>Returns this.</returns>
        public IAutoBuilder<TModel> ClearConventions()
        {
            configuration.Conventions.Clear();
            return this;
        }

        /// <summary>
        /// Clears the convention.
        /// </summary>
        /// <param name="conventionFilter">The convention filter.</param>
        /// <param name="type">The type.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilder<TModel> ClearConvention(string conventionFilter, Type type)
        {
            var conventionMap =
                configuration.Conventions.FirstOrDefault(x => x.ConventionFilter == conventionFilter && x.Type == type);
            if (conventionMap != null)
            {
                configuration.Conventions.Remove(conventionMap);
            }

            return this;
        }

        /// <summary>
        /// Adds the convention.
        /// </summary>
        /// <param name="conventionFilterType">Type of the convention filter.</param>
        /// <param name="conventionFilter">The convention filter.</param>
        /// <param name="type">The type.</param>
        /// <param name="result">The result.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilder<TModel> AddConvention(ConventionFilterType conventionFilterType, string conventionFilter, Type type, Func<AutoBuilderConfiguration, object> result)
        {
            configuration.Conventions.Add(new ConventionMap(conventionFilterType, conventionFilter, type, result));
            return this;
        }

        /// <summary>
        /// Adds the convention.
        /// </summary>
        /// <param name="conventionMap">The convention map.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilder<TModel> AddConvention(ConventionMap conventionMap)
        {
            configuration.Conventions.Add(conventionMap);
            return this;
        }

        /// <summary>
        /// Adds the conventions.
        /// </summary>
        /// <param name="conventionMaps">The convention maps.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilder<TModel> AddConventions(params ConventionMap[] conventionMaps)
        {
            configuration.Conventions.AddRange(conventionMaps);
            return this;
        }

        /// <summary>
        /// Provides write access to configuration settings.
        /// </summary>
        /// <param name="configure">The configure.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilder<TModel> Configure(Action<AutoBuilderConfiguration> configure)
        {
            configure.Invoke(configuration);
            return this;
        }

        /// <summary>
        /// Constructs the model.
        /// </summary>
        /// <param name="constructorArguments">The constructor arguments (if your class requires constructor arguments, NAuto will auto create them if not passed in).</param>
        /// <returns>Returns this.</returns>
        /// <exception cref="System.ArgumentException">
        /// Can't instantiate interfaces
        /// or
        /// Can't instantiate abstract classes
        /// </exception>
        public IAutoBuilderOverrides<TModel> Construct(params object[] constructorArguments)
        {
            if (typeof(TModel).IsInterface)
            {
                throw new ArgumentException("Can't instantiate interfaces");
            }

            if (typeof(TModel).IsAbstract)
            {
                throw new ArgumentException("Can't instantiate abstract classes");
            }

            this.constructorParameters = constructorArguments;

            propertyPopulationService.AddConfiguration(configuration);
           
            return this;
        }

        /// <summary>
        /// Loads the specified relative path.
        /// </summary>
        /// <param name="relativeFilePath">The relative path.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilderOverrides<TModel> Load(string relativeFilePath)
        {
            var currentDirectory = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"..\..\..\";
            var fullPath = currentDirectory + relativeFilePath;

            if (File.Exists(fullPath))
            {
                Entity = JsonSerializer.FromJsonString<TModel>(File.ReadAllText(fullPath));
                isLoadedModel = true;
            }

            return this;
        }

        /// <summary>
        /// Override model property.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilderOverrides<TModel> With(Action<TModel> expression)
        {
            this.Actions.Add(() => expression(this.Entity));
            return this;
        }

        /// <summary>
        /// Override model string property.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="propertyType">Type of the property.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            PropertyType propertyType)
        {
            this.Actions.Add(() => SetStringPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomPropertyType(propertyType), expression));
            return this;
        }

        /// <summary>
        /// Override model string property.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="length">The length.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            int length)
        {
            this.Actions.Add(() => SetStringPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomString(length), expression));
            return this;
        }

        /// <summary>
        /// Override model string property.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="length">The length.</param>
        /// <param name="characterSetType">Type of the character set.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            int length,
            CharacterSetType characterSetType)
        {
            this.Actions.Add(() => SetStringPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomString(length, characterSetType), expression));
            return this;
        }

        /// <summary>
        /// Override model string property.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="length">The length.</param>
        /// <param name="characterSetType">Type of the character set.</param>
        /// <param name="spaces">The spaces.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            int length,
            CharacterSetType characterSetType,
            Spaces spaces)
        {
            this.Actions.Add(() => SetStringPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomString(length, characterSetType, spaces), expression));
            return this;
        }

        /// <summary>
        /// Override model string property.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="length">The length.</param>
        /// <param name="characterSetType">Type of the character set.</param>
        /// <param name="spaces">The spaces.</param>
        /// <param name="casing">The casing.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression, 
            int length, 
            CharacterSetType characterSetType, 
            Spaces spaces, 
            Casing casing)
        {
            this.Actions.Add(() => SetStringPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomString(length, characterSetType, spaces, casing), expression));
            return this;
        }

        /// <summary>
        /// Override model string property.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="characterSetType">Type of the character set.</param>
        /// <param name="spaces">The spaces.</param>
        /// <param name="casing">The casing.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression, 
            int minLength, 
            int maxLength, 
            CharacterSetType characterSetType, 
            Spaces spaces, 
            Casing casing)
        {
            this.Actions.Add(() => SetStringPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomString(minLength, minLength, characterSetType, spaces, casing), expression));
            return this;
        }

        /// <summary>
        /// Override model string property.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="characterSetType">Type of the character set.</param>
        /// <param name="spaces">The spaces.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            int minLength,
            int maxLength,
            CharacterSetType characterSetType,
            Spaces spaces)
        {
            this.Actions.Add(() => SetStringPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomString(minLength, minLength, characterSetType, spaces), expression));
            return this;
        }

        /// <summary>
        /// Override model integer property.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilderOverrides<TModel> With(
           Expression<Func<TModel, int>> expression,
           int max)
        {
            this.Actions.Add(() => SetIntegerPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomInteger(max), expression));
            return this;
        }

        /// <summary>
        /// Override model integer property.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, int>> expression,
            int min,
            int max)
        {
            this.Actions.Add(() => SetIntegerPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomInteger(min, max), expression));
            return this;
        }

        /// <summary>
        /// Override model nullable integer property.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilderOverrides<TModel> With(
           Expression<Func<TModel, int?>> expression,
           int max)
        {
            this.Actions.Add(() => SetIntegerPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomInteger(max), expression));
            return this;
        }

        /// <summary>
        /// Override model nullable integer property.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilderOverrides<TModel> With(
           Expression<Func<TModel, int?>> expression,
           int min,
           int max)
        {
            this.Actions.Add(() => SetIntegerPropertyUsingNewRandomizerSetting(() => NAuto.GetRandomInteger(min, max), expression));
            return this;
        }

        /// <summary>
        /// Override model double property.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilderOverrides<TModel> With(
           Expression<Func<TModel, double>> expression,
           double min,
           double max)
        {
            this.Actions.Add(() => SetDoublePropertyUsingNewRandomizerSetting(() => NAuto.GetRandomDouble(min, max), expression));
            return this;
        }

        /// <summary>
        /// Override model nullable double property.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>Returns this.</returns>
        public IAutoBuilderOverrides<TModel> With(
           Expression<Func<TModel, double?>> expression,
           double min,
           double max)
        {
            this.Actions.Add(() => SetDoublePropertyUsingNewRandomizerSetting(() => NAuto.GetRandomDouble(min, max), expression));
            return this;
        }

        /// <summary>
        /// If then syntax for updating model.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Returns this.</returns>
        public IConditionalResult<TModel> If(Func<TModel, bool> expression)
        {
            return new ConditionalResult<TModel>(this, this.Entity, b => expression(this.Entity));
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>Returns the populated model.</returns>
        public TModel Build()
        {
            if (!isLoadedModel)
            {
                if (this.constructorParameters.Length > 0)
                {
                    this.Entity = (TModel)Activator.CreateInstance(typeof(TModel), this.constructorParameters);
                }
                else
                {
                    var constructors = typeof(TModel).GetConstructors();
                    if (typeof(TModel).BaseType == typeof(Array))
                    {
                        this.Entity =
                            (TModel)Activator.CreateInstance(typeof(TModel), configuration.DefaultCollectionItemCount);
                    }
                    else if (constructors.All(x => x.GetParameters().Count() != 0))
                    {
                        var constructorParameters = propertyPopulationService.BuildConstructorParameters(
                            constructors,
                            1);
                        this.Entity = (TModel)Activator.CreateInstance(typeof(TModel), constructorParameters);
                    }
                    else
                    {
                        this.Entity = (TModel)Activator.CreateInstance(typeof(TModel));
                    }
                }

                this.Entity = (TModel)propertyPopulationService.PopulateProperties(this.Entity, 1);
            }

            foreach (var action in this.Actions)
            {
                action();
            }

            return this.Entity;
        }

        /// <summary>
        /// Returns a JSON representation of the model.
        /// </summary>
        /// <param name="useCamelCase">
        /// Use Camel Case.
        /// </param>
        /// <param name="ignoreNulls">
        /// Ignore Nulls.
        /// </param>
        /// <param name="indentJson">
        /// Indent Json.
        /// </param>
        /// <returns>
        /// JSON model.
        /// </returns>
        public string ToJson(bool useCamelCase = true, bool ignoreNulls = true, bool indentJson = true)
        {
            this.Build();
            return JsonSerializer.ToIndentedJsonString(this.Entity, useCamelCase, ignoreNulls, indentJson);
        }

        private void SetStringPropertyUsingNewRandomizerSetting(
            Func<string> getRandomStringFunction, 
            Expression<Func<TModel, string>> expression)
        {
            var memberExpression = (MemberExpression)expression.Body;
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
            var memberExpression = (MemberExpression)expression.Body;
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
            var memberExpression = (MemberExpression)expression.Body;
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
            var memberExpression = (MemberExpression)expression.Body;
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
            var memberExpression = (MemberExpression)expression.Body;
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
                return this.Entity;
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
    }
}
