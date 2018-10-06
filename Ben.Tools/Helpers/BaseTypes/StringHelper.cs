using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenTools.Helpers.BaseTypes
{
    public static class StringHelper
    {
        private static Random Random = new Random();

        #region Conversion
        public static string ToString(IEnumerable<bool> booleans) =>
            new string(booleans.Select(boolean => boolean ? '1' : '0')
                               .ToArray());

        public static string ToString(IEnumerable<int> integers) => string.Join(string.Empty, integers.Select(integer => integer.ToString()));

        public static string ToString(IEnumerable<char> characters) => new string(characters.ToArray());
        #endregion

        #region All Characters Types
    public static readonly string AllDigits = "0123456789";

        public static readonly string AllLowerLettersWithoutAccents = "abcdefghijklmnopqrstuvwxyz";

        public static readonly string AllUpperLettersWithoutAccents = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static readonly string AllLettersWithoutAccents = AllLowerLettersWithoutAccents + AllUpperLettersWithoutAccents;

        public static readonly string AllLowerLettersWithAccents = "àáâãäåòóôõöøèéêëìíîïùúûüÿñç";

        public static readonly string AllUpperLettersWithAccents = "ÀÁÂÃÄÅÒÓÔÕÖØÈÉÊËÌÍÎÏÙÚÛÜÑÇ"; // manque le Y

        public static readonly string AllLettersWithAccents = AllLowerLettersWithAccents + AllUpperLettersWithAccents;

        public static readonly string AllLowersLetters = AllLowerLettersWithoutAccents + AllLowerLettersWithAccents;

        public static readonly string AllUppersLetters = AllUpperLettersWithoutAccents + AllUpperLettersWithAccents;

        public static readonly string AllLetters = AllLettersWithoutAccents + AllLettersWithAccents;
        #endregion

        #region Build All Characters Types & String
        public static IEnumerable<char> BuildAllDigits() => AllDigits.ToCharArray();

        public static IEnumerable<char> BuildAllLowerLettersWithoutAccents() => AllLowerLettersWithoutAccents.ToCharArray();

        public static IEnumerable<char> BuildAllUpperLettersWithoutAccents() => AllUpperLettersWithoutAccents.ToCharArray();

        public static IEnumerable<char> BuildAllLettersWithoutAccents() => AllLettersWithoutAccents.ToCharArray();

        public static IEnumerable<char> BuildAllLowerLettersWithAccents() => AllLowerLettersWithAccents.ToCharArray();

        public static IEnumerable<char> BuildAllUpperLettersWithAccents() => AllUpperLettersWithAccents.ToCharArray();

        public static IEnumerable<char> BuildAllLettersWithAccents() => AllLettersWithAccents.ToCharArray();

        public static IEnumerable<char> BuildAllLowersLetters() => AllLowersLetters.ToCharArray();

        public static IEnumerable<char> BuildAllUppersLetters() => AllUppersLetters.ToCharArray();

        public static IEnumerable<char> BuildAllLetters() => AllLetters.ToCharArray();

        public static string BuilldAllCharactersOfTypes(
            bool withDigits = false, 
            bool withLowerLettersWithoutAccent = false,
            bool withUpperLettersWithoutAccent = false,
            bool withLowerLettersWithAccent = false,
            bool withUpperLettersWithAccent = false)
        {
            var allCharacters = new StringBuilder();

            if (withDigits)                     allCharacters.Append(AllDigits);
            if (withLowerLettersWithoutAccent)  allCharacters.Append(AllLowerLettersWithoutAccents);
            if (withUpperLettersWithoutAccent)  allCharacters.Append(AllUpperLettersWithoutAccents);
            if (withLowerLettersWithAccent)     allCharacters.Append(AllLowerLettersWithAccents);
            if (withUpperLettersWithAccent)     allCharacters.Append(AllUpperLettersWithAccents);

            return allCharacters.ToString();
        }

        /// <summary>
        /// Permet de générer une string aléatoires d'une taille définit tout en utilisant les types de charactères définits : chiffres, lettres minuscules ou majuscules avec et sans accents.
        /// </summary>
        public static string BuildRandomString(
            int length = 6,
            bool withDigits = false,
            bool withLowerLettersWithoutAccent = false,
            bool withUpperLettersWithoutAccent = false,
            bool withLowerLettersWithAccent = false,
            bool withUpperLettersWithAccent = false) =>
            new string(Enumerable.Repeat(BuilldAllCharactersOfTypes(withDigits, withLowerLettersWithoutAccent, withUpperLettersWithoutAccent, withLowerLettersWithAccent, withUpperLettersWithAccent), length)
                .Select(chars => chars[Random.Next(chars.Length)])
                .ToArray());
        #endregion
    }
}
