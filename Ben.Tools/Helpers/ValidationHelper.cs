using System;
using System.Text.RegularExpressions;

namespace BenTools.Helpers
{
    public static class ValidationHelper
    {
         
        public static bool IsRegex(string regex)
        {
            try
            {
                Regex.Match("", regex);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }
        
    }
}