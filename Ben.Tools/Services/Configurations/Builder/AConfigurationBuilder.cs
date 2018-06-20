using System.IO;

namespace Ben.Tools.Services.Configurations
{
    public abstract class AConfigurationBuilder : IConfigurationBuilder
    {
        #region Public Behaviour(s)
        public (string path, string content) Build(ALightConfigurationService configurationService, string filename)
        {
            var configurationPaths = configurationService.BuildPaths(configurationService, filename);

            if (!configurationService.MergeConfiguration)
                return (configurationPaths.current, File.ReadAllText(configurationPaths.current));

            var defaultFileContent = File.ReadAllText(configurationPaths.@default);

            if (configurationPaths.@default == configurationPaths.current)
                return (configurationPaths.@default, defaultFileContent);

            var currentFileContent = File.ReadAllText(configurationPaths.current);

            return Build(defaultFileContent, currentFileContent, configurationPaths.destination);
        }
        #endregion

        #region Abstract Behaviour(s)
        public abstract (string path, string content) Build(string defaultFileContent, string currentFileContent, string destinationPath);
        public abstract SectionType Deserialize<SectionType>(string fileContent);
        #endregion
    }
}