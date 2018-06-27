using System.Collections.Generic;
using BenTools.Services.Configurations.Builder;
using BenTools.Services.Configurations.Options;
using System.IO;

namespace BenTools.Services.Configurations.Light
{
    /// <summary>
    /// Cette version permet d'importer seulement Newtonsoft.json ce qui est beaucoup moins lourd.
    ///
    /// Description :
    /// - Service permettant de simplifier et de réduire l'écriture et l'utilisation des fichiers de configurations.
    /// - Vous pouvez choisir de fusionné vos fichiers de configurations ou non avec sa version de l'environnement par défault définit.
    /// 
    /// Les types de services :
    /// - Light : 
    ///     + Vous devez référencer seulement Newtonsoft.json.
    ///     + ToClass() : convertit votre fichier de configuration en permet de définir des champs requis avec [JsonProperty(Required = Required.Always)] et des champs privée avec [JsonProperty] pour sa version Json.
    ///                   vous pouvez récupérer des sous sections de configurations avec le paramètres subSections.  
    /// - Normal :
    ///     + Vous devez référencer Newtonsoft.Json, System.Extensions.Configuration.Json et Microsoft.Extensions.Configuration.Binder.
    ///     + Donne accès à toutes les intéractions de sa version light.
    ///     + ToRoot : permet d'éviter de définir une classe de mappage de votre fichier de configuration pour utiliser votre fichier de configuration tel quel et donc très rapidement.
    /// 
    /// Raison de création de ce service :
    /// - Le système éxistant ConfigurationBuilder ne permet pas de définir des champs requis ou privées.
    /// - Le système éxistant ConfigurationSection ne permet pas de baser ses configurations sur des fichiers existants et est trop long à utiliser.
    /// 
    /// Fonctionnalités :
    /// - Permet l'utilisation de champs requis avec [JsonProperty(Required = Required.Always)] pour que le programme compile et de champs privée avec [JsonProperty] pour ne pas autoriser la modification du champ de configuration.
    /// - Vous pouvez (service.ToClass) ou non (service.ToConfigurationRoot) définir une classe correspond à votre fichier de configuration pour faciliter et réduire l'écriture de son mappage (de vos classes de configurations).
    /// - Base si vous le souhaitez tous vos fichiers de configurations sur leurs versions de l'environnement par défault pour réduire et simplifier leur écriture.
    /// - Vos fichiers de configurations sont mieux ordonnés et sont stockés dans un répertoire en fonction de leur environnement qu'ils définissent.
    /// - Vous pouvez redéfinir le répertoire d'environnement courant utilisé afin de spécifier d'où vont être récupéré vos fichiers de configurations.
    /// 
    /// Prérequis :
    /// - Installer Newtonsoft.Json, System.Extensions.Configuration.Json et Microsoft.Extensions.Configuration.Binder via NuGet.
    ///     Pour la version light de ce service seulement Newtonsoft.Json est requis mais elle ne permet pas d'utiliser la racine de configuration pour utiliser le fichier de configuration tel quel sans classe de mappage.
    /// - Créer le fichier Configurations/environments.json à la racine du projet qui définira l'environnement par défault et courant.
    ///     Exemple : { "Default": "Development", "Current": "Test"}
    /// - Trier vos fichiers de configurations par environnement, exemple :
    ///     - Configurations/Development/configurationSample.json
    ///     - Configurations/Test/configurationSample.json
    /// </summary>
    public abstract class ALightConfigurationService : ILightConfigurationService
    {
        #region Field(s)
        protected readonly IConfigurationBuilder Builder;
        protected IConfigurationOptions Options;
        #endregion

        #region Constructor(s)
        public ALightConfigurationService(IConfigurationBuilder builder, IConfigurationOptions options)
        {
            Builder = builder;

            RefreshOptions(options);
        }
        #endregion

        #region Public Behaviour(s)
        /// <summary>
        /// La classe de configuration permet l'utilisation de champs requis ou privés et d'écrire et d'utiliser verbeusement vos configurations.
        /// </summary>
        public SectionType ToClass<SectionType>(string filename, IEnumerable<string> subSections = null) =>
            Builder.Deserialize<SectionType>(Builder.BuildConfiguration(Options, filename, Extension, subSections).FileContent);

        public void RefreshOptions(IConfigurationOptions options)
        {
            Options = options;

            Options.BuildOptions(BuildEnvironments);
        }
        #endregion

        #region Abstract Behaviour(s)
        public abstract string Extension { get; }
        #endregion

        #region Intern Behaviour(s)
        protected ConfigurationEnvironments BuildEnvironments()
        {
            var environmentsFilePath = Path.Combine(Options.ConfigurationPath, $"environments{Extension}");

            if (!File.Exists(environmentsFilePath))
                throw new FileNotFoundException(environmentsFilePath);

            var environmentsFileContent = File.ReadAllText(environmentsFilePath);

            return Builder.Deserialize<ConfigurationEnvironments>(environmentsFileContent);
        }
        #endregion
    }
}