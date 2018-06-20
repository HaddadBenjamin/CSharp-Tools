using System;
using System.IO;
using System.Reflection;
using Ben.Tools.Services.Configurations;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Ben.Tools.Services.Configurations
{
    /// <summary>
    /// Cette version permet d'utiliser la racine de configuration pour utiliser votre fichier de configuration sans classe de mappage mais nécéssite 2 dépendances supplémentaires lourdes.
    public abstract class AConfigurationService : ALightConfigurationService, IConfigurationService
    {
        #region Constructor(s)
        public AConfigurationService(IConfigurationBuilder configurationBuilder, string directory = "Configurations", bool mergeConfiguration = true, string forcedCurrentEnvironment = null) : base(configurationBuilder, directory, mergeConfiguration, forcedCurrentEnvironment) { }
        #endregion

        #region Public Behaviour(s)
        /// <summary>
        /// La racine de configuration permet d'éviter de définir une classe de mappage de votre fichier de configuration pour utiliser votre fichier de configuration tel quel et donc très rapidement.
        /// </summary>
        public IConfigurationRoot ToRoot(string filename)
        {
            var mergedConfiguration = ConfigurationBuilder.Build(this, filename);

            File.WriteAllText(mergedConfiguration.path, mergedConfiguration.content);

            return AddFile(new ConfigurationBuilder(), mergedConfiguration.path).Build();
        }
        #endregion

        #region Abstract Behaviour(s)
        protected abstract Microsoft.Extensions.Configuration.IConfigurationBuilder AddFile(Microsoft.Extensions.Configuration.IConfigurationBuilder builder, string path);
        #endregion
    }
}