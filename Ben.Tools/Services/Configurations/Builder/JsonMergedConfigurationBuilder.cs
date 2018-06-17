using System.IO;
using Newtonsoft.Json.Linq;

namespace Ben.Tools.Services.Configurations
{
    public class JsonMergedConfigurationBuilder : IMergedConfigurationBuilder
    {
        public (string path, string content) Build(ALightConfigurationService configurationService, string filename)
        {
            var configurationPaths = configurationService.BuildPaths(configurationService, filename);
            var defaultFileContent = File.ReadAllText(configurationPaths.@default);

            if (configurationPaths.@default == configurationPaths.current)
                return (configurationPaths.@default, defaultFileContent);

            var currentFileContent = File.ReadAllText(configurationPaths.current);
            var defaultJObject = JObject.Parse(defaultFileContent);
            var currentJObject = JObject.Parse(currentFileContent);

            defaultJObject.Merge(currentJObject, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union });

            return (configurationPaths.destination, defaultJObject.ToString());
        }
    }
}