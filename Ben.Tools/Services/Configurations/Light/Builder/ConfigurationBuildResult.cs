namespace BenTools.Services.Configurations.Builder
{
    public class ConfigurationBuildResult
    {
        public readonly string FilePath;
        public readonly string FileContent;

        public ConfigurationBuildResult(string filePath, string fileContent)
        {
            FilePath = filePath;
            FileContent = fileContent;
        }
    }
}