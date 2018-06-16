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

        protected override (string path, string content) GenerateMergedConfiguration(string configurationFilename)
        {
            var configurationPaths = GenerateConfigurationPaths(configurationFilename);

            var defaultConfigurationFileContent = File.ReadAllText(configurationPaths.@default);

            if (configurationPaths.@default == configurationPaths.current)
                return (configurationPaths.@default, defaultConfigurationFileContent);

            var currentConfigurationFileContent = File.ReadAllText(configurationPaths.current);
            var defaultConfigurationJObject = JObject.Parse(defaultConfigurationFileContent);
            var currentConfigurationJObject = JObject.Parse(currentConfigurationFileContent);

            defaultConfigurationJObject.Merge(currentConfigurationJObject, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union });

            (string path, string content) mergedConfiguration = (configurationPaths.destination, defaultConfigurationJObject.ToString());

            File.WriteAllText(mergedConfiguration.path, mergedConfiguration.content);

            return mergedConfiguration;
        }
        #endregion
    }
}