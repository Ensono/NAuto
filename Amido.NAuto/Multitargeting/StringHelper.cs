using System;
using System.Globalization;

namespace Amido.NAuto.MultiTargeting
{
    internal static class StringHelper
    {
        internal static string ToTitleCase(this string text)
        {
#if NET40
            var textInfo = new CultureInfo("en-GB", false).TextInfo;
            return textInfo.ToTitleCase(text.ToLower());
#else
            var tokens = text.Split(new[] { " ", "-" }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];
                tokens[i] = token == token.ToUpper()
                    ? token
                    : token.Substring(0, 1).ToUpper() + token.Substring(1).ToLower();
            }

            return string.Join(" ", tokens);
#endif
        }

    }
}
