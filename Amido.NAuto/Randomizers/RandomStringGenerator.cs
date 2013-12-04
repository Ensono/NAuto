using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Amido.NAuto.Randomizers
{
    public static class RandomStringGenerator
    {
        private static readonly Random Random = new Random();
        private static List<string> englishNumbers = new List<string>{ "0", "1", "2", "3", "4", "5", "6", "8", "9" };
        private static List<string> SpecialCharacters = new List<string> { "!", "£", "$", "%", "^", "&", "*", "(", ")", "+", "{", "}", "[", "]", "@", "<", ">", "?", ":", ";", "~", "#", "|", "\\", "/", ",", "." };
        private static List<string> Lowercase = new List<string>();
        private static List<string> Uppercase = new List<string>();
        private static List<string> Numbers = new List<string>();


        private static List<string> englishLowercase = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        private static List<string> englishUppercase = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        private static List<string> cyrillicLowercase = new List<string> { "а", "б", "в", "г", "д", "е", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ" };
        private static List<string> cyrillicUppercase = new List<string> { "Ѐ", "Ё", "Ђ", "Ѓ", "Є", "Ѕ", "І", "Ї", "Ј", "Љ", "Њ", "Ћ", "Ќ", "Ѝ", "Ў", "Џ", "А", "Б", "В", "Г", "Д", "Е", "Ж", "З", "И", "Й" };

        private static List<string> spanishLowercase = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "ñ", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        private static List<string> spanishUppercase = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N","Ñ", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        private static List<string> italianLowercase = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "z" };
        private static List<string> italianUppercase = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "Z" };

        private static List<string> germanLowercase = new List<string> { "ä", "ö", "ü", "ß", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        private static List<string> germanUppercase = new List<string> { "ä", "ö", "ü", "ß", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        private static List<string> pinyin = new List<string> { "ēi", "bǐ", "xī", "dí", "yī", "ài fú", "jí", "ài chǐ", "ài", "jié", "kāi", "ài lè", "ài mǎ", "ài nà", "ó", "pì", "jí wú", "ài ér", "ài sī", "tí", "yī wú", "wéi", "dòu bèi ěr wéi", "yī kè sī", "wú ài", "zéi dé" };
     
        private static List<string> chinese = new List<string> { "安", "吧", "爸", "八", "百", "北", "不", "大", "岛", "的", "弟", "地", "东", "都", "对", "多", "儿", "二", "方", "港", "哥", "个", "关", "贵", "国", "过", "海", "好", "很", "会", "家", "见", "叫", "姐", "京", "九", "可", "老", "李", "零", "六", "吗", "妈", "么", "没", "美", "妹", "们", "明", "名", "哪", "那", "南", "你", "您", "朋", "七", "起", "千", "去", "人", "认", "日", "三", "上", "谁", "什", "生", "师", "识", "十", "是", "四", "他", "她", "台", "天", "湾", "万", "王", "我", "五", "西", "息", "系", "先", "香", "想", "小", "谢", "姓", "休", "学", "也", "一", "亿", "英", "友", "月", "再", "张", "这", "中", "字" };
        private static List<string> chineseNumbers = new List<string> { "〇", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };


        public static string Get(int length, CharacterSetType characterSetType, Spaces spaces, Casing casing,
            Language language = Language.English)
        {
            SetLanguageCharacterSets(language);
            SetLanguageNumbers(language);
            var characters = GetCharacterSet(characterSetType, casing);

            var sb = BuildRandomString(length, spaces, characters);

            return ConvertToProperCaseIfRequired(sb.ToString(), casing);
        }

        public static string Get(int minLength, int maxLength, CharacterSetType characterSetType, Spaces spaces, Casing casing, Language language = Language.English)
        {
            SetLanguageCharacterSets(language);
            SetLanguageNumbers(language);
            var characters = GetCharacterSet(characterSetType, casing);

            var length = Random.Next(minLength, maxLength);

            var sb = BuildRandomString(length, spaces, characters);

            return ConvertToProperCaseIfRequired(sb.ToString(), casing);
        }

        private static void SetLanguageNumbers(Language language)
        {
            Numbers.Clear();
            if (language == Language.Chinese)
            {
                Numbers.AddRange(chineseNumbers);
            }
            else
            {
                Numbers.AddRange(englishNumbers);
            }
        }

        private static void SetLanguageCharacterSets(Language language)
        {
            Lowercase.Clear();
            Uppercase.Clear();
            switch (language)
            {
                    case Language.Russian:
                    Lowercase.AddRange(cyrillicLowercase);
                    Uppercase.AddRange(cyrillicUppercase);
                    break;
                    case Language.Chinese:
                    Lowercase.AddRange(chinese);
                    Uppercase.AddRange(chinese);
                    break;
                    case Language.German:
                    Lowercase.AddRange(germanLowercase);
                    Uppercase.AddRange(germanUppercase);
                    break;
                    case Language.Spanish:
                    Lowercase.AddRange(spanishLowercase);
                    Uppercase.AddRange(spanishUppercase);
                    break;
                    case Language.Italian:
                    Lowercase.AddRange(italianLowercase);
                    Uppercase.AddRange(italianUppercase);
                    break;
                    case Language.Pinyin:
                    Lowercase.AddRange(pinyin);
                    Uppercase.AddRange(pinyin);
                    break;
                default:
                    Lowercase.AddRange(englishLowercase);
                    Uppercase.AddRange(englishUppercase);
                    break;
            }
        }


        private static int GetRandomNumber(int maxNumber)
        {
            return Random.Next(1, maxNumber);
        }

        private static string ConvertToProperCaseIfRequired(string text, Casing casing)
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

    public enum Language
    {
        English,
        Russian,
        Chinese,
        Spanish,
        Italian,
        German,
        Pinyin
    }
}
