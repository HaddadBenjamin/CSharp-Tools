using BenTools.Services.Configurations.Light;
using Microsoft.Extensions.Configuration;

namespace BenTools.Services.Configurations.Normal
{
    public interface IConfigurationService : ILightConfigurationService
    {
        IConfigurationRoot ToRoot(string filename);
    }
}