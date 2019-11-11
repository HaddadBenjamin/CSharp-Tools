using System;
using System.Linq;
using System.Reflection;
using BenTools.Utilities.Attributes;

namespace BenTools.Extensions.Enums
{
    public static class EnumExtension
    {
        /// <summary>
        /// Return the enumeration display value attribute associated to the following enumeration value.
        /// </summary>
        public static string GetDisplayName(this Enum enumeration)
            => enumeration
                   .GetType()
                   .GetMember((enumeration.ToString()))
                   .FirstOrDefault()
                   .GetCustomAttributes<EnumerationDisplayValueAttribute>(false)
                   .FirstOrDefault()?.DisplayValue ??
               throw new Exception($"({enumeration.GetType().Name} with value {enumeration} requires a [DisplayAttribute]");

    }
}