using System.Linq;
using BenTools.Helpers.BaseTypes;

namespace BenTools.Extensions.BaseTypes
{
    public static class CharExtension
    {
        #region Is Character Of Type
        public static bool IsDigit(this char @char) => @char >= '0' && @char <= '9';

        public static bool IsLowerLetterWithoutAccent(this char @char) => @char >= 'a' && @char <= 'z';

        public static bool IsUpperLetterWithoutAccent(this char @char) => @char >= 'A' && @char <= 'Z';

        public static bool IsLetterWithoutAccent(this char @char) => @char.IsLowerLetterWithoutAccent() || @char.IsUpperLetterWithoutAccent();

        public static bool IsLowerLetterWithAccent(this char @char) => StringHelper.AllLowerLettersWithAccents.Contains(@char);
        
        public static bool IsUpperLetterWithAccent(this char @char) => StringHelper.AllUpperLettersWithAccents.Contains(@char);

        public static bool IsLetterWithAccent(this char @char) => @char.IsLowerLetterWithAccent() || @char.IsUpperLetterWithAccent();

        public static bool IsLowerLetter(this char @char) => @char.IsLowerLetterWithoutAccent() || @char.IsLowerLetterWithAccent();

        public static bool IsUpperLetter(this char @char) => @char.IsUpperLetterWithoutAccent() || @char.IsUpperLetterWithAccent();

        public static bool IsLetter(this char @char) => @char.IsLetterWithoutAccent() || @char.IsLetterWithAccent();
        #endregion
    }
}
