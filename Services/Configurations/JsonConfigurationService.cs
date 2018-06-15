using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Ben.Tools.Services.Configurations
{
    public class JsonConfigurationService : AConfigurationService
    {
        #region Override Behaviour(s)
        protected override string ConfigurationExtension => ".json";

        protected override IConfigurationBuilder AddConfigurationFile(IConfigurationBuilder builder, string configurationPath) =>
            builder.AddJsonFile(configurationPath);

        protected override string ToMergedConfigurationFileContent(string currentEnvironmentPath, string defaultEnvironmentPath)
        {
            var currentEnvironmentJObject = JObject.Parse(File.ReadAllText(currentEnvironmentPath));
            var defaultEnvironmentJObject = JObject.Parse(File.ReadAllText(defaultEnvironmentPath));

            defaultEnvironmentJObject.Merge(currentEnvironmentJObject, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union });

            return defaultEnvironmentJObject.ToString();
        }
        #endregion
    }
}