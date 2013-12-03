using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Amido.NAuto.Randomizers
{
    public static class RandomStringGenerator
    {
        private static readonly Random Random = new Random();
        private static readonly List<string> Numbers = new List<string>{ "0", "1", "2", "3", "4", "5", "6", "8", "9" };
        private static readonly List<string> SpecialCharacters = new List<string> { "!", "£", "$", "%", "^", "&", "*", "(", ")", "+", "{", "}", "[", "]", "@", "<", ">", "?", ":", ";", "~", "#", "|", "\\", "/", ",", "." };
        private static readonly List<string> Lowercase = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        private static readonly List<string> Uppercase = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        public static int GetRandomNumber(int maxNumber)
        {
            return Random.Next(1, maxNumber);
        }

        public static string Get(int length, CharacterSetType characterSetType, Spaces spaces, Casing casing)
        {
            var characters = GetCharacterSet(characterSetType, casing);

            var sb = BuildRandomString(length, spaces, characters);

            return ConvertToProperCaseIfRequired(sb.ToString(), casing);
        }

        public static string Get(int minLength, int maxLength, CharacterSetType characterSetType, Spaces spaces, Casing casing)
        {
            var characters = GetCharacterSet(characterSetType, casing);

            var length = Random.Next(minLength, maxLength);

            var sb = BuildRandomString(length, spaces, characters);

            return ConvertToProperCaseIfRequired(sb.ToString(), casing);
        }

        public static string ConvertToProperCaseIfRequired(string text, Casing casing)
        {
            if (casing == Casing.ProperCase)
            {
                var textInfo = new CultureInfo("en-GB", false).TextInfo;
                return textInfo.ToTitleCase(text.ToLower());
            }
            return text;
        }

        private static StringBuilder BuildRandomString(int length, Spaces spaces, List<string> characters)
        {
            switch (spaces)
            {
                case Spaces.Any:
                    characters.Add(" ");
                    break;
            }

            var sb = new StringBuilder();
            var randomMiddleSpaceLocation = 0;

            if (length - 2 > 0)
            {
                randomMiddleSpaceLocation = Random.Next(1, length - 2);
            }

            for (var i = 0; i < length; i++)
            {
                if (i == 0 && (spaces == Spaces.Start || spaces == Spaces.StartAndEnd))
                {
                    sb.Append(" ");
                    continue;
                }

                if (i == length - 1 && (spaces == Spaces.End || spaces == Spaces.StartAndEnd))
                {
                    sb.Append(" ");
                    continue;
                }

                if (spaces == Spaces.Middle && randomMiddleSpaceLocation > 0 && i == randomMiddleSpaceLocation)
                {
                    sb.Append(" ");
                    continue;
                }

                sb.Append(characters[GetRandomNumber(characters.Count - 1)]);
            }
            return sb;
        }

        private static List<string> GetCharacterSet(CharacterSetType characterSetType, Casing casing)
        {
            var characters = new List<string>();

            switch (characterSetType)
            {
                case CharacterSetType.Alpha:
                    GetAlphaCharacterSet(casing, characters);
                    break;
                case CharacterSetType.AlphaNumeric:
                    GetAlphaNumericCharacterSet(casing, characters);
                    break;
                case CharacterSetType.Numeric:
                    GetNumericCharacterSet(characters);
                    break;
                default:
                    GetAnythingCharacterSet(casing, characters);
                    break;
            }
            return characters;
        }

        private static void GetNumericCharacterSet(List<string> characters)
        {
            characters.AddRange(Numbers);
        }

        private static void GetAnythingCharacterSet(Casing casing, List<string> characters)
        {
            switch (casing)
            {
                case Casing.Lowered:
                    characters.AddRange(Numbers);
                    characters.AddRange(Lowercase);
                    characters.AddRange(SpecialCharacters);
                    break;
                case Casing.Uppered:
                    characters.AddRange(Numbers);
                    characters.AddRange(Uppercase);
                    characters.AddRange(SpecialCharacters);
                    break;
                default:
                    characters.AddRange(Numbers);
                    characters.AddRange(Lowercase);
                    characters.AddRange(Uppercase);
                    characters.AddRange(SpecialCharacters);
                    break;
            }
        }

        private static void GetAlphaNumericCharacterSet(Casing casing, List<string> characters)
        {
            switch (casing)
            {
                case Casing.Lowered:
                case Casing.ProperCase:
                    characters.AddRange(Numbers);
                    characters.AddRange(Lowercase);
                    break;
                case Casing.Uppered:
                    characters.AddRange(Numbers);
                    characters.AddRange(Uppercase);
                    break;
                default:
                    characters.AddRange(Numbers);
                    characters.AddRange(Lowercase);
                    characters.AddRange(Uppercase);
                    break;
            }
        }

        private static void GetAlphaCharacterSet(Casing casing, List<string> characters)
        {
            switch (casing)
            {
                case Casing.Lowered:
                case Casing.ProperCase:
                    characters.AddRange(Lowercase);
                    break;
                case Casing.Uppered:
                    characters.AddRange(Uppercase);
                    break;
                default:
                    characters.AddRange(Lowercase);
                    characters.AddRange(Uppercase);
                    break;
            }
        }
    }

    public enum CharacterSetType
    {
        Anything,
        Alpha,
        AlphaNumeric,
        Numeric
    }

    public enum Casing
    {
        Any,
        Lowered,
        Uppered,
        ProperCase
    }

    public enum Spaces
    {
        None,
        Any,
        Start,
        Middle,
        End,
        StartAndEnd
    }
}
