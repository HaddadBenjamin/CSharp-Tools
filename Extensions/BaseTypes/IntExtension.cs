namespace Ben.Tools.Extensions.BaseTypes
{
    public static class IntExtension
    {
        public static bool Any(this int integer) => integer > 0;
        
        public static bool IsBetween(this int number, int minimum, int maximum) => number >= minimum && number <= maximum;
    }
}
