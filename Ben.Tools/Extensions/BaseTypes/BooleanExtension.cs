using System.Linq;

namespace BenTools.Extensions.BaseTypes
{
    public static class BooleanExtension
    {
        public static bool Inverse(this bool boolean) => boolean ^ true;
        
        public static bool AtLeastOneTrue(params bool[] booleans) => booleans.Any(element => element);

        public static bool AllTrue(params bool[] booleans) => booleans.All(element => element);
    }
}
