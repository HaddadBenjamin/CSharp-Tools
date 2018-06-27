using System.Linq;
using BenTools.Helpers.BaseTypes;

namespace BenTools.Extensions.BaseTypes
{
    public static class CharExtension
    {
        public static bool IsDigit(this char @char) => @char >= '0' && @char <= '9';

        public static bool IsUpper(this char @char) => @char >= 'A' && @char <= 'Z';

        public static bool IsLower(this char @char) => @char >= 'a' && @char <= 'z';

        public static bool IsAccent(this char @char) => StringHelper.BuildAllAccents().Contains(@char);
    }
}
