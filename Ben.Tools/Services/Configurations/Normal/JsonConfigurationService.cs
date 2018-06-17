using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Ben.Tools.Services.Configurations
{
    public class JsonConfigurationService : AConfigurationService
    {
        #region Constructor(s)
        public JsonConfigurationService(string directory = "Configurations") : base(new JsonMergedConfigurationBuilder(), directory)
        {
        }
        #endregion

        #region Override Behaviour(s)
        public override string Extension => ".json";

        protected override IConfigurationBuilder AddFile(IConfigurationBuilder builder, string path) =>
            builder.AddJsonFile(path);
        #endregion
    }
}