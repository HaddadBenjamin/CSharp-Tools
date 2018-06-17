using System.IO;
using Newtonsoft.Json.Linq;

namespace Ben.Tools.Services.Configurations
{
    public class JsonLightConfigurationService : ALightConfigurationService
    {
        #region Constructor(s)
        public JsonLightConfigurationService(string directory = "Configurations") : base(new JsonMergedConfigurationBuilder(), directory)
        {
        }
        #endregion

        #region Override Behaviour(s)
        public override string Extension => ".json";
        #endregion      
    }
}