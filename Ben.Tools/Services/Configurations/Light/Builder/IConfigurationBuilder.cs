using BenTools.Services.Configurations.Options;

namespace BenTools.Services.Configurations.Builder
{
    public interface IConfigurationBuilder
    {
        ConfigurationBuildResult Build(IConfigurationOptions configurationOptions, string filename, string extension, params string[] subSections);
        ConfigurationBuildResult Build(string defaultFileContent, string currentFileContent, string destinationPath);
        SectionType Deserialize<SectionType>(string fileContent);
        string BuildRawSection(string fileContent, params string[] subSections);
    }
}