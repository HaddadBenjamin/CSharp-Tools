using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Ben.Tools.Services.Configurations
{
    /// <summary>
    /// Cette version permet d'importer seulement Newtonsoft.json ce qui est beaucoup moins lourd.
    /// </summary>
    public abstract class ALightConfigurationService : ILightConfigurationService
    {
        #region Field(s)
        protected readonly IConfigurationBuilder ConfigurationBuilder;
        public readonly ConfigurationEnvironments Environments;
        public readonly string Directory;
        public readonly bool MergeConfiguration;
        public readonly string Path;
        #endregion

        #region Constructor(s)
        public ALightConfigurationService(IConfigurationBuilder configurationBuilder, string directory = "Configurations", bool mergeConfiguration = true, string forcedCurrentEnvironment = null)
        {
            ConfigurationBuilder = configurationBuilder;

            Directory = directory;
            MergeConfiguration = mergeConfiguration;
            Path = System.IO.Path.Combine(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path), "..", Directory);

            bool forceEnvironment = !string.IsNullOrWhiteSpace(forcedCurrentEnvironment);

            if (!forceEnvironment)
                Environments = GetEnvironments();
            else
            {
                // Il n'est pas nécéssaire de créer le fichiers d'environnement si vous l'option merge environment n'est pas activée et que vous forced le répertoire de l'environnement utilisé.
                if (!mergeConfiguration)
                    Environments = new ConfigurationEnvironments()
                    {
                        Current = forcedCurrentEnvironment,
                        Default = forcedCurrentEnvironment
                    };

                Environments.Current = forcedCurrentEnvironment;
            }
        }
        #endregion

        #region Public Behaviour(s)
        /// <summary>
        /// La classe de configuration permet l'utilisation de champs requis ou privés et d'écrire et d'utiliser verbeusement vos configurations.
        /// </summary>
        public SectionType ToClass<SectionType>(string filename) =>
            ConfigurationBuilder.Deserialize<SectionType>(ConfigurationBuilder.Build(this, filename).content);
        #endregion

        #region Public Behaviour
        public (string current, string @default, string destination) BuildPaths(ALightConfigurationService configurationService, string filename)
        {
            var destinationPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}{configurationService.Extension}");
            var currentPath = System.IO.Path.Combine(configurationService.Path, configurationService.Environments.Current, $"{filename}{configurationService.Extension}");
            var defaultPath = System.IO.Path.Combine(configurationService.Path, configurationService.Environments.Default, $"{filename}{configurationService.Extension}");

            return (currentPath, defaultPath, destinationPath);
        }
        #endregion

        #region Abstract Behaviour(s)
        public abstract string Extension { get; }
        #endregion

        #region Intern Behaviour(s)
        protected ConfigurationEnvironments GetEnvironments() =>
            ConfigurationBuilder.Deserialize<ConfigurationEnvironments>(File.ReadAllText(System.IO.Path.Combine(Path, $"environments{Extension}")));
        #endregion
    }
}