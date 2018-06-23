using System.IO;
using BenTools.Services.Configurations.Light;
using BenTools.Services.Configurations.Options;
using Microsoft.Extensions.Configuration;
using IConfigurationBuilder = BenTools.Services.Configurations.Builder.IConfigurationBuilder;

namespace BenTools.Services.Configurations.Normal
{
    /// <summary>
    /// Cette version permet d'utiliser la racine de configuration pour utiliser votre fichier de configuration sans classe de mappage mais nécéssite 2 dépendances supplémentaires lourdes.
    public abstract class AConfigurationService : ALightConfigurationService, IConfigurationService
    {
        #region Constructor(s)
        public AConfigurationService(IConfigurationBuilder builder, IConfigurationOptions options) : base(builder, options) { }
        #endregion

        #region Public Behaviour(s)
        /// <summary>
        /// La racine de configuration permet d'éviter de définir une classe de mappage de votre fichier de configuration pour utiliser votre fichier de configuration tel quel et donc très rapidement.
        /// </summary>
        public IConfigurationRoot ToConfigurationRoot(string filename)
        {
            var mergedConfiguration = Builder.BuildConfiguration(Options, filename, Extension, null);

            File.WriteAllText(mergedConfiguration.FilePath, mergedConfiguration.FileContent);

            return AddFile(new ConfigurationBuilder(), mergedConfiguration.FilePath).Build();
        }
        #endregion

        #region Abstract Behaviour(s)
        protected abstract Microsoft.Extensions.Configuration.IConfigurationBuilder AddFile(Microsoft.Extensions.Configuration.IConfigurationBuilder builder, string path);
        #endregion
    }
}