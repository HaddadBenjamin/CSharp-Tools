using System;
using System.IO;
using System.Reflection;

namespace BenTools.Helpers.FileSystem
{ 
    public static class PathHelper
    {
        public static string GenerateUniquePath(string extension = ".txt") => $"{Path.GetTempPath()}{Guid.NewGuid()}{extension}";

        public static string GeneratePath(string filename, string extension = ".txt") => $"{Path.GetTempPath()}{filename}{extension}";

        public static string AppDataRoaming { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static string Desktop { get; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        
        /// <summary>
        /// => C:\Sources\SolutionName\ProjectName
        /// </summary>
        public static string ExecutingAssemblyPath => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// => C:\Sources\SolutionName\ProjectName\Configurations
        /// </summary>
        public static string ConfigurationPath => Path.Combine(ExecutingAssemblyPath, "Configurations");
    }
}
