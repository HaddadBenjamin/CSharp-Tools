using System;
using System.IO;
using System.Reflection;

namespace BenTools.Services.Configurations.Options
{
    public class ConfigurationOptions : IConfigurationOptions
    {
        #region Field(s)
        public ConfigurationEnvironments ConfigurationEnvironments { get; set; }
        public bool MergeConfigurationFiles { get; set; }
        public string ConfigurationPath { get; set; }
        public string DirectoryEnvironment { get; set; }
        #endregion

        #region Constructor(s)
        public ConfigurationOptions(string configurationDirectory = "Configurations", bool mergeConfigurationFiles = true, string directoryEnvironment = null)
        {
            MergeConfigurationFiles = mergeConfigurationFiles;
            ConfigurationPath = Path.Combine(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path), "..", configurationDirectory);
            DirectoryEnvironment = directoryEnvironment;     
        }
        #endregion

        #region Public Behvaiour(s)
        public void BuildOptions(Func<ConfigurationEnvironments> getConfigurationEnvironments)
        {
            bool forceEnvironment = !string.IsNullOrWhiteSpace(DirectoryEnvironment);

            if (!forceEnvironment)
                ConfigurationEnvironments = getConfigurationEnvironments();
            else
            {
                // Il n'est pas nécéssaire de créer le fichiers d'environnement si vous l'option merge environment n'est pas activée et que vous forced le répertoire de l'environnement utilisé.
                if (!MergeConfigurationFiles)
                    ConfigurationEnvironments = new ConfigurationEnvironments()
                    {
                        Current = DirectoryEnvironment,
                        Default = DirectoryEnvironment
                    };

                ConfigurationEnvironments.Current = DirectoryEnvironment;
            }
        }

        public ConfigurationPathsResult BuildPaths(string filename, string extension)
            => new ConfigurationPathsResult(
                @default: Path.Combine(ConfigurationPath, ConfigurationEnvironments.Default, $"{filename}{extension}"),
                current: Path.Combine(ConfigurationPath, ConfigurationEnvironments.Current, $"{filename}{extension}"),
                destination : Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}{extension}"));
        #endregion
    }
}