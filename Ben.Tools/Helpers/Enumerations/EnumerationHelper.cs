using System;
using System.Collections.Generic;
using System.Linq;
using BenTools.Extensions.Sequences;

namespace BenTools.Helpers.Enumerations
{
    public static class EnumerationHelper
    {
        public static int Count<EnumerationType>() where EnumerationType : struct, IConvertible => 
            Enum.GetValues(typeof(EnumerationType)).Length;
        
        public static void Foreach<EnumerationType>(Action<EnumerationType> callback)
            where EnumerationType : struct, IConvertible => 
            ToEnumerations<EnumerationType>().Foreach(callback);

        public static IEnumerable<EnumerationType> ToEnumerations<EnumerationType>()
            where EnumerationType : struct, IConvertible => 
            Enum.GetValues(typeof(EnumerationType)).Cast<EnumerationType>();

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

        public static IEnumerable<string> ToStrings<EnumerationType>()
            where EnumerationType : struct, IConvertible => 
            Enum.GetNames(typeof(EnumerationType));

        public static string ToString<EnumerationType>(EnumerationType enumeration)
            where EnumerationType : struct, IConvertible => 
            enumeration.ToString();

        public static string ToString<EnumerationType>(int enumerationIndex)
            where EnumerationType : struct, IConvertible => 
            ToString(ToEnumeration<EnumerationType>(enumerationIndex));

        public static IEnumerable<int> ToIntegers<EnumerationType>()
            where EnumerationType : struct, IConvertible => 
            Enum.GetValues(typeof(EnumerationType)).Cast<int>();

        public static int ToInteger<EnumerationType>(EnumerationType enumeration)
            where EnumerationType : struct, IConvertible => 
            (int) (object) enumeration;

        public static int ToInteger<EnumerationType>(string enumerationText)
            where EnumerationType : struct, IConvertible => 
            ToInteger(ToEnumeration<EnumerationType>(enumerationText));
    }
}
