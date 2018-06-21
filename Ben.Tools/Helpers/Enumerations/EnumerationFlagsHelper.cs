using System;
using System.Collections.Generic;
using System.Linq;

namespace BenTools.Helpers.Enumerations
{
    /// <summary>
    /// All methods have been tested.
    /// </summary>
    public static class EnumerationFlagsHelper
    {
        #region ToEnumeration(s)
        public static EnumerationType ToEnumeration<EnumerationType>(int enumerationFlagValues)
           where EnumerationType : struct, IConvertible =>
           EnumerationHelper.ToEnumeration<EnumerationType>(enumerationFlagValues);

        public static EnumerationType ToEnumeration<EnumerationType>(IEnumerable<int> enumerationFlagValues)
            where EnumerationType : struct, IConvertible =>
            EnumerationHelper.ToEnumeration<EnumerationType>(ToInteger(enumerationFlagValues));

        public static EnumerationType ToEnumeration<EnumerationType>(IEnumerable<EnumerationType> enumerationFlags)
            where EnumerationType : struct, IConvertible =>
            ToEnumeration<EnumerationType>(enumerationFlags.Select(EnumerationHelper.ToInteger));

        public static EnumerationType ToEnumeration<EnumerationType>(IEnumerable<string> enumerationFlagTexts)
            where EnumerationType : struct, IConvertible =>
            ToEnumeration<EnumerationType>(enumerationFlagTexts.Select(EnumerationHelper.ToInteger<EnumerationType>));

        public static IEnumerable<EnumerationType> ToEnumerations<EnumerationType>(int enumerationFlagsValue)
            where EnumerationType : struct, IConvertible =>
            EnumerationHelper.ToEnumerations<EnumerationType>()
                .Where(enumeration => DoesFlagIsEnable(enumerationFlagsValue, EnumerationHelper.ToInteger(enumeration)));

        public static IEnumerable<EnumerationType> ToEnumerations<EnumerationType>(EnumerationType enumerationFlags)
            where EnumerationType : struct, IConvertible =>
            ToEnumerations<EnumerationType>(EnumerationHelper.ToInteger(enumerationFlags));

        public static IEnumerable<EnumerationType> ToEnumerations<EnumerationType>(IEnumerable<int> enumerationFlagValues)
            where EnumerationType : struct, IConvertible =>
            enumerationFlagValues.SelectMany(ToEnumerations<EnumerationType>)
                                 .Distinct();

        public static IEnumerable<EnumerationType> ToEnumerations<EnumerationType>(IEnumerable<string> enumerationFlagTexts)
            where EnumerationType : struct, IConvertible =>
            ToEnumerations<EnumerationType>(enumerationFlagTexts.Select(EnumerationHelper.ToInteger<EnumerationType>));
        #endregion

        #region ToInteger(s)
        public static int ToInt(IEnumerable<int> enumerationFlagValues) =>
            AddFlags(0, enumerationFlagValues);

        public static int ToInteger<EnumerationType>(EnumerationType enumerationFlagValues)
            where EnumerationType : struct, IConvertible =>
            EnumerationHelper.ToInteger(enumerationFlagValues);

        public static int ToInteger<EnumerationType>(IEnumerable<EnumerationType> enumerationFlagValues)
            where EnumerationType : struct, IConvertible =>
            ToInt(enumerationFlagValues.Select(EnumerationHelper.ToInteger));

        public static int ToInteger<EnumerationType>(IEnumerable<string> enumerationFlagTexts)
            where EnumerationType : struct, IConvertible =>
            ToInt(enumerationFlagTexts.Select(EnumerationHelper.ToInteger<EnumerationType>));

        public static IEnumerable<int> ToInts<EnumerationType>(EnumerationType enumerationFlags)
            where EnumerationType : struct, IConvertible =>
            ToEnumerations(enumerationFlags).Select(ToInteger);

        public static IEnumerable<int> ToIntegers<EnumerationType>(IEnumerable<EnumerationType> enumerationFlags)
            where EnumerationType : struct, IConvertible =>
            enumerationFlags.Select(ToInteger);

