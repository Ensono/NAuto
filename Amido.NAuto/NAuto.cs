using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using Amido.NAuto.Randomizers;

namespace Amido.NAuto
{
    public static class NAuto
    {

        static readonly Nject.NAutoContainer Container = new Nject.NAutoContainer();

        static NAuto()
        {
            ServiceInstaller.Install(Container);
        }
        public static TTestDataBuilder Build<TTestDataBuilder>() where TTestDataBuilder : new()
        {
            return new TTestDataBuilder();
        }


        public static IAutoBuilder<TModel> AutoBuild<TModel>() where TModel : class
        {
            return new AutoBuilder<TModel>(Container.Resolve<IPropertyPopulationService>());
        }

        public static IAutoBuilder<TModel> AutoBuild<TModel>(AutoBuilderConfiguration autoBuilderConfiguration) 
            where TModel : class
        {
            return new AutoBuilder<TModel>(Container.Resolve<IPropertyPopulationService>(), autoBuilderConfiguration);
        }

        public static string GetRandomString(int length, Language language = Language.English)
        {
            return RandomStringGenerator.Get(length, CharacterSetType.Anything, Spaces.Any, Casing.Any, language);
        }

        public static string GetRandomString(int length, CharacterSetType characterSetType, Language language = Language.English)
        {
            return RandomStringGenerator.Get(length, characterSetType, Spaces.Any, Casing.Any, language);
        }

        public static string GetRandomString(int length, CharacterSetType characterSetType, Spaces spaces, Language language = Language.English)
        {
            return RandomStringGenerator.Get(length, characterSetType, spaces, Casing.Any, language);
        }

        public static string GetRandomString(int length, CharacterSetType characterSetType, Spaces spaces, Casing casing, Language language = Language.English)
        {
            return RandomStringGenerator.Get(length, characterSetType, spaces, casing, language);
        }

        public static string GetRandomString(int minLength, int maxLength, CharacterSetType characterSetType, Spaces spaces, Language language = Language.English)
        {
            return RandomStringGenerator.Get(minLength, maxLength, characterSetType, spaces, Casing.Any, language);
        }

        public static string GetRandomString(int minLength, int maxLength, CharacterSetType characterSetType, Spaces spaces, Casing casing, Language language = Language.English)
        {
            return RandomStringGenerator.Get(minLength, maxLength, characterSetType, spaces, casing, language);
        }

        public static int GetRandomInteger(int max)
        {
            return RandomNumberGenerator.GetInteger(max);
        }

        public static int GetRandomInteger(int min, int max)
        {
            return RandomNumberGenerator.GetInteger(min, max);
        }

        public static double GetRandomDouble(double min, double max)
        {
            return RandomNumberGenerator.GetDouble(min, max);
        }

        public static List<TModel> GetRandomList<TModel>(int numberOfItems = 2, Language language = Language.English) where TModel : class
        {
            return RandomListGenerator.Get<TModel>(numberOfItems, language);
        }

        public static List<TModel> GetRandomList<TModel>(Expression<Func<TModel, int>> identityProperty, int numberOfItems = 2, int seed = 1, int increment = 1, Language language = Language.English) where TModel : class
        {
            return RandomListGenerator.Get(identityProperty, numberOfItems, seed, increment, language);
        }

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
