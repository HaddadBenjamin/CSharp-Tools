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
        protected readonly IMergedConfigurationBuilder MergedConfigurationBuilder;
        public readonly ConfigurationEnvironments Environments;
        public readonly string Directory;
        public readonly string Path;
        #endregion

        #region Constructor(s)
        public ALightConfigurationService(IMergedConfigurationBuilder mergedConfigurationBuilder, string directory = "Configurations")
        {
            MergedConfigurationBuilder = mergedConfigurationBuilder;

            Directory = directory;
            Path = System.IO.Path.Combine(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path), "..", Directory);
            Environments = GetEnvironments();
        }
        #endregion

        #region Public Behaviour(s)
        /// <summary>
        /// La classe de configuration permet l'utilisation de champs requis ou privés et d'écrire et d'utiliser verbeusement vos configurations.
        /// </summary>
        public SectionType ToClass<SectionType>(string filename) =>
            JsonConvert.DeserializeObject<SectionType>(MergedConfigurationBuilder.Build(this, filename).content);
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
            JsonConvert.DeserializeObject<ConfigurationEnvironments>(File.ReadAllText(System.IO.Path.Combine(Path, $"environments{Extension}")));
        #endregion
    }

}