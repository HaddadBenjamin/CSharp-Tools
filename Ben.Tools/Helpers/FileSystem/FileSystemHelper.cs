using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BenTools.Helpers.FileSystem
{
    public static class FileSystemHelper
    {
        public static string SafeGetFileContent(string path) => File.Exists(@path) ? File.ReadAllText(@path) : string.Empty;

        public static IEnumerable<string> EnumerateFiles(string directoryPath, IEnumerable<string> extensions, params string[] ignoreFilters) =>
                 Directory.EnumerateFiles(directoryPath, "*", SearchOption.AllDirectories)
                          .Where(filePath => extensions.Contains(Path.GetExtension(filePath)) && !ignoreFilters.Any(filePath.Contains));
    }
}
