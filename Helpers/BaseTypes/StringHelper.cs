using System;
using System.Linq;

namespace Ben.Tools.Helpers.BaseTypes
{
    public static class StringHelper
    {
        public static string GenerateRandomString(int length = 6)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length)
                .Select(@char => @char[random.Next(@char.Length)])
                .ToArray());
        }
    }
}
