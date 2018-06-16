using System;
using System.IO;
using System.Reflection;
using Ben.Tools.Services.Configurations;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Ben.Tools.Services.Configurations
{
    /// <summary>
    /// Description :
    /// - Service permettant de simplifier et de réduire l'écriture et l'utilisation des fichiers de configurations.
    /// - Chaque fichier de configuration est fusionné à sa définition de l'environnement par défault.
    /// 
    /// Raison de création de ce service :
    /// - Le système éxistant ConfigurationBuilder ne permet pas de définir des champs requis ou privée.
    /// - Le système éxistant ConfigurationSection ne permet pas de baser ses configurations sur des fichiers existant et est trop long à utiliser.
    /// 
    /// Fonctionnalités :
    /// - Permet l'utilisation de champs requis [JsonProperty(Required = Required.Always)] pour que le programme compile et de champs privée [JsonProperty].
    /// - Vous pouvez (service.ToClass) ou non (service.ToConfigurationRoot) définir une classe correspond à votre fichier de configuration pour faciliter et réduire l'écriture le mappage.
    /// - Base tous vos fichiers de configuration sur leur définition d'un environnement par défault pour réduire et simplifier l'écriture de ces derniers.
    /// - Vos fichiers de configurations sont mieux ordonnés et sont stockés dans un répertoire en fonction de leur environnement qu'il définisse.
    /// 
    /// Prérequis :
    /// - Installer Newtonsoft.Json, System.Extensions.Configuration.Json et Microsoft.Extensions.Configuration.Binder via NuGet.
    /// - Créer le fichier Configurations/environments.json à la racine du projet qui définira l'environnement par défault et courant.
    ///     Exemple : { "Default": "Development", "Current": "Test"}
    /// - Trier vos fichiers de configurations par environnement, exemple :
    ///     - Configurations/Development/configurationSample.json
    ///     - Configurations/Test/configurationSample.json
    public abstract class AConfigurationService : IConfigurationService
    {
        #region Field(s)
        private ConfigurationEnvironments ConfigurationEnvironments;
        private string ConfigurationDirectory;
        private string ConfigurationPath;
        #endregion

        #region Constructor(s)
        public AConfigurationService(string configurationDirectory = "Configurations")
        {
            ConfigurationDirectory = configurationDirectory;
            ConfigurationPath = Path.Combine(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path), "..", ConfigurationDirectory);
            ConfigurationEnvironments = GetEnvironments();
        }
        #endregion

        #region Public Behaviour(s)
        /// <summary>
        /// La classe de configuration permet l'utilisation de champs requis ou privée et d'écrire et d'utiliser verbeusement vos configurations.
        /// </summary>
        public SectionType ToClass<SectionType>(string configurationFilename) =>
            JsonConvert.DeserializeObject<SectionType>(GenerateMergedConfiguration(configurationFilename).content);

        /// <summary>
        /// La racine de configuration permet d'éviter de définir une classe de mappage à votre fichier de configuration pour utiliser votre configuration rapidement.
        /// </summary>
        public IConfigurationRoot ToConfigurationRoot(string configurationFilename) =>
            AddConfigurationFile(new ConfigurationBuilder(), GenerateMergedConfiguration(configurationFilename).path).Build();
        #endregion

        #region Abstract Behaviour(s)
        protected abstract string ConfigurationExtension { get; }

        protected abstract IConfigurationBuilder AddConfigurationFile(IConfigurationBuilder builder, string configurationPath);

        protected abstract (string path, string content) GenerateMergedConfiguration(string configurationFilename);
        #endregion

        #region Intern Behaviour(s)
        protected (string current, string @default, string destination) GenerateConfigurationPaths(string configurationFilename)
        {
            var destinationPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}{ConfigurationExtension}");
            var currentEnvironmentPath = Path.Combine(ConfigurationPath, ConfigurationEnvironments.Current, $"{configurationFilename}{ConfigurationExtension}");
            var defaultEnvironmentPath = Path.Combine(ConfigurationPath, ConfigurationEnvironments.Default, $"{configurationFilename}{ConfigurationExtension}");

            return (currentEnvironmentPath, defaultEnvironmentPath, destinationPath);
        }

        protected ConfigurationEnvironments GetEnvironments() =>
            JsonConvert.DeserializeObject<ConfigurationEnvironments>(File.ReadAllText(Path.Combine(ConfigurationPath, $"environments{ConfigurationExtension}")));
        #endregion
    }
}