namespace BenTools.Managers.Configurations.Light
{
    public interface IConfigurationManager
    {
        ConfigurationClass GetConfigurationClass<ConfigurationClass>(string configurationKey);
    }
}