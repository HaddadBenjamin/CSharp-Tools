using System;
using System.IO;

namespace BenTools.Helpers.FileSystem
{
    public static class PathHelper
    {
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
