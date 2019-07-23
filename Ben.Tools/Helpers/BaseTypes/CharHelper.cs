using System;
using System.Collections.Generic;
using System.Linq;

namespace BenTools.Helpers.BaseTypes
{
    public static class CharHelper
    {
        private static Random Random = new Random();

        #region Random Character Of Type
        public static char RandomDigit() => StringHelper.AllDigits[Random.Next(StringHelper.AllDigits.Length)];

        public static char RandomLowerLetterWithoutAccent() => StringHelper.AllLowerLettersWithoutAccents[Random.Next(StringHelper.AllLowerLettersWithoutAccents.Length)];

        public static char RandomUpperLetterWithoutAccent() => StringHelper.AllUpperLettersWithoutAccents[Random.Next(StringHelper.AllUpperLettersWithoutAccents.Length)];

        public static char RandomLetterWithoutAccent() => StringHelper.AllLettersWithoutAccents[Random.Next(StringHelper.AllLettersWithoutAccents.Length)];

        public static char RandomLowerLetterWithAccent() => StringHelper.AllLowerLettersWithAccents[Random.Next(StringHelper.AllLowerLettersWithAccents.Length)];

        public static char RandomUpperLetterWithAccent() => StringHelper.AllUpperLettersWithAccents[Random.Next(StringHelper.AllUpperLettersWithAccents.Length)];

        public static char RandomLetterWitAccent() => StringHelper.AllLettersWithAccents[Random.Next(StringHelper.AllLettersWithAccents.Length)];

        public static char RandomLowerLetter() => StringHelper.AllLowersLetters[Random.Next(StringHelper.AllLowersLetters.Length)];

        public static char RandomUpperLetter() => StringHelper.AllUppersLetters[Random.Next(StringHelper.AllUppersLetters.Length)];

        public static char RandomLetter() => StringHelper.AllLetters[Random.Next(StringHelper.AllLetters.Length)];
        #endregion

        /// <summary>
        /// Le ToList() à la fin est important car autrement une nouvelle génération d'éléments se fera à chaque fois que vous allez parcourir votre séquence.
        /// </summary>
        public static List<char> Generate(int count = 10) => Enumerable.Range(0, count).Select(_ => RandomLetter()).ToList();

    }
}
