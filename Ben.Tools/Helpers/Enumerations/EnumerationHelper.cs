using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using BenTools.Extensions.Sequences;
using BenTools.Utilities.Attributes;

namespace BenTools.Helpers.Enumerations
{
    public static class EnumerationHelper
    {
        public static int Count<EnumerationType>() where EnumerationType : struct, IConvertible => 
            Enum.GetValues(typeof(EnumerationType)).Length;
        
        public static void Foreach<EnumerationType>(Action<EnumerationType> callback)
            where EnumerationType : struct, IConvertible => 
            ToEnumerations<EnumerationType>().Foreach(callback);
        
        public static bool In<EnumerationType>(EnumerationType enumerationSource, params EnumerationType[] enumerations)  
            where EnumerationType : struct, IConvertible =>
            enumerations.Contains(enumerationSource);

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

        /// <summary>
        /// Return the enumeration element associated to the enumeration display value attribute.
        /// </summary>
        public static TEnumeration ConvertDisplayValueToEnumeration<TEnumeration>(string enumerationDisplayValue)
            where TEnumeration : IComparable, IFormattable, IConvertible
        {
            var enumerations = Enum
                .GetValues(typeof(TEnumeration))
                .Cast<TEnumeration>()
                .Where(enumeration => typeof(TEnumeration).GetMember(enumeration.ToString(CultureInfo.InvariantCulture))
                                          .FirstOrDefault()
                                          ?.GetCustomAttributes<EnumerationDisplayValueAttribute>(false)
                                          ?.FirstOrDefault()
                                          ?.DisplayValue
                                          .Equals(enumerationDisplayValue, StringComparison.InvariantCulture) ?? false)
                .ToList();

            return enumerations.Any() ?
                enumerations.First() : 
                throw new Exception($"{enumerationDisplayValue} is not a valid {typeof(TEnumeration).Name}");
        }

    }
}
