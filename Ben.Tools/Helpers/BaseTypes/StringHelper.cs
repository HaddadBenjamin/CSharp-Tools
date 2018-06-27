using System;
using System.Collections.Generic;
using System.Linq;
using BenTools.Extensions.BaseTypes;

namespace BenTools.Helpers.BaseTypes
{
    public static class StringHelper
    {
        public static IEnumerable<char> BuildAllDigits() => "0123456789".ToCharArray();

        public static IEnumerable<char> BuildAllLowers() => "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public static IEnumerable<char> BuildAllUppers() => "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        public static IEnumerable<char> BuildAllAccents() => "ÀÁÂÃÄÅàáâãäåÒÓÔÕÖØòóôõöøÈÉÊËèéêëÌÍÎÏìíîïÙÚÛÜùúûüÿÑñÇç".ToCharArray();

        public static string BuildAllCharactersString(bool containsNumbers = false, bool containsLowers = true, bool containsUppers = false, bool containsAccents = false)
        {
            var allCharacters = string.Empty;

            if (containsNumbers) allCharacters += BuildAllDigits();
            if (containsLowers) allCharacters += BuildAllLowers();
            if (containsUppers) allCharacters += BuildAllUppers();

            if (containsAccents)
            {
                var allAccents = BuildAllAccents();

                if (containsLowers) allCharacters += allAccents.Where(@char => @char.IsLower());
                if (containsUppers) allCharacters += allAccents.Where(@char => @char.IsUpper());
            }

            return allCharacters;
        }

        public static string GenerateRandomString(int length = 6, bool containsNumbers = false, bool containsLowers = true, bool containsUppers = false, bool containsAccents = false) =>
            new string(Enumerable.Repeat(BuildAllCharactersString(containsNumbers, containsLowers, containsUppers, containsAccents), length)
                .Select(chars => chars[new Random().Next(chars.Length)])
                .ToArray());
    }
}
