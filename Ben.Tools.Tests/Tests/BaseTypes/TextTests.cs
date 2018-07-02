using System.Linq;
using BenTools.Extensions.BaseTypes;
using BenTools.Helpers.BaseTypes;
using NUnit.Framework;

namespace BenTools.Tests.Tests.BaseTypes
{
    [TestFixture]
    public class TextTests
    {
        [Test]
        public void RandomString()
        {
            // String Helper :
            var BuildAllDigits = new string(StringHelper.BuildAllDigits().ToArray());
            var BuildAllLowerLettersWithoutAc = new string(StringHelper.BuildAllLowerLettersWithoutAccents().ToArray());
            var BuildAllUpperLettersWithoutAc = new string(StringHelper.BuildAllUpperLettersWithoutAccents().ToArray());
            var BuildAllLettersWithoutAccents = new string(StringHelper.BuildAllLettersWithoutAccents().ToArray());
            var BuildAllLowerLettersWithAccen = new string(StringHelper.BuildAllLowerLettersWithAccents().ToArray());
            var BuildAllUpperLettersWithAccen = new string(StringHelper.BuildAllUpperLettersWithAccents().ToArray());
            var BuildAllLettersWithAccents = new string(StringHelper.BuildAllLettersWithAccents().ToArray());
            var BuildAllLowersLetters = new string(StringHelper.BuildAllLowersLetters().ToArray());
            var BuildAllUppersLetters = new string(StringHelper.BuildAllUppersLetters().ToArray());
            var BuildAllLetters = new string(StringHelper.BuildAllLetters().ToArray());

            var digitsAlphaLower = StringHelper.BuildRandomString(50, true, true);
            var digitsAlphaUpper = StringHelper.BuildRandomString(50, true, false, true);
            var digitsAlphaLowerAccents = StringHelper.BuildRandomString(50, true, false, false, true);
            var digitsAlphaUpperAccents = StringHelper.BuildRandomString(50, true, false, false, false, true);


            // String Extension :
            var AreDigits = BuildAllDigits.AreDigits();
            var AreLowerLettersWithoutAc = BuildAllLowerLettersWithoutAc.AreLowerLettersWithoutAccent();
            var AreUpperLettersWithoutAc = BuildAllUpperLettersWithoutAc.AreUpperLettersWithoutAccent();
            var AreLettersWithoutAccents = BuildAllLettersWithoutAccents.AreLettersWithoutAccent();
            var AreLowerLettersWithAccen = BuildAllLowerLettersWithAccen.AreLowerLettersWithAccent();
            var AreUpperLettersWithAccen = BuildAllUpperLettersWithAccen.AreUpperLettersWithAccent();
            var AreLettersWithAccents = BuildAllLettersWithAccents.AreLettersWithAccent();
            var AreLowersLetters = BuildAllLowersLetters.AreLowerLetters();
            var AreUppersLetters = BuildAllUppersLetters.AreUpperLetters();
            var AreLetters = BuildAllLetters.AreLetters();

            var CountCharactersTypes = BuildAllLetters.CountCharactersTypes();

            // Char Helper :
            var RandomDigit = CharHelper.RandomDigit();
            var RandomLowerLetterWithoutAccent = CharHelper.RandomLowerLetterWithoutAccent();
            var RandomUpperLetterWithoutAccent = CharHelper.RandomUpperLetterWithoutAccent();
            var RandomLetterWithoutAccent = CharHelper.RandomLetterWithoutAccent();
            var RandomLowerLetterWithAccent = CharHelper.RandomLowerLetterWithAccent();
            var RandomUpperLetterWithAccent = CharHelper.RandomUpperLetterWithAccent();
            var RandomLetterWitAccent = CharHelper.RandomLetterWitAccent();
            var RandomLowerLetter = CharHelper.RandomLowerLetter();
            var RandomUpperLetter = CharHelper.RandomUpperLetter();
            var RandomLetter = CharHelper.RandomLetter();

            // Char Extension :
            var IsDigit = CharExtension.IsDigit(RandomDigit);
            var IsLowerLetterWithoutAccent = CharExtension.IsLowerLetterWithoutAccent(RandomLowerLetterWithoutAccent);
            var IsUpperLetterWithoutAccent = CharExtension.IsUpperLetterWithoutAccent(RandomUpperLetterWithoutAccent);
            var IsLetterWithoutAccent = CharExtension.IsLetterWithoutAccent(RandomLetterWithoutAccent);
            var IsLowerLetterWithAccent = CharExtension.IsLowerLetterWithAccent(RandomLowerLetterWithAccent);
            var IsUpperLetterWithAccent = CharExtension.IsUpperLetterWithAccent(RandomUpperLetterWithAccent);
            var IsLetterWithAccent = CharExtension.IsLetterWithAccent(RandomLetterWitAccent);
            var IsLowerLetter = CharExtension.IsLowerLetter(RandomLowerLetter);
            var IsUpperLetter = CharExtension.IsUpperLetter(RandomUpperLetter);
            var IsLetter = CharExtension.IsLetter(RandomLetter);
        }
    }
}