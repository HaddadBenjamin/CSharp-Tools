using System.IO;

namespace BenTools.Extensions.Streams
{
    public static class StreamExtension
    {
        public static void ToFile(this Stream stream, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                stream.CopyTo(fileStream);

            stream.Seek(0, SeekOrigin.Begin);
        }

        public static MemoryStream ToMemoryStream(this Stream stream)
        {
            var memoryStream = new MemoryStream();

            stream.CopyTo(memoryStream);

            return memoryStream;
        }

        public static byte[] ToBytes(this Stream stream)
        {
            using (var memoryStream = stream.ToMemoryStream())
                return memoryStream.ToArray();
        }
    }
}
