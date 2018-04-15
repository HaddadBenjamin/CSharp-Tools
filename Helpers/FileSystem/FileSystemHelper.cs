using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ben.Tools.Extensions.Collections;
using Ben.Tools.Services;

namespace Ben.Tools.Helpers.FileSystem
{
    // Erreur :
    // Le chemmin n'est pas valide, trop long, contiend des caractères non voulues :
    // - solution : path.Replace("/", "\\");
    // Lorsque l'on travail avec des chemins de fichiers pointant sur le disque dur ":D" :
    //•	Il faut utiliser ":D\" plutôt que ":D/". 
    public static class FileSystemHelper
    {
         
        public static string SafeGetFileContent(string path) => 
            File.Exists(@path) ?
            File.ReadAllText(@path) :
            "";

        public static void ShowFileContent(string path)
        {
            DebugService.Instance.Log(SafeGetFileContent(@path));
        }

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

        public static IEnumerable<string> GetAllDirectoryFilesRecursively(
            string directoryPath,
            string[] extensions,
            string[] exclusiveIgnoreFilters) => 
            extensions.SelectMany(extension =>
                Directory.GetFiles(@directoryPath, "*" + extension, SearchOption.AllDirectories))
            .Where(fileName => !exclusiveIgnoreFilters.Contains(fileName));

        public static void ListAllFilesRecursivelyInADirectory(
            string directoryPath,
            string[] extensions,
            string[] exclusiveIgnoreFilters)
        {
            GetAllDirectoryFilesRecursively(directoryPath, extensions, exclusiveIgnoreFilters)
                .Foreach(DebugService.Instance.Log);
        }

        public static int SubDirectoriesCount(string rootDirectory) => 
            Directory.GetDirectories(@rootDirectory).Length;

        public static bool IsEmptyFile(string path) => new FileInfo(@path).Length == 0;

        public static string TakeOutExtension(string fileName) => Path.GetFileNameWithoutExtension(@fileName);

        public static bool DoesPathIsAFileOrADirectory(string path) => !string.IsNullOrWhiteSpace(Path.GetExtension(@path));

        public static string GetDesktopPath() => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static long GetDirectoryBytesSize(string directoryPath) => 
            Directory.GetFiles(@directoryPath, "*", SearchOption.AllDirectories)
            .Sum(subFile => (new FileInfo(@subFile).Length));

        public static bool IsReadonly(string path) => new FileInfo(path).IsReadOnly;
        
    }
}