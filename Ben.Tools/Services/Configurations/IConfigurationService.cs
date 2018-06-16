using Microsoft.Extensions.Configuration;

namespace Ben.Tools.Services.Configurations
{
    public interface IConfigurationService
    {
        IConfigurationRoot ToConfigurationRoot(string configurationFilename);
        SectionType ToClass<SectionType>(string configurationFilename);
    }
}