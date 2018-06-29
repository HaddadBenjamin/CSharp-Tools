using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BenTools.Extensions.Sequences;
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

	    public static bool IsNumber(this string text) => Int32.TryParse(text, out _);

        #region Are Just Characters Of Type
	    public static bool AreDigits(this string text) => text.All(@char => @char.IsDigit());

        public static bool AreLowerLettersWithoutAccent(this string text) => text.All(@char => @char.IsLowerLetterWithoutAccent());

	    public static bool AreUpperLettersWithoutAccent(this string text) => text.All(@char => @char.IsUpperLetterWithoutAccent());

	    public static bool AreLettersWithoutAccent(this string text) => text.All(@char => @char.IsLetterWithoutAccent());

	    public static bool AreLowerLettersWithAccent(this string text) => text.All(@char => @char.IsLowerLetterWithAccent());

	    public static bool AreUpperLettersWithAccent(this string text) => text.All(@char => @char.IsUpperLetterWithAccent());

	    public static bool AreLettersWithAccent(this string text) => text.All(@char => @char.IsLetterWithAccent());

	    public static bool AreLowerLetters(this string text) => text.All(@char => @char.IsLowerLetter());

	    public static bool AreUpperLetters(this string text) => text.All(@char => @char.IsUpperLetter());

        public static bool AreLetters(this string text) => text.All(@char => @char.IsLetter());
        #endregion

        #region Count Characters Of Types
        public class CharactersOfTypesCount
	    {
	        public int DigitCount;
	        public int LowerLetterWithoutAccentCount;
	        public int UpperLetterWithoutAccentCount;
	        public int LetterWithoutAccentCount;
	        public int LowerLetterWithAccentCount;
	        public int UpperLetterWithAccentCount;
	        public int LetterWithAccentCount;
	        public int LowerLettersCount;
	        public int UpperLettersCount;
	        public int LettersCount;
	        public int RemainingCharacterCount;
	        public int TotalCharacterCount;

            public CharactersOfTypesCount() { }

            public CharactersOfTypesCount(
	            int digitCount,
	            int lowerLetterWithoutAccentCount,
	            int upperLetterWithoutAccentCount,
	            int lowerLetterWithAccentCount,
	            int upperLetterWithAccentCount,
	            int remainingCharacterCount)
	        {
	            DigitCount = digitCount;

	            LowerLetterWithoutAccentCount = lowerLetterWithoutAccentCount;
	            UpperLetterWithoutAccentCount = upperLetterWithoutAccentCount;
                LowerLetterWithAccentCount = lowerLetterWithAccentCount;
	            UpperLetterWithAccentCount = upperLetterWithAccentCount;

	            LetterWithoutAccentCount = lowerLetterWithoutAccentCount + upperLetterWithoutAccentCount;
	            LetterWithAccentCount = lowerLetterWithAccentCount + upperLetterWithAccentCount;

	            LowerLettersCount = LowerLetterWithoutAccentCount + LowerLetterWithAccentCount;
	            UpperLettersCount = UpperLetterWithoutAccentCount + UpperLetterWithAccentCount;

	            LettersCount = LowerLettersCount + UpperLettersCount;

	            RemainingCharacterCount = remainingCharacterCount;

	            TotalCharacterCount = DigitCount + LettersCount;
	        }
        }
        /// <summary>
        /// Permet de compter les différents types de caractères d'une chaîne de caractères : minuscules, majuscules, chiffres, accents, etc..
        /// </summary>
        public static CharactersOfTypesCount CountCharactersTypes(this string text)
        {
            if (String.IsNullOrWhiteSpace(text))
                return new CharactersOfTypesCount();

            var digitCount = 0;
            var lowerLetterWithoutAccentCount = 0;
            var upperLetterWithoutAccentCount = 0;
            var lowerLetterWithAccentCount = 0;
            var upperLetterWithAccentCount = 0;
            var remainingCharacterCount = 0;

            for (int charIndex = 0; charIndex < text.Length; charIndex++)
            {
                var @char = text.ElementAt(charIndex);

                if (@char.IsDigit()) digitCount++;
                else if (@char.IsLowerLetterWithoutAccent()) lowerLetterWithoutAccentCount++;
                else if (@char.IsUpperLetterWithoutAccent()) upperLetterWithoutAccentCount++;
                else if (@char.IsLowerLetterWithAccent()) lowerLetterWithAccentCount++;
                else if (@char.IsUpperLetterWithAccent()) upperLetterWithAccentCount++;
                else remainingCharacterCount++;
            }
           
	        return new CharactersOfTypesCount(digitCount, lowerLetterWithoutAccentCount, upperLetterWithoutAccentCount, lowerLetterWithAccentCount, upperLetterWithAccentCount, remainingCharacterCount);
	    }
        #endregion

        public static char[] GetDistinctAccents(this string text) =>
	        text.Intersect(StringHelper.AllLettersWithAccents)
	            .Distinct()
	            .ToArray();

	    public static string ReplaceAccentLettersByNoAccentLetters(this string text) =>
	        new string(text.Normalize(NormalizationForm.FormD)
	                       .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
	                       .ToArray())
	            .Normalize(NormalizationForm.FormC);


        public static string RemoveChar(this string text, char @char) =>
	        text.Replace(@char.ToString(), string.Empty);
           
	    public static string RemoveAccents(this string text) =>
	        String.Join(text, text.Split(text.GetDistinctAccents(), StringSplitOptions.RemoveEmptyEntries));

        public static string ToTitleCase(this string text) =>
	        CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(text.ToLower(CultureInfo.CurrentUICulture));

	    public static string ReplaceMultipleSpacesBySingleSpace(this string text) =>
	        SeveralSpacesRegex.Replace(text, " ");

	    public static IEnumerable<int> FindAllTextOccurenceIndices(this string text, string searchText)
	    {
	        var indices = new List<int>();

	        for (int index = 0; ; index += searchText.Length)
	        {
	            index = text.IndexOf(searchText, index);

	            if (index == -1)
	                return indices;

	            indices.Add(index);
	        }
	    }
    }
}
