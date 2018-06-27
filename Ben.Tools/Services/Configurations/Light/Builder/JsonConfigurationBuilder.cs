using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BenTools.Services.Configurations.Light.Builder
{
    public class JsonConfigurationBuilder : AConfigurationBuilder
    {
        #region Override Behaviour(s)
        public override ConfigurationBuildResult MergeConfiguration(string defaultFileContent, string currentFileContent, string destinationPath)
        {
            var defaultJObject = JObject.Parse(defaultFileContent);
            var currentJObject = JObject.Parse(currentFileContent);

            defaultJObject.Merge(currentJObject, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union });

            return new ConfigurationBuildResult(destinationPath, defaultJObject.ToString());
        }

        public override string ToRawSection(string fileContent, IEnumerable<string> subSections)
        {
            if (subSections == null || !subSections.Any())
                return fileContent;

            var jObject = JObject.Parse(fileContent);

            foreach (var subSection in subSections)
                jObject = (JObject)jObject[subSection];

            return jObject.ToString();
        }

        public override SectionType Deserialize<SectionType>(string fileContent) =>
            JsonConvert.DeserializeObject<SectionType>(fileContent);
        #endregion
    }
}