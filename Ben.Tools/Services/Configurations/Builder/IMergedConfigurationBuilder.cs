namespace Ben.Tools.Services.Configurations
{
    public interface IMergedConfigurationBuilder
    {
        (string path, string content) Build(ALightConfigurationService configurationService, string filename);
    }
}