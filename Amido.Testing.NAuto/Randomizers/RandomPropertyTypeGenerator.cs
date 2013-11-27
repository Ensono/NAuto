namespace Amido.Testing.NAuto.Randomizers
{
    public static class RandomPropertyTypeGenerator
    {
        public static string GetRandomEmail()
        {
            var identifier = NAuto.GetRandomString(5, 25, CharacterSetType.AlphaNumeric, Casing.Lowered, Spaces.None);
            var domain = NAuto.GetRandomString(15, CharacterSetType.AlphaNumeric, Casing.Lowered, Spaces.None);
            return string.Format("{0}@{1}.com", identifier, domain);
        }

        public static string GetRandomUrl()
        {
            const string firstPart = "http://www.";
            var secondPart = NAuto.GetRandomString(5, 15, CharacterSetType.AlphaNumeric, Casing.Lowered, Spaces.None);
            const string lastPart = ".com";
            return string.Format("{0}{1}{2}", firstPart, secondPart, lastPart);
        }

        public static string GetRandomPostCode()
        {
            var firstPart = NAuto.GetRandomString(2, CharacterSetType.Alpha, Casing.Uppered, Spaces.None);
            var secondPart = NAuto.GetRandomString(1, 2, CharacterSetType.Numeric, Casing.Uppered, Spaces.None);
            var thirdPart = NAuto.GetRandomString(1, CharacterSetType.Numeric, Casing.Uppered, Spaces.None);

            var lastPart = NAuto.GetRandomString(2, CharacterSetType.Alpha, Casing.Uppered, Spaces.None);
            return string.Format("{0}{1} {2}{3}", firstPart, secondPart, thirdPart, lastPart);
        }

        public static string GetRandomTelephoneNumber()
        {
            var firstPart = NAuto.GetRandomString(4, CharacterSetType.Numeric, Casing.Any, Spaces.None);
            var secondPart = NAuto.GetRandomString(3, CharacterSetType.Numeric, Casing.Any, Spaces.None);
            var thirdPart = NAuto.GetRandomString(3, CharacterSetType.Numeric, Casing.Any, Spaces.None);
            return string.Format("0{0} {1} {2}", firstPart, secondPart, thirdPart);
        }
    }
}
