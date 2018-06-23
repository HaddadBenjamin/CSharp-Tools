using BenTools.Services.Configurations.Options;

namespace BenTools.Services.Configurations.Builder
{
    public interface IConfigurationBuilder
    {
        ConfigurationBuildResult BuildConfiguration(IConfigurationOptions configurationOptions, string filename, string extension, params string[] subSections);
        ConfigurationBuildResult MergeConfiguration(string defaultFileContent, string currentFileContent, string destinationPath);
        string ToRawSection(string fileContent, params string[] subSections);
        SectionType Deserialize<SectionType>(string fileContent);
    }
}