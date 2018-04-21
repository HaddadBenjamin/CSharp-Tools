using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ben.Tools.Extensions.Collections;
using Ben.Tools.Services;

namespace Ben.Tools.Helpers.FileSystem
{
        public static string SafeGetFileContent(string path) => 
                File.Exists(@path) ?
                File.ReadAllText(@path) :
                string.Empty;

        public static bool IsOfExtension(string path, string[] extensions) => 
            extensions.Contains(Path.GetExtension(path));

        public static void SafeCreatePath(string path)
        {
            try
            {
                var pathDirectory = Path.GetDirectoryName(@path);

                if (!string.IsNullOrEmpty(@pathDirectory) &&
                    !Directory.Exists(@pathDirectory))
                    Directory.CreateDirectory(@pathDirectory);


                if (!string.IsNullOrEmpty(@path) &&
                    !File.Exists(@path))
                {
                    using (var fileStream = System.IO.File.Create(@path))
                        fileStream.Close();
                }
            }
            catch (Exception exception)
            {
                DebugService.Instance.Log(string.Format("Exception lancé durant la création d'un chemin : {0} au chemin {1}",
                    exception.Message,
                    path));

                throw;
            }
        }

        public static IEnumerable<string> EnumerateFiles(
            string directoryPath,
            string[] extensions,
            params string[] exclusiveIgnoreFilters) => 
            extensions.SelectMany(extension =>
                Directory.EnumerateFiles(directoryPath, "*", SearchOption.AllDirectories)
                         .Where(filePath => extensions.Contains(Path.GetExtension(filePath)) &&
                                            !exclusiveIgnoreFilters.Contains(filePath)));

        public static string TakeOutExtension(string fileName) => Path.GetFileNameWithoutExtension(@fileName);
    }
}
