using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BenTools.Services.Configurations.Builder
{
    public class JsonConfigurationBuilder : AConfigurationBuilder
    {
        #region Override Behaviour(s)
        public override (string path, string content) Build(string defaultFileContent, string currentFileContent, string destinationPath)
        {
            var defaultJObject = JObject.Parse(defaultFileContent);
            var currentJObject = JObject.Parse(currentFileContent);

            defaultJObject.Merge(currentJObject, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union });

            return (destinationPath, defaultJObject.ToString());
        }

        public override SectionType Deserialize<SectionType>(string fileContent) =>
            JsonConvert.DeserializeObject<SectionType>(fileContent);
        #endregion
    }
}