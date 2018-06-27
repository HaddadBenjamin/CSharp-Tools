using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BenTools.Helpers.BaseTypes;
using BenTools.Helpers.Streams;

namespace BenTools.Extensions.BaseTypes
{
    public static class StringExtension
	{
        private static readonly Regex SeveralSpacesRegex = new Regex(@"[ ]{2,}", RegexOptions.Compiled);

	    public static byte[] EncodedPngBase64StringToBytes(this string pngEncodedText) => Convert.FromBase64String(pngEncodedText.Replace("data:image/png;base64,", ""));

        public static Bitmap EncodedPngBase64StringToBitmap(this string pngEncodedText) => BytesHelper.ToBitmap(pngEncodedText.EncodedPngBase64StringToBytes());

        public static string FirstLetterToUppercase(this string text) => Char.ToUpper(text[0]) + text.Substring(1);

        public static string RemoveTextOccurence(this string text, string textToRemove) => text.Replace(textToRemove, String.Empty);

        /// <summary>
        /// "SalutCavaBienDeviendra" -> "Salut Cava Bien Deviendra"
        /// </summary>
        public static string AddSpaceBetweenUppercase(this string text) => Regex.Replace(text, @"\B[A-Z]", match => " " + match.ToString());

        /// <summary>
        /// "my name is pierre" => "My Name Is Pierre".
        /// </summary>
        public static string FirstWordLetterToUppercase(this string text) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);

        public static int ToInteger(this string text) => Int32.Parse(text);
	    
	    public static bool In(this string text, IEnumerable<string> collection) => collection.Contains(text);

        public static string RemoveNewLineAndCarriageReturn(this string text) =>
            text.Replace("\n", String.Empty)
                .Replace("\r", String.Empty);

	    public static bool IsNumber(this string text) =>
	        Int32.TryParse(text, out _);

	    public static bool AreJustDigits(this string text) => text.All(@char => @char.IsDigit());

	    public static bool AreJustUppers(this string text) => text.All(@char => @char.IsUpper());

	    public static bool AreJustLowers(this string text) => text.All(@char => @char.IsLower());

	    public static bool AreJustAccents(this string text) => text.All(@char => @char.IsAccent());

        /// <summary>
        /// Permet de compter les différents types de caractères d'une chaîne de caractères : minuscules, majuscules, chiffres, accents et le reste.
        /// </summary>
        public static (int digitCount, int upperCount, int lowerCount, int accentCount, int remainingCount) CountCharTypes(this string text)
	    {
	        if (String.IsNullOrWhiteSpace(text))
	            return (0, 0, 0, 0, 0);

	        var stringCharacterTypeCount = (
	            text.Count(@char => @char.IsDigit()),
	            text.Count(@char => @char.IsUpper()),
	            text.Count(@char => @char.IsLower()), 
	            text.Count(@char => @char.IsAccent()),
                0);

	        stringCharacterTypeCount.Item5 = text.Length - stringCharacterTypeCount.Item1 - stringCharacterTypeCount.Item2 - stringCharacterTypeCount.Item3 - stringCharacterTypeCount.Item4;

	        return stringCharacterTypeCount;
	    }

	    public static char[] GetDistinctAccents(this string text) =>
	        text.Intersect(StringHelper.BuildAllAccents())
	            .Distinct()
	            .ToArray();

	    public static string RemoveChar(this string text, char @char) =>
	        text.Replace(@char.ToString(), string.Empty);
           
	    public static string RemoveAccents(this string text) =>
	        String.Join(text, text.Split(text.GetDistinctAccents(), StringSplitOptions.RemoveEmptyEntries));

        public static string ToTitleCase(this string text) =>
	        CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(text.ToLower(CultureInfo.CurrentUICulture));

	    public static string ReplaceMultipleSpacesBySingleSpace(this string text) =>
	        SeveralSpacesRegex.Replace(text, " ");
	}
}
