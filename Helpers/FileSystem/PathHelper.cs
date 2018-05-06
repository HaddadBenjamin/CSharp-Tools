using System;
using System.IO;

namespace Ben.Tools.Helpers.FileSystem
{ 
    public static class PathHelper
    {
        public static string GenerateUniquePath(string extension = ".txt") => $"{Path.GetTempPath()}{Guid.NewGuid()}{extension}";

        public static string GeneratePath(string filename, string extension = ".txt") => $"{Path.GetTempPath()}{filename}{extension}";

        public static string AppDataRoaming { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static string Desktop { get; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    }
}
