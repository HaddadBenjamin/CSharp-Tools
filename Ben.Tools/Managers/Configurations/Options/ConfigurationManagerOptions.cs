using System;
using System.Collections.Generic;

namespace BenTools.Managers.Configurations.Options
{
    public class ConfigurationManagerOptions : IConfigurationManagerOptions
    {
        #region Field(s)
        public IEnumerable<string> SubSections { get; set; }
        public string ConfigurationDirectory { get; set; } = "Configurations";
        public string ConfigurationFilename { get; set; }
        public string ConfigurationKey { get; set; }
        public string EnvironmentDirectory { get; set; } = null;
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
}