        public static IEnumerable<int> ToIntegers<EnumerationType>(IEnumerable<string> enumerationFlagsTexts)
            where EnumerationType : struct, IConvertible =>
            ToIntegers(enumerationFlagsTexts.Select(EnumerationHelper.ToInteger<EnumerationType>));
        #endregion

        #region ToStrings
        public static IEnumerable<string> ToStrings<EnumerationType>(IEnumerable<int> enumerationFlagValues)
            where EnumerationType : struct, IConvertible =>
            ToStrings(ToEnumeration<EnumerationType>(enumerationFlagValues));

        public static IEnumerable<string> ToStrings<EnumerationType>(EnumerationType enumerationFlags)
            where EnumerationType : struct, IConvertible =>
            ToEnumerations(enumerationFlags).Select(enumerationFlag => enumerationFlag.ToString());

        public static IEnumerable<string> ToStrings<EnumerationType>(int enumerationFlagsValue)
            where EnumerationType : struct, IConvertible =>
            ToStrings(ToEnumeration<EnumerationType>(enumerationFlagsValue));

        public static IEnumerable<string> ToStrings<EnumerationType>(IEnumerable<EnumerationType> enumerationFlags)
            where EnumerationType : struct, IConvertible =>
            enumerationFlags.Select(enumerationFlag => enumerationFlag.ToString());
        #endregion

        #region DoesFlag[s]{is|Are}Enable
        public static bool DoesFlagIsEnable(int enumerationFlagValues, int flagToTestValue) => (enumerationFlagValues & flagToTestValue) != 0;

        public static bool DoesFlagIsEnable<EnumerationType>(EnumerationType enumerationFlags, EnumerationType flagToTest)
            where EnumerationType : struct, IConvertible =>
            DoesFlagIsEnable(ToInteger(enumerationFlags), ToInteger(flagToTest));

        public static bool DoesFlagIsEnable<EnumerationType>(EnumerationType enumerationFlags, string flagToTestText)
            where EnumerationType : struct, IConvertible =>
            DoesFlagIsEnable(ToInteger(enumerationFlags), EnumerationHelper.ToInteger<EnumerationType>(flagToTestText));

        public static bool DoesFlagsAreEnable<EnumerationType>(EnumerationType enumerationFlags, IEnumerable<int> flagsToTestsValues)
            where EnumerationType : struct, IConvertible =>
            flagsToTestsValues.All(flagToTestValue => DoesFlagIsEnable(ToInteger(enumerationFlags), flagToTestValue));

        public static bool DoesFlagsAreEnable<EnumerationType>(EnumerationType enumerationFlags, IEnumerable<EnumerationType> flagsToTests)
            where EnumerationType : struct, IConvertible =>
            flagsToTests.All(flagToTest => DoesFlagIsEnable(enumerationFlags, flagToTest));

        public static bool DoesFlagsAreEnable<EnumerationType>(EnumerationType enumerationFlags, IEnumerable<string> flagsToTestsTexts)
            where EnumerationType : struct, IConvertible =>
            DoesFlagsAreEnable(enumerationFlags, ToEnumerations<EnumerationType>(flagsToTestsTexts));
        #endregion

        #region AddFlag(s)
        public static int AddFlag(int flagsValues, int flagToAddValue) => flagsValues | flagToAddValue;

        public static EnumerationType AddFlag<EnumerationType>(EnumerationType enumerationFlags, EnumerationType flagToAdd)
            where EnumerationType : struct, IConvertible =>
            ToEnumeration<EnumerationType>(AddFlag(ToInteger(enumerationFlags), ToInteger(flagToAdd)));

        public static EnumerationType AddFlag<EnumerationType>(EnumerationType enumerationFlags, string flagToAddText)
            where EnumerationType : struct, IConvertible =>
            ToEnumeration<EnumerationType>(AddFlag(ToInteger(enumerationFlags), EnumerationHelper.ToInteger<EnumerationType>(flagToAddText)));

        public static int AddFlags(int flagsValues, IEnumerable<int> flagsToAddValues) => flagsToAddValues.Aggregate((flagsSum, flagToAdd) => flagsSum | flagsValues | flagToAdd);

