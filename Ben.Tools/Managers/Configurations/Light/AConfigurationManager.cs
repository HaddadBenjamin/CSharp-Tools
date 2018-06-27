using System.Collections.Generic;
using System.Linq;
using BenTools.Managers.Configurations.Options;
using BenTools.Services.Configurations.Light;
using BenTools.Services.Configurations.Light.Options;

namespace BenTools.Managers.Configurations.Light
{
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
                configurationDirectory: configurationManagerOption.ConfigurationDirectory,
                mergeConfigurationFiles:configurationManagerOption.MergeConfigurationFiles,
                environmentDirectory:configurationManagerOption.EnvironmentDirectory);

            _configurationService.RefreshOptions(configurationOption);

            return _configurationService.ToClass<ConfigurationClass>(configurationManagerOption.ConfigurationFilename, configurationManagerOption.SubSections);
        }
        #endregion
    }
}
