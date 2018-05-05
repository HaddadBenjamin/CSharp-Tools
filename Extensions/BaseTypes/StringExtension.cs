using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Ben.Tools.Helpers.Streams;

namespace Ben.Tools.Extensions.BaseTypes
{
    public static class StringExtension
	{
        public static byte[] EncodedPngBase64StringToBytes(this string pngEncodedText) => Convert.FromBase64String(pngEncodedText.Replace("data:image/png;base64,", ""));

        public static Bitmap EncodedPngBase64StringToBitmap(this string pngEncodedText) => BytesHelper.ToBitmap(pngEncodedText.EncodedPngBase64StringToBytes());

        public static string FirstLetterToUppercase(this string text) => char.ToUpper(text[0]) + text.Substring(1);

        public static string RemoveTextOccurence(this string text, string textToRemove) => text.Replace(textToRemove, string.Empty);

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
    }
}
