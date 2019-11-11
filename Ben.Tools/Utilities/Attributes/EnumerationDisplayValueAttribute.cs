using System;

namespace BenTools.Utilities.Attributes
{
    public class EnumerationDisplayValueAttribute : Attribute
    {
        public string DisplayValue { get; }

        public EnumerationDisplayValueAttribute(string displayValue)
        {
            DisplayValue = displayValue;
        }
    }
}