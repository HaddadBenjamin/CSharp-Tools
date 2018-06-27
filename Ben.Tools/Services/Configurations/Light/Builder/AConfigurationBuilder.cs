using System.Collections.Generic;
using System.IO;
using BenTools.Services.Configurations.Light.Options;

namespace BenTools.Services.Configurations.Light.Builder
{
    public abstract class AConfigurationBuilder : IConfigurationBuilder
    {
        #region Public Behaviour(s)
        public ConfigurationBuildResult BuildConfiguration(IConfigurationOptions configurationOptions, string filename, string extension, IEnumerable<string> subSections)
        {
            var configurationPaths = configurationOptions.BuildPaths(filename, extension);

            if (!File.Exists(configurationPaths.Current))
                throw new FileNotFoundException(configurationPaths.Current);

            if (!configurationOptions.MergeConfigurationFiles)
                return new ConfigurationBuildResult(configurationPaths.Current, File.ReadAllText(configurationPaths.Current));

            if (!File.Exists(configurationPaths.Default))
                throw new FileNotFoundException(configurationPaths.Default);

            var defaultFileContent = File.ReadAllText(configurationPaths.Default);

            if (configurationPaths.Default == configurationPaths.Current)
                return new ConfigurationBuildResult(configurationPaths.Default, defaultFileContent);

            var currentFileContent = File.ReadAllText(configurationPaths.Current);

            return MergeConfiguration(defaultFileContent, currentFileContent, configurationPaths.Destination);
        }
        #endregion

        #region Abstract Behaviour(s)
        public abstract ConfigurationBuildResult MergeConfiguration(string defaultFileContent, string currentFileContent, string destinationPath);
        public abstract SectionType Deserialize<SectionType>(string fileContent);
        public abstract string ToRawSection(string fileContent, IEnumerable<string> subSections);
        #endregion
    }
}