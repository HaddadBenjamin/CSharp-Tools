using System;
using System.Linq;

namespace Ben.Tools.Helpers.BaseTypes
{
    public static class StringHelper
    {
        public static string GenerateRandomString(int length = 6)
        {
            var random = new Random();

            return new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz0123456789", length)
                .Select(chars => chars[random.Next(chars.Length)])
                .ToArray());
        }
    }
}
