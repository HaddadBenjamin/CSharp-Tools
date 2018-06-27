using System;
using System.Collections.Generic;
using System.Linq;
using BenTools.Services.Configurations.Light;
using BenTools.Services.Configurations.Options;

namespace BenTools.Managers.Configurations
{
    public interface IConfigurationManagerOptions
    {
        IEnumerable<string> SubSections { get; set; }
        string ConfigurationFilename { get; set; }
        string ConfigurationKey { get; set; }
        string EnvironmentDirectory { get; set; }
        bool MergeConfigurationFiles { get; set; }
    }

    public class ConfigurationManagerOptions : IConfigurationManagerOptions
    {
        #region Field(s)
        public IEnumerable<string> SubSections { get; set; }
        public string ConfigurationFilename { get; set; }
        public string ConfigurationKey { get; set; }
        public string EnvironmentDirectory { get; set; } = "Configurations";
        public bool MergeConfigurationFiles { get; set; } = true;
        #endregion

        #region Constructor(s)
        public ConfigurationManagerOptions(string configurationFilename)
        {
            if (string.IsNullOrWhiteSpace(configurationFilename))
                throw new ArgumentException(nameof(configurationFilename));

            ConfigurationFilename = configurationFilename;
        }
        #endregion
    }

    // Intérêts de ce service ; gérer tous type de configuration merge ou non etc...
    public interface IConfigurationManager
    {
        ConfigurationClass GetConfigurationClass<ConfigurationClass>(string configurationKey);
    }

    /// <summary>
    /// Permet d'enregistrer vos options de configurations et d'accéder à leurs fichiers de façons normalisées ce qui vous évitera de rajouter du code doublon.
    /// </summary>
    public  abstract class AConfigurationManager<ConfigurationService> : IConfigurationManager
            where ConfigurationService : ALightConfigurationService, new()
    {
        #region Field(s)
        private readonly ConfigurationService _configurationService = new ConfigurationService();
        private readonly Dictionary<string, ConfigurationManagerOptions> _configurationOptions;
        #endregion

        #region Constructor(s)
        public AConfigurationManager(params ConfigurationManagerOptions[] options)
        {
            _configurationOptions = options.ToDictionary(element => element.ConfigurationKey ?? element.ConfigurationFilename,
                                                         element => element);
        }
        #endregion

        #region Public Behaviour(s)
        public ConfigurationClass GetConfigurationClass<ConfigurationClass>(string configurationKey)
        {
            var configurationManagerOption = _configurationOptions[configurationKey];
            var configurationOption = new ConfigurationOptions(
                configurationManagerOption.EnvironmentDirectory,
                configurationManagerOption.MergeConfigurationFiles,
                configurationManagerOption.EnvironmentDirectory);

            _configurationService.RefreshOptions(configurationOption);

            return _configurationService.ToClass<ConfigurationClass>(configurationManagerOption.ConfigurationFilename, configurationManagerOption.SubSections);
        }
        #endregion
    }

    public abstract class JsonConfigurationManager : AConfigurationManager<JsonLightConfigurationService>
    {
    }
}
