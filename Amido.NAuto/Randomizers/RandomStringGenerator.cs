using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Amido.NAuto.MultiTargeting;

namespace Amido.NAuto.Randomizers
{
    public class RandomStringGenerator
    {
        private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());
        private readonly List<string> _lowercase = new List<string>();
        private readonly List<string> _uppercase = new List<string>();
        private readonly List<string> _numbers = new List<string>();

        private static readonly List<string> EnglishNumbers = new List<string> { "0", "1", "2", "3", "4", "5", "6", "8", "9" };
        private static readonly List<string> SpecialCharacters = new List<string> { "!", "£", "$", "%", "^", "&", "*", "(", ")", "+", "{", "}", "[", "]", "@", "<", ">", "?", ":", ";", "~", "#", "|", "\\", "/", ",", "." };

        private static readonly List<string> EnglishLowercase = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        private static readonly List<string> EnglishUppercase = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        private static readonly List<string> CyrillicLowercase = new List<string> { "а", "б", "в", "г", "д", "е", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ" };
        private static readonly List<string> CyrillicUppercase = new List<string> { "Ѐ", "Ё", "Ђ", "Ѓ", "Є", "Ѕ", "І", "Ї", "Ј", "Љ", "Њ", "Ћ", "Ќ", "Ѝ", "Ў", "Џ", "А", "Б", "В", "Г", "Д", "Е", "Ж", "З", "И", "Й" };

        private static readonly List<string> SpanishLowercase = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "ñ", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        private static readonly List<string> SpanishUppercase = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "Ñ", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        private static readonly List<string> ItalianLowercase = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "z" };
        private static readonly List<string> ItalianUppercase = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "Z" };

        private static readonly List<string> GermanLowercase = new List<string> { "ä", "ö", "ü", "ß", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        private static readonly List<string> GermanUppercase = new List<string> { "ä", "ö", "ü", "ß", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        private static readonly List<string> Pinyin = new List<string> { "ēi", "bǐ", "xī", "dí", "yī", "ài fú", "jí", "ài chǐ", "ài", "jié", "kāi", "ài lè", "ài mǎ", "ài nà", "ó", "pì", "jí wú", "ài ér", "ài sī", "tí", "yī wú", "wéi", "dòu bèi ěr wéi", "yī kè sī", "wú ài", "zéi dé" };

        private static readonly List<string> Chinese = new List<string> { "安", "吧", "爸", "八", "百", "北", "不", "大", "岛", "的", "弟", "地", "东", "都", "对", "多", "儿", "二", "方", "港", "哥", "个", "关", "贵", "国", "过", "海", "好", "很", "会", "家", "见", "叫", "姐", "京", "九", "可", "老", "李", "零", "六", "吗", "妈", "么", "没", "美", "妹", "们", "明", "名", "哪", "那", "南", "你", "您", "朋", "七", "起", "千", "去", "人", "认", "日", "三", "上", "谁", "什", "生", "师", "识", "十", "是", "四", "他", "她", "台", "天", "湾", "万", "王", "我", "五", "西", "息", "系", "先", "香", "想", "小", "谢", "姓", "休", "学", "也", "一", "亿", "英", "友", "月", "再", "张", "这", "中", "字" };
        private static readonly List<string> ChineseNumbers = new List<string> { "〇", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };

        public string Get(
            int length,
            CharacterSetType characterSetType,
            Spaces spaces,
            Casing casing,
            Language language = Language.English)
        {
            SetLanguageCharacterSets(language);
            SetLanguageNumbers(language);
            var characters = GetCharacterSet(characterSetType, casing);

            var sb = BuildRandomString(length, spaces, characters);

            return ConvertToProperCaseIfRequired(sb.ToString(), casing);
        }

        public string Get(int minLength, int maxLength, CharacterSetType characterSetType, Spaces spaces, Casing casing, Language language = Language.English)
        {
            SetLanguageCharacterSets(language);
            SetLanguageNumbers(language);
            var characters = GetCharacterSet(characterSetType, casing);

            var length = _random.Next(minLength, maxLength);

            var sb = BuildRandomString(length, spaces, characters);

            return ConvertToProperCaseIfRequired(sb.ToString(), casing);
        }

        private void SetLanguageNumbers(Language language)
        {
            _numbers.Clear();
            if (language == Language.Chinese)
            {
                _numbers.AddRange(ChineseNumbers);
            }
            else
            {
                _numbers.AddRange(EnglishNumbers);
            }
        }

        private void SetLanguageCharacterSets(Language language)
        {
            _lowercase.Clear();
            _uppercase.Clear();
            switch (language)
            {
                case Language.Russian:
                    _lowercase.AddRange(CyrillicLowercase);
                    _uppercase.AddRange(CyrillicUppercase);
                    break;
                case Language.Chinese:
                    _lowercase.AddRange(Chinese);
                    _uppercase.AddRange(Chinese);
                    break;
                case Language.German:
                    _lowercase.AddRange(GermanLowercase);
                    _uppercase.AddRange(GermanUppercase);
                    break;
                case Language.Spanish:
                    _lowercase.AddRange(SpanishLowercase);
                    _uppercase.AddRange(SpanishUppercase);
                    break;
                case Language.Italian:
                    _lowercase.AddRange(ItalianLowercase);
                    _uppercase.AddRange(ItalianUppercase);
                    break;
                case Language.Pinyin:
                    _lowercase.AddRange(Pinyin);
                    _uppercase.AddRange(Pinyin);
                    break;
                default:
                    _lowercase.AddRange(EnglishLowercase);
                    _uppercase.AddRange(EnglishUppercase);
                    break;
            }
        }

        private int GetRandomNumber(int maxNumber)
        {
            return _random.Next(1, maxNumber);
        }

        private static string ConvertToProperCaseIfRequired(string text, Casing casing)
        {
            if (casing == Casing.ProperCase)
            {
                text.ToLower().ToTitleCase();
            }

            return text;
        }

        private StringBuilder BuildRandomString(int length, Spaces spaces, List<string> characters)
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
                randomMiddleSpaceLocation = _random.Next(1, length - 2);
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

        private List<string> GetCharacterSet(CharacterSetType characterSetType, Casing casing)
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

        private void GetNumericCharacterSet(List<string> characters)
        {
            characters.AddRange(_numbers);
        }

        private void GetAnythingCharacterSet(Casing casing, List<string> characters)
        {
            switch (casing)
            {
                case Casing.Lowered:
                    characters.AddRange(_numbers);
                    characters.AddRange(_lowercase);
                    characters.AddRange(SpecialCharacters);
                    break;
                case Casing.Uppered:
                    characters.AddRange(_numbers);
                    characters.AddRange(_uppercase);
                    characters.AddRange(SpecialCharacters);
                    break;
                default:
                    characters.AddRange(_numbers);
                    characters.AddRange(_lowercase);
                    characters.AddRange(_uppercase);
                    characters.AddRange(SpecialCharacters);
                    break;
            }
        }

        private void GetAlphaNumericCharacterSet(Casing casing, List<string> characters)
        {
            switch (casing)
            {
                case Casing.Lowered:
                case Casing.ProperCase:
                    characters.AddRange(_numbers);
                    characters.AddRange(_lowercase);
                    break;
                case Casing.Uppered:
                    characters.AddRange(_numbers);
                    characters.AddRange(_uppercase);
                    break;
                default:
                    characters.AddRange(_numbers);
                    characters.AddRange(_lowercase);
                    characters.AddRange(_uppercase);
                    break;
            }
        }

        private void GetAlphaCharacterSet(Casing casing, List<string> characters)
        {
            switch (casing)
            {
                case Casing.Lowered:
                case Casing.ProperCase:
                    characters.AddRange(_lowercase);
                    break;
                case Casing.Uppered:
                    characters.AddRange(_uppercase);
                    break;
                default:
                    characters.AddRange(_lowercase);
                    characters.AddRange(_uppercase);
                    break;
            }
        }
    }
}
