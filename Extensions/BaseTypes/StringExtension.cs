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
        public static byte[] EncodedPngBase64StringToBytes(this string encodedPngBase64String) =>
            Convert.FromBase64String(encodedPngBase64String.Replace("data:image/png;base64,", ""));

        public static Bitmap EncodedPngBase64StringToBitmap(this string encodedPngBase64String) =>
            BytesHelper.ToBitmap(encodedPngBase64String.EncodedPngBase64StringToBytes());

        public static string FirstLetterToUppercase(this string input) =>
            char.ToUpper(input[0]) + input.Substring(1);
         
        public static string RemoveTextOccurence(
            this string text,
            string textToRemove) => text.Replace(textToRemove, string.Empty);

        /// <summary>
        /// "MyNameIsFrank" -> "My Name Is Frank"
        /// </summary>
        public static string ReplaceUppercaseBySpaceAndUppercase(this string text) =>
            Regex.Replace(text, @"\B[A-Z]", m => " " + m.ToString());

        /// <summary>
        /// "my name is pierre" => "My Name Is Pierre".
        /// </summary>
        public static string FirstWordLetterToUppercase(this string text) =>
            CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
    }
}