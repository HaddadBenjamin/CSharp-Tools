using System;
using System.Collections.Generic;
using System.Linq;

namespace Ben.Tools.Helpers.Enumerations
{
    public static class EnumerationHelper
    {
         
        public static int Count<EnumerationType>() where EnumerationType : struct, IConvertible => 
            Enum.GetValues(typeof(EnumerationType)).Length;

        public static IEnumerable<EnumerationType> ToEnumerations<EnumerationType>()
            where EnumerationType : struct, IConvertible => 
            Enum.GetValues(typeof(EnumerationType)).Cast<EnumerationType>();

        public static EnumerationType ToEnumeration<EnumerationType>(IEnumerable<string> enumerationTexts)
            where EnumerationType : struct, IConvertible
        {
            var enumerationValue = 0;

            foreach (var enumerationText in enumerationTexts)
            {
                var enumeration = ToEnumeration<EnumerationType>(enumerationText);

                enumerationValue |= ToInteger(enumeration);
            }

            return ToEnumeration<EnumerationType>(enumerationValue);
        }

        public static EnumerationType ToEnumeration<EnumerationType>(string enumerationText)
            where EnumerationType : struct, IConvertible => 
            (EnumerationType) Enum.Parse(typeof(EnumerationType), enumerationText, true);

        public static EnumerationType ToEnumeration<EnumerationType>(int enumerationValue)
            where EnumerationType : struct, IConvertible => 
            (EnumerationType) (object) enumerationValue;

        public static DestinationEnumeration ToEnumerationByIndex<SourceEnumeration, DestinationEnumeration>(
            SourceEnumeration sourceEnumeration)
            where SourceEnumeration : struct, IConvertible
            where DestinationEnumeration : struct, IConvertible => 
            (DestinationEnumeration) (object) sourceEnumeration;

        public static DestinationEnumeration ToEnumerationByText<SourceEnumeration, DestinationEnumeration>(
            SourceEnumeration sourceEnumeration)
            where SourceEnumeration : struct, IConvertible
            where DestinationEnumeration : struct, IConvertible => 
            (DestinationEnumeration) Enum.Parse(typeof(DestinationEnumeration), sourceEnumeration.ToString());

        public static IEnumerable<string> ToStrings<EnumerationType>(EnumerationType enumerationFlags)
            where EnumerationType : struct, IConvertible
        {
            var enumerationFlagInteger = ToInteger(enumerationFlags);

            return ToEnumerations<EnumerationType>()
                .Where(enumeration => (enumerationFlagInteger & ToInteger(enumeration)) != 0)
                .Select(enumeration => enumeration.ToString());
        }

        public static IEnumerable<string> ToStrings<EnumerationType>()
            where EnumerationType : struct, IConvertible => 
            Enum.GetNames(typeof(EnumerationType));

        public static string ToString<EnumerationType>(EnumerationType enumeration)
            where EnumerationType : struct, IConvertible => 
            enumeration.ToString();

        public static string ToString<EnumerationType>(int enumerationIndex)
            where EnumerationType : struct, IConvertible
        {
            var enumeration = ToEnumeration<EnumerationType>(enumerationIndex);

            return ToString(enumeration);
        }

        public static IEnumerable<int> ToIntegers<EnumerationType>()
            where EnumerationType : struct, IConvertible => 
            Enum.GetValues(typeof(EnumerationType)).Cast<int>();

        public static int ToInteger<EnumerationType>(EnumerationType enumeration)
            where EnumerationType : struct, IConvertible => 
            (int) (object) enumeration;

        public static int ToInteger<EnumerationType>(string enumerationText)
            where EnumerationType : struct, IConvertible
        {
            var enumeration = ToEnumeration<EnumerationType>(enumerationText);

            return ToInteger(enumeration);
        }
        

        // 
        //public static int Count<EnumerationType>() where EnumerationType : struct, IConvertible
        //{
        //    return Enum.GetValues(typeof(EnumerationType)).Length;
        //}

        //public static IEnumerable<EnumerationType> ToEnumerations<EnumerationType>() where EnumerationType : struct, IConvertible
        //{
        //    return Enum.GetValues(typeof(EnumerationType)).Cast<EnumerationType>();
        //}

        //public static EnumerationType ToEnumeration<EnumerationType>(IEnumerable<string> enumerationTexts)
        //    where EnumerationType : struct, IConvertible
        //{
        //    int enumerationValue = 0;

        //    foreach (var enumerationText in enumerationTexts)
        //    {
        //        var enumeration = ToEnumeration<EnumerationType>(enumerationText);

        //        enumerationValue |= ToInteger(enumeration);
        //    }

        //    return ToEnumeration<EnumerationType>(enumerationValue);
        //}

        //public static EnumerationType ToEnumeration<EnumerationType>(string enumerationText)
        //    where EnumerationType : struct, IConvertible
        //{
        //    return (EnumerationType)Enum.Parse(typeof(EnumerationType), enumerationText, true);
        //}

        //public static EnumerationType ToEnumeration<EnumerationType>(int enumerationValue)
        //    where EnumerationType : struct, IConvertible
        //{
        //    return (EnumerationType)(object)enumerationValue;
        //}

        //public static int ToInteger<EnumerationType>(EnumerationType enumeration)
        //    where EnumerationType : struct, IConvertible
        //{
        //    return (int)(object)enumeration;
        //}

        //public static int ToInteger<EnumerationType>(string enumerationText)
        //    where EnumerationType : struct, IConvertible
        //{
        //    var enumeration = ToEnumeration<EnumerationType>(enumerationText);

        //    return ToInteger(enumeration);
        //}

        //public static string[] ToStrings<EnumerationType>()
        //    where EnumerationType : struct, IConvertible
        //{
        //    return Enum.GetNames(typeof(EnumerationType));
        //}

        //public static string ToString<EnumerationType>(EnumerationType enumeration)
        //    where EnumerationType : struct, IConvertible
        //{
        //    return enumeration.ToString();
        //}

        //public static string ToString<EnumerationType>(int enumerationIndex)
        //    where EnumerationType : struct, IConvertible
        //{
        //    EnumerationType enumeration = ToEnumeration<EnumerationType>(enumerationIndex);

        //    return ToString(enumerationIndex);
        //}
        //
    }
}