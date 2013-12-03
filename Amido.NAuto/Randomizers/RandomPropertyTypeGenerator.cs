namespace Amido.NAuto.Randomizers
{
    public static class RandomPropertyTypeGenerator
    {
        public static string GetRandomEmail()
        {
            var identifier = NAuto.GetRandomString(5, 25, CharacterSetType.AlphaNumeric, Spaces.None, Casing.Lowered);
            var domain = NAuto.GetRandomString(15, CharacterSetType.AlphaNumeric, Spaces.None, Casing.Lowered);
            return string.Format("{0}@{1}.com", identifier, domain);
        }

        public static string GetRandomUrl()
        {
            const string firstPart = "http://www.";
            var secondPart = NAuto.GetRandomString(5, 15, CharacterSetType.AlphaNumeric, Spaces.None, Casing.Lowered);
            const string lastPart = ".com";
            return string.Format("{0}{1}{2}", firstPart, secondPart, lastPart);
        }

        public static string GetRandomPostCode()
        {
            var firstPart = NAuto.GetRandomString(2, CharacterSetType.Alpha, Spaces.None, Casing.Uppered);
            var secondPart = NAuto.GetRandomString(1, 2, CharacterSetType.Numeric, Spaces.None, Casing.Uppered);
            var thirdPart = NAuto.GetRandomString(1, CharacterSetType.Numeric, Spaces.None, Casing.Uppered);

            var lastPart = NAuto.GetRandomString(2, CharacterSetType.Alpha, Spaces.None, Casing.Uppered);
            return string.Format("{0}{1} {2}{3}", firstPart, secondPart, thirdPart, lastPart);
        }

        public static string GetRandomTelephoneNumber()
        {
            var firstPart = NAuto.GetRandomString(4, CharacterSetType.Numeric, Spaces.None, Casing.Any);
            var secondPart = NAuto.GetRandomString(3, CharacterSetType.Numeric, Spaces.None, Casing.Any);
            var thirdPart = NAuto.GetRandomString(3, CharacterSetType.Numeric, Spaces.None, Casing.Any);
            return string.Format("0{0} {1} {2}", firstPart, secondPart, thirdPart);
        }
    }
}
