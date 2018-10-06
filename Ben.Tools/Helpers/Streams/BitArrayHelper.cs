using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BenTools.Helpers.Streams
{
    public class BitArrayHelper
    {
        public static IEnumerable<bool> ToBits(byte[] bytes) => new BitArray(bytes).Cast<bool>();
    }
}