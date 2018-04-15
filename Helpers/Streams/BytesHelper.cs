using System.Drawing;
using System.IO;

namespace Ben.Tools.Helpers.Streams
{
    public static class BytesHelper
    {
         
        public static Bitmap ToBitmap(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
                return new Bitmap(memoryStream);
        }
        
        public static Stream Tostream(byte[] bytes) => new MemoryStream(bytes);

        
    }
}