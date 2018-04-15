using System.IO;

namespace Ben.Tools.Extensions.Streams
{
    public static class StreamExtension
    {
        public static void ToFile(
            this Stream stream,
            string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                var buffer = new byte[1024];
                int len;

                while ((len = stream.Read(buffer, 0, buffer.Length)) > 0) fileStream.Write(buffer, 0, len);
            }

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