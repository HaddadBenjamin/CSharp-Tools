using BenTools.Services.Configurations.Options;

namespace BenTools.Services.Configurations.Builder
{
    public interface IConfigurationBuilder
    {
        (string path, string content) Build(IConfigurationOptions configurationOptions, string filename, string extension);
        (string path, string content) Build(string defaultFileContent, string currentFileContent, string destinationPath);
        SectionType Deserialize<SectionType>(string fileContent);
    }
}