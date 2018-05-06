using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ben.Tools.Extensions.Collections;
using Ben.Tools.Services;

namespace Ben.Tools.Helpers.FileSystem
{
    public static class FileSystemHelper
    { 
       public static string SafeGetFileContent(string path) => File.Exists(@path) ? File.ReadAllText(@path) : string.Empty;

        public static bool IsOfExtension(string path, string[] extensions) =>  extensions.Contains(Path.GetExtension(path));

        public static IEnumerable<string> EnumerateFiles(
            string directoryPath,
            string[] extensions,
            params string[] ignoreFilters) => 
            extensions.SelectMany(extension =>
                Directory.EnumerateFiles(directoryPath, "*", SearchOption.AllDirectories)
                         .Where(filePath => extensions.Contains(Path.GetExtension(filePath)) &&
                                            !ignoreFilters.Contains(filePath)));

        public static string TakeOutExtension(string fileName) => Path.GetFileNameWithoutExtension(@fileName);
    }
}
