using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using Amido.NAuto.Randomizers;

namespace Amido.NAuto
{
    /// <summary>
    /// NAuto - Test Data Builder.
    /// </summary>
    public static class NAuto
    {
        private static readonly Nject.NAutoContainer Container = new Nject.NAutoContainer();

        static NAuto()
        {
            ServiceInstaller.Install(Container);
        }

        /// <summary>
        /// Builds an instance using the test data builder pattern.
        /// </summary>
        /// <typeparam name="TTestDataBuilder">The type of the test data builder.</typeparam>
        /// <returns>Returns test builder.</returns>
        public static TTestDataBuilder Build<TTestDataBuilder>() where TTestDataBuilder : new()
        {
            return new TTestDataBuilder();
        }

        /// <summary>
        /// Automatically build an instance of the passed in model type.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <returns>Autobuilder instance.</returns>
        public static IAutoBuilder<TModel> AutoBuild<TModel>() where TModel : class
        {
            return new AutoBuilder<TModel>(Container.Resolve<IPropertyPopulationService>());
        }

        /// <summary>
        /// Automatically build an instance of the passed in model type.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="autoBuilderConfiguration">The configuration.</param>
        /// <returns>Autobuilder instance.</returns>
        public static IAutoBuilder<TModel> AutoBuild<TModel>(AutoBuilderConfiguration autoBuilderConfiguration) 
            where TModel : class
        {
            return new AutoBuilder<TModel>(Container.Resolve<IPropertyPopulationService>(), autoBuilderConfiguration);
        }

        /// <summary>
        /// Gets the random string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="language">The language.</param>
        /// <returns>Random string.</returns>
        public static string GetRandomString(int length, Language language = Language.English)
        {
            return RandomStringGenerator.Get(length, CharacterSetType.Anything, Spaces.Any, Casing.Any, language);
        }

        /// <summary>
        /// Gets the random string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="characterSetType">Type of the character set.</param>
        /// <param name="language">The language.</param>
        /// <returns>Random string.</returns>
        public static string GetRandomString(int length, CharacterSetType characterSetType, Language language = Language.English)
        {
            return RandomStringGenerator.Get(length, characterSetType, Spaces.Any, Casing.Any, language);
        }

        /// <summary>
        /// Gets the random string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="characterSetType">Type of the character set.</param>
        /// <param name="spaces">The spaces.</param>
        /// <param name="language">The language.</param>
        /// <returns>Random string.</returns>
        public static string GetRandomString(int length, CharacterSetType characterSetType, Spaces spaces, Language language = Language.English)
        {
            return RandomStringGenerator.Get(length, characterSetType, spaces, Casing.Any, language);
        }

        /// <summary>
        /// Gets the random string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="characterSetType">Type of the character set.</param>
        /// <param name="spaces">The spaces.</param>
        /// <param name="casing">The casing.</param>
        /// <param name="language">The language.</param>
        /// <returns>Random string.</returns>
        public static string GetRandomString(int length, CharacterSetType characterSetType, Spaces spaces, Casing casing, Language language = Language.English)
        {
            return RandomStringGenerator.Get(length, characterSetType, spaces, casing, language);
        }

        /// <summary>
        /// Gets the random string.
        /// </summary>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="characterSetType">Type of the character set.</param>
        /// <param name="spaces">The spaces.</param>
        /// <param name="language">The language.</param>
        /// <returns>Random string.</returns>
        public static string GetRandomString(int minLength, int maxLength, CharacterSetType characterSetType, Spaces spaces, Language language = Language.English)
        {
            return RandomStringGenerator.Get(minLength, maxLength, characterSetType, spaces, Casing.Any, language);
        }

        /// <summary>
        /// Gets the random string.
        /// </summary>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="characterSetType">Type of the character set.</param>
        /// <param name="spaces">The spaces.</param>
        /// <param name="casing">The casing.</param>
        /// <param name="language">The language.</param>
        /// <returns>Random string.</returns>
        public static string GetRandomString(int minLength, int maxLength, CharacterSetType characterSetType, Spaces spaces, Casing casing, Language language = Language.English)
        {
            return RandomStringGenerator.Get(minLength, maxLength, characterSetType, spaces, casing, language);
        }

        /// <summary>
        /// Gets the random integer.
        /// </summary>
        /// <param name="max">The maximum.</param>
        /// <returns>Random number.</returns>
        public static int GetRandomInteger(int max)
        {
            return RandomNumberGenerator.GetInteger(max);
        }

        /// <summary>
        /// Gets the random integer.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>Random number.</returns>
        public static int GetRandomInteger(int min, int max)
        {
            return RandomNumberGenerator.GetInteger(min, max);
        }

        /// <summary>
        /// Gets the random double.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>Random number.</returns>
        public static double GetRandomDouble(double min, double max)
        {
            return RandomNumberGenerator.GetDouble(min, max);
        }

        /// <summary>
        /// Gets the random list.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="numberOfItems">The number of items.</param>
        /// <param name="language">The language.</param>
        /// <returns>Random list.</returns>
        public static List<TModel> GetRandomList<TModel>(int numberOfItems = 2, Language language = Language.English) where TModel : class
        {
            return RandomListGenerator.Get<TModel>(numberOfItems, language);
        }

        /// <summary>
        /// Gets the random list with a sequenced column.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="identityProperty">The identity property.</param>
        /// <param name="numberOfItems">The number of items.</param>
        /// <param name="seed">The seed.</param>
        /// <param name="increment">The increment.</param>
        /// <param name="language">The language.</param>
        /// <returns>Random list.</returns>
        public static List<TModel> GetRandomList<TModel>(Expression<Func<TModel, int>> identityProperty, int numberOfItems = 2, int seed = 1, int increment = 1, Language language = Language.English) where TModel : class
        {
            return RandomListGenerator.Get(identityProperty, numberOfItems, seed, increment, language);
        }

        /// <summary>
        /// Gets a random string for a specified type such as email or url.
        /// </summary>
        /// <param name="propertyType">Type of the property.</param>
        /// <param name="language">The language.</param>
        /// <returns>Random string.</returns>
        public static string GetRandomPropertyType(PropertyType propertyType, Language language = Language.English)
        {
            switch (propertyType)
            {
                case PropertyType.Email:
                    return RandomPropertyTypeGenerator.GetRandomEmail(language);
                case PropertyType.Url:
                    return RandomPropertyTypeGenerator.GetRandomUrl(language);
                case PropertyType.PostalCode:
                    return RandomPropertyTypeGenerator.GetRandomPostalCode(language);
                case PropertyType.TelephoneNumber:
                    return RandomPropertyTypeGenerator.GetRandomTelephoneNumber(language);
            }

            return null;
        }
    }
}
