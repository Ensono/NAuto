using Amido.Testing.NAuto.Builders;
using Amido.Testing.NAuto.Builders.Services;
using Amido.Testing.NAuto.Randomizers;

namespace Amido.Testing.NAuto
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

        public static string GetRandomString(int length)
        {
            return RandomStringGenerator.Get(length, CharacterSetType.Anything, Casing.Any, Spaces.Any);
        }

        public static string GetRandomString(int length, CharacterSetType characterSetType)
        {
            return RandomStringGenerator.Get(length, characterSetType, Casing.Any, Spaces.Any);
        }

        public static string GetRandomString(int length, CharacterSetType characterSetType, Casing casing)
        {
            return RandomStringGenerator.Get(length, characterSetType, casing, Spaces.Any);
        }

        public static string GetRandomString(int length, CharacterSetType characterSetType, Casing casing, Spaces spaces)
        {
            return RandomStringGenerator.Get(length, characterSetType, casing, spaces);
        }

        public static string GetRandomString(int minLength, int maxLength, CharacterSetType characterSetType, Casing casing)
        {
            return RandomStringGenerator.Get(minLength, maxLength, characterSetType, casing, Spaces.Any);
        }

        public static string GetRandomString(int minLength, int maxLength, CharacterSetType characterSetType, Casing casing, Spaces spaces)
        {
            return RandomStringGenerator.Get(minLength, maxLength, characterSetType, casing, spaces);
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

        public static string GetRandomPropertyType(PropertyType propertyType)
        {
            switch (propertyType)
            {
                case PropertyType.Email:
                    return RandomPropertyTypeGenerator.GetRandomEmail();
                case PropertyType.Url:
                    return RandomPropertyTypeGenerator.GetRandomUrl();
                case PropertyType.PostCode:
                    return RandomPropertyTypeGenerator.GetRandomPostCode();
                case PropertyType.TelephoneNumber:
                    return RandomPropertyTypeGenerator.GetRandomTelephoneNumber();
            }
            return null;
        }
    }
}