        public static EnumerationType AddFlags<EnumerationType>(EnumerationType enumerationFlags, IEnumerable<EnumerationType> flagsToAdd)
            where EnumerationType : struct, IConvertible =>
            ToEnumeration<EnumerationType>(AddFlag(ToInteger(enumerationFlags), ToInteger(flagsToAdd)));

        public static EnumerationType AddFlags<EnumerationType>(EnumerationType enumerationFlags, IEnumerable<string> flagsToAddTexts)
            where EnumerationType : struct, IConvertible =>
            ToEnumeration<EnumerationType>(AddFlag(ToInteger(enumerationFlags), ToInteger<EnumerationType>(flagsToAddTexts)));
        #endregion

        #region RemoveFlag(s)
        public static int RemoveFlag(int flagsValues, int flagToRemoveValue) => flagsValues & (~flagToRemoveValue);

        public static EnumerationType RemoveFlag<EnumerationType>(EnumerationType enumerationFlags, EnumerationType flagToRemove)
            where EnumerationType : struct, IConvertible =>
            ToEnumeration<EnumerationType>(RemoveFlag(ToInteger(enumerationFlags), ToInteger(flagToRemove)));

        public static EnumerationType RemoveFlag<EnumerationType>(EnumerationType enumerationFlags, string flagToRemoveText)
            where EnumerationType : struct, IConvertible =>
            ToEnumeration<EnumerationType>(RemoveFlag(ToInteger(enumerationFlags), EnumerationHelper.ToInteger<EnumerationType>(flagToRemoveText)));

        public static int RemoveFlags(int flagsValues, IEnumerable<int> flagsToRemoveValues) => RemoveFlag(flagsValues, ToInteger(flagsToRemoveValues));

        public static EnumerationType RemoveFlags<EnumerationType>(EnumerationType enumerationFlags, IEnumerable<EnumerationType> flagsToRemove)
            where EnumerationType : struct, IConvertible =>
            ToEnumeration<EnumerationType>(RemoveFlag(ToInteger(enumerationFlags), ToInteger(flagsToRemove)));

        public static EnumerationType RemoveFlags<EnumerationType>(EnumerationType enumerationFlags, IEnumerable<string> flagsToRemoveTexts)
            where EnumerationType : struct, IConvertible =>
            ToEnumeration<EnumerationType>(RemoveFlag(ToInteger(enumerationFlags), ToInteger<EnumerationType>(flagsToRemoveTexts)));
        #endregion

        #region CombineFlag(s)
        public static int CombineFlag(IEnumerable<int> flagsValues, int flagToAddValue) =>
            AddFlag(ToInteger(flagsValues), flagToAddValue);

        public static EnumerationType CombineFlag<EnumerationType>(IEnumerable<EnumerationType> enumerationFlags, EnumerationType flagToAdd)
            where EnumerationType : struct, IConvertible =>
            ToEnumeration<EnumerationType>(AddFlag(ToInteger(enumerationFlags), ToInteger(flagToAdd)));

        public static EnumerationType CombineFlag<EnumerationType>(IEnumerable<EnumerationType> enumerationFlags, string flagToAddText)
            where EnumerationType : struct, IConvertible =>
            ToEnumeration<EnumerationType>(AddFlag(ToInteger(enumerationFlags), EnumerationHelper.ToInteger<EnumerationType>(flagToAddText)));

        public static int CombineFlags(IEnumerable<int> flagsValues, IEnumerable<int> flagsToAddValues) =>
            AddFlags(ToInteger(flagsValues), flagsToAddValues);

        public static EnumerationType CombineFlags<EnumerationType>(IEnumerable<EnumerationType> enumerationFlags, IEnumerable<EnumerationType> flagsToAdd)
            where EnumerationType : struct, IConvertible =>
            ToEnumeration<EnumerationType>(AddFlag(ToInteger(enumerationFlags), ToInteger(flagsToAdd)));

        public static EnumerationType CombineFlags<EnumerationType>(IEnumerable<EnumerationType> enumerationFlags, IEnumerable<string> flagsToAddTexts)
            where EnumerationType : struct, IConvertible =>
            ToEnumeration<EnumerationType>(AddFlag(ToInteger(enumerationFlags), ToInteger<EnumerationType>(flagsToAddTexts)));
        #endregion
    }
}
