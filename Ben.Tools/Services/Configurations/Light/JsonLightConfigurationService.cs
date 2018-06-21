using BenTools.Services.Configurations.Builder;
using BenTools.Services.Configurations.Options;

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