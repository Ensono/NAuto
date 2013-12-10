namespace Amido.NAuto.Randomizers
{
    public static class RandomPropertyTypeGenerator
    {
        public static string GetRandomEmail(Language language = Language.English)
        {
            var identifier = NAuto.GetRandomString(5, 25, CharacterSetType.AlphaNumeric, Spaces.None, Casing.Lowered, language);
            var domain = NAuto.GetRandomString(15, CharacterSetType.AlphaNumeric, Spaces.None, Casing.Lowered, language);
            return string.Format("{0}@{1}.com", identifier, domain);
        }

        public static string GetRandomUrl(Language language = Language.English)
        {
            const string FirstPart = "http://www.";
            var secondPart = NAuto.GetRandomString(5, 15, CharacterSetType.AlphaNumeric, Spaces.None, Casing.Lowered, language);
            const string LastPart = ".com";
            return string.Format("{0}{1}{2}", FirstPart, secondPart, LastPart);
        }

        public static string GetRandomPostalCode(Language language = Language.English)
        {
            var firstPart = NAuto.GetRandomString(2, CharacterSetType.Alpha, Spaces.None, Casing.Uppered, language);
            var secondPart = NAuto.GetRandomString(1, 2, CharacterSetType.Numeric, Spaces.None, Casing.Uppered, language);
            var thirdPart = NAuto.GetRandomString(1, CharacterSetType.Numeric, Spaces.None, Casing.Uppered, language);

            var lastPart = NAuto.GetRandomString(2, CharacterSetType.Alpha, Spaces.None, Casing.Uppered, language);
            return string.Format("{0}{1} {2}{3}", firstPart, secondPart, thirdPart, lastPart);
        }

        public static string GetRandomTelephoneNumber(Language language = Language.English)
        {
            var firstPart = NAuto.GetRandomString(4, CharacterSetType.Numeric, Spaces.None, Casing.Any, language);
            var secondPart = NAuto.GetRandomString(3, CharacterSetType.Numeric, Spaces.None, Casing.Any, language);
            var thirdPart = NAuto.GetRandomString(3, CharacterSetType.Numeric, Spaces.None, Casing.Any, language);
            if (language != Language.Chinese)
            {
                return string.Format("0{0} {1} {2}", firstPart, secondPart, thirdPart);    
            }

            return string.Format("{0} {1} {2}", firstPart, secondPart, thirdPart);
        }
    }
}
