using BenTools.Services.Configurations.Builder;
using BenTools.Services.Configurations.Options;
using Microsoft.Extensions.Configuration;

namespace BenTools.Services.Configurations.Normal
{
    public class JsonConfigurationService : AConfigurationService
    {
        #region Constructor(s)
        public JsonConfigurationService() : base(new JsonConfigurationBuilder(), new ConfigurationOptions()) { }

        public JsonConfigurationService(IConfigurationOptions options) : base(new JsonConfigurationBuilder(), options) { }
        #endregion

        #region Override Behaviour(s)
        public override string Extension => ".json";

        protected override Microsoft.Extensions.Configuration.IConfigurationBuilder AddFile(Microsoft.Extensions.Configuration.IConfigurationBuilder builder, string path) =>
            builder.AddJsonFile(path);
        #endregion
    }
}