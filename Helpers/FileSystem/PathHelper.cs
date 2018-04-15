using System;
using System.IO;

namespace Ben.Tools.Helpers.FileSystem
{ 
    public static class PathHelper
    {
         
        public static string GenerateUniquePath(string extension = ".txt") => $"{Path.GetTempPath()}{Guid.NewGuid()}{extension}";

        public static string GeneratePath(string filename, string extension = ".txt") => $"{Path.GetTempPath()}{filename}{extension}";

        public static string GetAppDataRoamingPath(params string[] subPaths)
        {
            var appDataRoamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            foreach (var subPath in subPaths)
                appDataRoamingPath = Path.Combine(appDataRoamingPath, subPath);

            return appDataRoamingPath;
        }
        
    }
}