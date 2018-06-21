using System;
using System.Reflection;

namespace BenTools.Services.Configurations.Options
{
    public class ConfigurationOptions : IConfigurationOptions
    {
        #region Field(s)
        public ConfigurationEnvironments Environments { get; set; }
        public string Directory { get; set; }
        public bool MergeConfiguration { get; set; }
        public string Path { get; set; }
        public string ForcedCurrentEnvironment { get; set; }
        #endregion

        #region Constructor(s)
        public ConfigurationOptions(string directory = "Configurations", bool mergeConfiguration = true, string forcedCurrentEnvironment = null)
        {
            Directory = directory;
            MergeConfiguration = mergeConfiguration;
            Path = System.IO.Path.Combine(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path), "..", Directory);
            ForcedCurrentEnvironment = forcedCurrentEnvironment;     
        }
        #endregion

        #region Public Behvaiour(s)
        public void BuildOptions(Func<ConfigurationEnvironments> getConfigurationEnvironments)
        {
            bool forceEnvironment = !string.IsNullOrWhiteSpace(ForcedCurrentEnvironment);

            if (!forceEnvironment)
                Environments = getConfigurationEnvironments();
            else
            {
                // Il n'est pas nécéssaire de créer le fichiers d'environnement si vous l'option merge environment n'est pas activée et que vous forced le répertoire de l'environnement utilisé.
                if (!MergeConfiguration)
                    Environments = new ConfigurationEnvironments()
                    {
                        Current = ForcedCurrentEnvironment,
                        Default = ForcedCurrentEnvironment
                    };

                Environments.Current = ForcedCurrentEnvironment;
            }
        }

        public (string current, string @default, string destination) BuildPaths(string filename, string extension)
        {
            var destinationPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}{extension}");
            var currentPath = System.IO.Path.Combine(Path, Environments.Current, $"{filename}{extension}");
            var defaultPath = System.IO.Path.Combine(Path, Environments.Default, $"{filename}{extension}");

            return (currentPath, defaultPath, destinationPath);
        }
        #endregion
    }
}