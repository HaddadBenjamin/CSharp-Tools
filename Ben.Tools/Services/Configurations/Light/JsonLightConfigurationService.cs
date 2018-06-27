using BenTools.Services.Configurations.Light.Builder;
using BenTools.Services.Configurations.Light.Options;

namespace BenTools.Services.Configurations.Light
{
    public class JsonLightConfigurationService : ALightConfigurationService
    {
        #region Constructor(s)
        public JsonLightConfigurationService() : base(new JsonConfigurationBuilder(), new ConfigurationOptions()) { }

        public JsonLightConfigurationService(IConfigurationOptions configurationOptions) : base(new JsonConfigurationBuilder(), configurationOptions) { }
        #endregion

        #region Override Behaviour(s)
        public override string Extension => ".json";
        #endregion      
    }
}