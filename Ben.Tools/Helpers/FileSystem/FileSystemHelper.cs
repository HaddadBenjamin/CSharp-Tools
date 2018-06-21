using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BenTools.Helpers.FileSystem
{
    public static class FileSystemHelper
    { 
       public static string SafeGetFileContent(string path) => File.Exists(@path) ? File.ReadAllText(@path) : string.Empty;

        public static bool IsOfExtension(string path, string[] extensions) =>  extensions.Contains(Path.GetExtension(path));

        public static IEnumerable<string> EnumerateFiles(string directoryPath, IEnumerable<string> extensions, params string[] ignoreFilters) =>
                Directory.EnumerateFiles(directoryPath, "*", SearchOption.AllDirectories)
                         .AsParallel() // à retirer si vous estimiez que peu de fichiers seront récupérés.
                         .Where(filePath => extensions.Contains(Path.GetExtension(filePath)) &&
                                            !ignoreFilters.Any(ignoreFilter => filePath.Contains(ignoreFilter)));

        public static string TakeOutExtension(string fileName) => Path.GetFileNameWithoutExtension(@fileName);
    }
}
