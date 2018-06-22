using System.Collections.Generic;
using System.IO;
using System.Linq;
using BenTools.Services.Configurations.Options;
using Newtonsoft.Json.Linq;

namespace BenTools.Services.Configurations.Builder
{
    public abstract class AConfigurationBuilder : IConfigurationBuilder
    {
        #region Public Behaviour(s)
        public ConfigurationBuildResult Build(IConfigurationOptions configurationOptions, string filename, string extension, params string[] subSections)
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

            return Build(defaultFileContent, currentFileContent, configurationPaths.Destination);
        }
        #endregion

        #region Abstract Behaviour(s)
        public abstract ConfigurationBuildResult Build(string defaultFileContent, string currentFileContent, string destinationPath);
        public abstract SectionType Deserialize<SectionType>(string fileContent);
        public abstract string BuildRawSection(string fileContent, params string[] subSections);
        #endregion
    }
}