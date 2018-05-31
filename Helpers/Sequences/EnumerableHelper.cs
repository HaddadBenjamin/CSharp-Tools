using System;
using System.Collections.Generic;
using System.Linq;

namespace Ben.Tools.Helpers.Sequences
{
    public static class EnumerableHelper
    {
        public static IEnumerable<double> GenerateNormalizedValues(int numberOfElements) =>
            Enumerable.Repeat<Func<double>>(new Random().NextDouble, numberOfElements)
                      .Select(generateValueFunction => generateValueFunction());
    }
}