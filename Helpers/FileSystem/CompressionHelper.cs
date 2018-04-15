using System.IO;
using System.IO.Compression;
using Ionic.Zip;
using ZipFile = Ionic.Zip.ZipFile;

namespace Ben.Tools.Helpers.FileSystem
{
    public static class CompressionHelper
    {
         
        public static string DecompressGzipBytesInFile(
            byte[] gzipBytes,
            string decompressedFileExtension = ".json")
        {
            var decompressedPath = $"{Path.GetTempFileName()}{decompressedFileExtension}";
            var gzipPath = $"{decompressedPath}.gz";

            File.WriteAllBytes(gzipPath, gzipBytes);

            var fileInfo = new FileInfo(gzipPath);

            using (var gzipFileStream = fileInfo.OpenRead())
            using (var decompressedStream = File.Create(decompressedPath))
            {
                var streamDecompressed = new GZipStream(
                    gzipFileStream,
                    CompressionMode.Decompress);

                streamDecompressed.CopyTo(decompressedStream);

                return decompressedPath;
            }
        }
        

        //// Avec une dépendance au paquet NuGet System.IO.Compression.FileSystem
        //public static void DecompressZip(
        //        string zipPath,
        //        string unzipPath)
        //{
        //    ZipFile.ExtractToDirectory(zipPath, unzipPath);
        //}

        //// Avec une dépendance au paquet NuGet System.IO.Compression.FileSystem
        //public static void CompressADirectoryAsZip(
        //    string directoryToZip,
        //    string zipPath)
        //{
        //    ZipFile.CreateFromDirectory(directoryToZip, zipPath);
        //}

        /// <summary>
        ///     Décompresse le tableau de bytes byteArray dans le chemin défini par unzipPath.
        /// </summary>
        public static void UnzipByteArray(
            byte[] byteArray,
            string unzipPath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(unzipPath));

            using (var memoryStream = new MemoryStream(byteArray))
            using (var zipFile = ZipFile.Read(memoryStream))
                zipFile.ExtractAll(unzipPath, ExtractExistingFileAction.OverwriteSilently);
        }

        /// <summary>
        ///     Décompresse le .zip zipPath dans un répertoire défini par unzipPath.
        /// </summary>
        public static void Unzip(
            string zipPath,
            string unzipPath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(unzipPath));

            using (var zipFile = ZipFile.Read(zipPath))
                zipFile.ExtractAll(unzipPath, ExtractExistingFileAction.OverwriteSilently);
        }

        
        
    }
}