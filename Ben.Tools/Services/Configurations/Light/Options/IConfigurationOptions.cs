using System;

namespace BenTools.Services.Configurations.Options
{
    public interface IConfigurationOptions
    {
        ConfigurationEnvironments ConfigurationEnvironments { get; set; }
        bool MergeConfigurationFiles { get; set; }
        string ConfigurationPath { get; set; }
        string DirectoryEnvironment { get; set; }

        void BuildOptions(Func<ConfigurationEnvironments> getConfigurationEnvironments);
        ConfigurationPathsResult BuildPaths(string filename, string extension);
    }
}