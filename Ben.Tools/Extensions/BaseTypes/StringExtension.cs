using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BenTools.Extensions.BaseTypes
{
    public static class StringExtension
    {
        public static string FirstLetterToUppercase(this string text) => $"{char.ToUpper(text[0])}{text.Substring(1)}";

        // "SalutCavaBienDeviendra" -> "Salut Cava Bien Deviendra"
        public static string AddSpaceBetweenUppercase(this string text) =>  Regex.Replace(text, @"\B[A-Z]", match => " " + match.ToString());

        // "my name is pierre" => "My Name Is Pierre".
        public static string FirstWordLetterToUppercase(this string text) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
        public static string ToTitleCase(this string text) => CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(text.ToLower(CultureInfo.CurrentUICulture));

        public static string ReplaceAccentLettersByNoAccentLetters(this string text) =>
            new string(text.Normalize(NormalizationForm.FormD)
                    .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    .ToArray())
                .Normalize(NormalizationForm.FormC);

    }
}
