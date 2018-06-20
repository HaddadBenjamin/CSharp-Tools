namespace Ben.Tools.Services.Configurations
{
    public interface IConfigurationBuilder
    {
        (string path, string content) Build(ALightConfigurationService configurationService, string filename);
        (string path, string content) Build(string defaultFileContent, string currentFileContent, string destinationPath);
        SectionType Deserialize<SectionType>(string fileContent);
    }
}