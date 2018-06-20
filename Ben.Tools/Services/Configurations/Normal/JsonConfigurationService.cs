using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Ben.Tools.Services.Configurations
{
    public class JsonConfigurationService : AConfigurationService
    {
        #region Constructor(s)
        public JsonConfigurationService(string directory = "Configurations", bool mergeConfiguration = true, string forcedCurrentEnvironment = null) : base(new JsonConfigurationBuilder(), directory, mergeConfiguration, forcedCurrentEnvironment)
        {
        }
        #endregion

        #region Override Behaviour(s)
        public override string Extension => ".json";

        protected override Microsoft.Extensions.Configuration.IConfigurationBuilder AddFile(Microsoft.Extensions.Configuration.IConfigurationBuilder builder, string path) =>
            builder.AddJsonFile(path);
        #endregion
    }
}