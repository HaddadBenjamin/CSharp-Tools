namespace Ben.Tools.Extensions.BaseTypes
{
    public static class BooleanExtension
    {
        public static bool Inverse(this bool boolean) => boolean ^ true;
    }
}