using System;
using System.IO;

namespace BenTools.Services.Configurations.Light.Options
{
    public class ConfigurationOptions : IConfigurationOptions
    {
        #region Field(s)
        public ConfigurationEnvironments ConfigurationEnvironments { get; set; }
        public bool MergeConfigurationFiles { get; set; }
        public string ConfigurationPath { get; set; }
        public string EnvironmentDirectory { get; set; }
        #endregion

        #region Constructor(s)
        public ConfigurationOptions(string configurationDirectory = "Configurations", bool mergeConfigurationFiles = true, string environmentDirectory = null)
        {
            MergeConfigurationFiles = mergeConfigurationFiles;
            ConfigurationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configurationDirectory);
            EnvironmentDirectory = environmentDirectory;     
        }
        #endregion

        #region Public Behaviour(s)
        public void BuildOptions(Func<ConfigurationEnvironments> getConfigurationEnvironments)
        {
            bool forceEnvironment = !string.IsNullOrWhiteSpace(EnvironmentDirectory);

            if (!forceEnvironment)
                ConfigurationEnvironments = getConfigurationEnvironments();
            else
            {
                // Il n'est pas nécéssaire de créer le fichiers d'environnement si vous l'option merge environment n'est pas activée et que vous forced le répertoire de l'environnement utilisé.
                if (!MergeConfigurationFiles)
                    ConfigurationEnvironments = new ConfigurationEnvironments()
                    {
                        Current = EnvironmentDirectory,
                        Default = EnvironmentDirectory
                    };

                ConfigurationEnvironments.Current = EnvironmentDirectory;
            }
        }

        public ConfigurationPathsResult BuildPaths(string filename, string extension) =>
            new ConfigurationPathsResult(
                @default: Path.Combine(ConfigurationPath, ConfigurationEnvironments.Default, $"{filename}{extension}"),
                current: Path.Combine(ConfigurationPath, ConfigurationEnvironments.Current, $"{filename}{extension}"),
                destination : Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}{extension}"));
        #endregion
    }
}