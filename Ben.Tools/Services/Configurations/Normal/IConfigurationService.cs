using Microsoft.Extensions.Configuration;

namespace Ben.Tools.Services.Configurations
{
    public interface IConfigurationService : ILightConfigurationService
    {
        IConfigurationRoot ToRoot(string filename);
    }
}