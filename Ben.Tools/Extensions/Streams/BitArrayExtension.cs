using System.Collections;
using System.Text;

namespace BenTools.Extensions.Streams
{
    public static class BitArrayExtension
    {
        public static string ToBitString(this BitArray bits)
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < bits.Count; i++)
                stringBuilder.Append(bits[i] ? '1' : '0');

            return stringBuilder.ToString();
        }
    }
}