using System;

namespace BenTools.Services.Configurations.Options
{
    public interface IConfigurationOptions
    {
        ConfigurationEnvironments Environments { get; set; }
        string Directory { get; set; }
        bool MergeConfiguration { get; set; }
        string Path { get; set; }
        string ForcedCurrentEnvironment { get; set; }

        void BuildOptions(Func<ConfigurationEnvironments> getConfigurationEnvironments);
        (string current, string @default, string destination) BuildPaths(string filename, string extension);
    }
}