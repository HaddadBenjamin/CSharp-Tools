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
    /// - Chaque fichier de configuration est fusionné avec sa version de l'environnement par défault définit.
    /// 
    /// Les types de services :
    /// - Light : 
    ///     + Vous devez référencer seulement Newtonsoft.json.
    ///     + ToClass() : convertit votre fichier de configuration en permet de définir des champs requis avec [JsonProperty(Required = Required.Always)] et des champs privée avec [JsonProperty] pour sa version Json.
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
    /// - Base tous vos fichiers de configurations sur leurs versions de l'environnement par défault pour réduire et simplifier leur écriture.
    /// - Vos fichiers de configurations sont mieux ordonnés et sont stockés dans un répertoire en fonction de leur environnement qu'ils définissent.
    /// 
    /// Prérequis :
    /// - Installer Newtonsoft.Json, System.Extensions.Configuration.Json et Microsoft.Extensions.Configuration.Binder via NuGet.
    ///     Pour la version light de ce service seulement Newtonsoft.Json est requis mais elle ne permet pas d'utiliser la racine de configuration pour utiliser le fichier de configuration tel quel sans classe de mappage.
    /// - Créer le fichier Configurations/environments.json à la racine du projet qui définira l'environnement par défault et courant.
    ///     Exemple : { "Default": "Development", "Current": "Test"}
    /// - Trier vos fichiers de configurations par environnement, exemple :
    ///     - Configurations/Development/configurationSample.json
    ///     - Configurations/Test/configurationSample.json
    public abstract class AConfigurationService : ALightConfigurationService, IConfigurationService
    {
        #region Constructor(s)
        public AConfigurationService(IMergedConfigurationBuilder mergedConfigurationBuilder, string directory = "Configurations") : base(mergedConfigurationBuilder, directory) { }
        #endregion

        #region Public Behaviour(s)
        /// <summary>
        /// La racine de configuration permet d'éviter de définir une classe de mappage de votre fichier de configuration pour utiliser votre fichier de configuration tel quel et donc très rapidement.
        /// </summary>
        public IConfigurationRoot ToRoot(string filename)
        {
            var mergedConfiguration = MergedConfigurationBuilder.Build(this, filename);

            File.WriteAllText(mergedConfiguration.path, mergedConfiguration.content);

            return AddFile(new ConfigurationBuilder(), mergedConfiguration.path).Build();
        }
        #endregion

        #region Abstract Behaviour(s)
        protected abstract IConfigurationBuilder AddFile(IConfigurationBuilder builder, string path);
        #endregion
    }
}