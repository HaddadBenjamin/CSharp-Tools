using System;
using System.Collections.Generic;
using System.Linq;

namespace Ben.Tools.Helpers.Collections
{
    public static class ListHelper
    {
        public static IEnumerable<double> GenerateNormalizedValues(int numberOfElements)
        {
            var random = new Random();

            return Enumerable.Repeat<Func<double>>(random.NextDouble, numberOfElements)
                             .Select(generateValueFunction => generateValueFunction());
        }
    }
}