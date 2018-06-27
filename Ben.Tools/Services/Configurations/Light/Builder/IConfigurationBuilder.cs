using System.Collections.Generic;
using BenTools.Services.Configurations.Options;

namespace BenTools.Services.Configurations.Builder
{
    public interface IConfigurationBuilder
    {
        ConfigurationBuildResult BuildConfiguration(IConfigurationOptions configurationOptions, string filename, string extension, IEnumerable<string> subSections);
        SectionType Deserialize<SectionType>(string fileContent);
    }
}