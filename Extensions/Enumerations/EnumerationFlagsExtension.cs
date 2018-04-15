using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Ben.Tools.Extensions.Enumerations
{
    internal static class EnumerationFlagsExtension
    {
        private static void CheckIsEnum<TEnumerationType>(bool checkFlagsToo)
        {
            if (!typeof(TEnumerationType).IsEnum)
                throw new ArgumentException(string.Format(
                    "Type '{0}' is not an enum",
                    typeof(TEnumerationType).FullName));

            if (checkFlagsToo &&
                !Attribute.IsDefined(typeof(TEnumerationType), typeof(FlagsAttribute)))
                throw new ArgumentException(string.Format(
                    "Type '{0}' doesn't have the 'Flags' attribute",
                    typeof(TEnumerationType).FullName));
        }

        public static bool IsFlagSet<TEnumerationType>(
            this TEnumerationType enumeration,
            TEnumerationType enumerationFlag)
            where TEnumerationType : struct
        {
            CheckIsEnum<TEnumerationType>(true);

            var enumerationValue = Convert.ToInt64(enumeration);
            var enumerationFlagValue = Convert.ToInt64(enumerationFlag);

            return (enumerationValue & enumerationFlagValue) != 0;
        }

        public static IEnumerable<TEnumerationType> GetFlags<TEnumerationType>(
            this TEnumerationType enumeration)
            where TEnumerationType : struct
        {
            CheckIsEnum<TEnumerationType>(true);

            foreach (var enumerationFlag in Enum.GetValues(
                    typeof(TEnumerationType))
                .Cast<TEnumerationType>())
                if (enumeration.IsFlagSet(enumerationFlag))
                    yield return enumerationFlag;
        }

        public static TEnumerationType SetFlags<TEnumerationType>(
            this TEnumerationType enumeration,
            TEnumerationType enumerationFlags,
            bool enableFlag)
            where TEnumerationType : struct
        {
            CheckIsEnum<TEnumerationType>(true);

            var enumerationValue = Convert.ToInt64(enumeration);
            var enumerationFlagValue = Convert.ToInt64(enumerationFlags);

            enumerationValue = enableFlag ?
                enumerationValue |  enumerationFlagValue :
                enumerationValue &~ enumerationFlagValue;

            return (TEnumerationType) Enum.ToObject(typeof(TEnumerationType), enumerationValue);
        }

        public static TEnumerationType SetFlags<TEnumerationType>(
            this TEnumerationType enumeration,
            TEnumerationType enumerationFlags)
            where TEnumerationType : struct => 
            enumeration.SetFlags(enumerationFlags, true);

        public static TEnumerationType ClearFlags<TEnumerationType>(
            this TEnumerationType enumeration,
            TEnumerationType enumerationFlags)
            where TEnumerationType : struct => 
            enumeration.SetFlags(enumerationFlags, false);

        public static TEnumerationType CombineFlags<TEnumerationType>(
            this IEnumerable<TEnumerationType> enumerationFlags)
            where TEnumerationType : struct
        {
            CheckIsEnum<TEnumerationType>(true);

            var enumerationValue = 0L;

            foreach (var enumerationFlag in enumerationFlags)
            {
                var enumerationFlagValue = Convert.ToInt64(enumerationFlag);

                enumerationValue |= enumerationFlagValue;
            }

            return (TEnumerationType) Enum.ToObject(typeof(TEnumerationType), enumerationValue);
        }

        public static string GetDescription<TEnumerationType>(this TEnumerationType enumeration)
            where TEnumerationType : struct
        {
            CheckIsEnum<TEnumerationType>(false);

            var enumerationName = Enum.GetName(typeof(TEnumerationType), enumeration);

            if (enumerationName != null)
            {
                var fieldInfo = typeof(TEnumerationType).GetField(enumerationName);

                if (fieldInfo != null &&
                    Attribute.GetCustomAttribute(fieldInfo,
                        typeof(DescriptionAttribute)) is DescriptionAttribute attributeDescription)
                    return attributeDescription.Description;
            }

            return null;
        }
        
    }
}