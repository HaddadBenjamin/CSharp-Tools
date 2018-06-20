using System.IO;
using Newtonsoft.Json.Linq;

namespace Ben.Tools.Services.Configurations
{
    public class JsonLightConfigurationService : ALightConfigurationService
    {
        #region Constructor(s)
        public JsonLightConfigurationService(string directory = "Configurations", bool mergeConfiguration = true, string forcedCurrentEnvironment = null) : base(new JsonConfigurationBuilder(), directory, mergeConfiguration, forcedCurrentEnvironment)
        {
        }
        #endregion

        #region Override Behaviour(s)
        public override string Extension => ".json";
        #endregion      
    }
}