using System;

namespace BenTools.Services.Configurations.Light.Options
{
    public interface IConfigurationOptions
    {
        ConfigurationEnvironments ConfigurationEnvironments { get; set; }
        bool MergeConfigurationFiles { get; set; }
        string ConfigurationPath { get; set; }
        string EnvironmentDirectory { get; set; }

        void BuildOptions(Func<ConfigurationEnvironments> getConfigurationEnvironments);
        ConfigurationPathsResult BuildPaths(string filename, string extension);
    }
}