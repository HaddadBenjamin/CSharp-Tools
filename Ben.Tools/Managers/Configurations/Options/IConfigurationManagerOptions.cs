using System.Collections.Generic;

namespace BenTools.Managers.Configurations.Options
{
    public interface IConfigurationManagerOptions
    {
        IEnumerable<string> SubSections { get; set; }
        string ConfigurationDirectory { get; set; }
        string ConfigurationFilename { get; set; }
        string ConfigurationKey { get; set; }
        string EnvironmentDirectory { get; set; }
        bool MergeConfigurationFiles { get; set; }
    }
}