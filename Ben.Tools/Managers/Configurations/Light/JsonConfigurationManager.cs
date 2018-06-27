using BenTools.Managers.Configurations.Options;
using BenTools.Services.Configurations.Light;

namespace BenTools.Managers.Configurations.Light
{
    public class JsonConfigurationManager : AConfigurationManager<JsonLightConfigurationService>
    {
        public JsonConfigurationManager(params ConfigurationManagerOptions[] options) : base(options) { }
    }
}