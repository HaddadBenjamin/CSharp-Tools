using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Ben.Tools.Services.Configurations
{
    /// <summary>
    /// Description :
    /// - Service permettant de simplifier et de réduire l'écriture et l'utilisation des fichiers de configurations.
    /// - Baser chaque fichiers de configurations sur leur versions de l'environnement par défault définit pour réduire les éléments de configuration copiés.
    /// 
    /// Prérequis :
    /// - Installer System.Extensions.Configuration.Json et Microsoft.Extensions.Configuration.Binder via NuGet.
    /// - Il faut avoir un répertoire de Configurations à la racine du projet.
    /// - Dans ce dernier le fichier environments.json qui devra définir les environnements courant et par défault utiliser.
    ///   +  Exemple : Configurations/environments.json : { "CurrentEnvironment": "Test", "DefaultEnvironment": "Development" }
    /// - Le répertoire de configurations devra contenir un répertoire par environnement avec ses fichiers de configurations.
    ///   +  Exemple : Configurations/D evelopment/myconfigurations.json :   "firstSection": {{ "environment": "Development" }} 
    ///                Configurations/Test/myconfigurations.json :          "firstSection": {{ "environment": "Test" }} 
    ///
    /// Avantages :
    /// - Simplifie grandement l'utilisation et l'écriture des fichiers de configurations.
    /// - Fonctionnalité : il y a un répertoire de fichiers de configuration différent en fonction de l'environnement.
    /// - Simplifie l'écriture : n'oblige pas de passer par une sous classe pour récupérer nos éléments de configuration.
    /// - Simplifie l'écriture : peut si vous le souhaitez passer par une classe de configuration pour faire la liaison avec vos fichiers de configuration.
    /// - Simplifie l'écriture et fonctionnalité : fusionne des fichiers de configurations afin d'éviter de réécrire des éléments commums.
    /// 
    /// Inconvénients :
    /// - On ne peut pas supprimer les setteurs de nos classes de configurations.
    /// - On ne peut pas spécifier qu'un champ est requis dans la classe de configuration pour faire péter le compilateur lorsqu'un champ est manquant.
    /// 
    /// To do :
    /// - Envoyer ce service sur Github, Bitbucket.
    /// - Cours complet sur toute les classes que j'ai utilisé, fichiers de configurations, méthodes, etc...
    /// </summary>
    public abstract class AConfigurationService : IConfigurationService
    {
        #region Field(s)
        private EnvironmentSection Environments;
        private string ConfigurationDirectory;
        private string ConfigurationPath;
        #endregion

        #region Constructor(s)
        public AConfigurationService(string configurationDirectory = "Configurations")
        {
            ConfigurationDirectory = configurationDirectory;
            ConfigurationPath = Path.Combine(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path), "..", ConfigurationDirectory);
            Environments = GetEnvironments();
        }
        #endregion

        #region Virtual Behaviour(s)
        public EnvironmentSection GetEnvironments() =>
            AddConfigurationFile(new ConfigurationBuilder().SetBasePath(ConfigurationPath), $"environments{ConfigurationExtension}")
                .Build()
                .Get<EnvironmentSection>();

        public SectionType ToClass<SectionType>(string configurationFilename) =>
            ToConfigurationRoot(configurationFilename)
                .Get<SectionType>();

        public IConfigurationRoot ToConfigurationRoot(string configurationFilename)
        {
            var jsonPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}{ConfigurationExtension}");
            var currentEnvironmentPath = Path.Combine(ConfigurationPath, Environments.CurrentEnvironment, $"{configurationFilename}{ConfigurationExtension}");
            var defaultEnvironmentPath = Path.Combine(ConfigurationPath, Environments.DefaultEnvironment, $"{configurationFilename}{ConfigurationExtension}");
            var mergedConfigurationFileContent = ToMergedConfigurationFileContent(currentEnvironmentPath, defaultEnvironmentPath);

            File.WriteAllText(jsonPath, mergedConfigurationFileContent);

            return AddConfigurationFile(new ConfigurationBuilder(), jsonPath).Build();
        }
        #endregion

        #region Abstract Behaviour(s)
        protected abstract string ConfigurationExtension { get; }

        protected abstract IConfigurationBuilder AddConfigurationFile(IConfigurationBuilder builder, string configurationPath);

        protected abstract string ToMergedConfigurationFileContent(string currentEnvironmentPath, string defaultEnvironmentPath);
        #endregion
    }
}