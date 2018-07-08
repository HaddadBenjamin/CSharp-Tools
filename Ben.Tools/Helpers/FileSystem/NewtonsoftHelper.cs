using Newtonsoft.Json.Linq;

namespace BenTools.Helpers.FileSystem
{
    public static class NewtonsoftHelper
    {
        public static string MergeJson(string leftRawJson, string rightRawJson)
        {
            var leftJObject = JObject.Parse(leftRawJson);
            var rightJObject = JObject.Parse(rightRawJson);

            leftJObject.Merge(rightJObject, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union });

            return leftJObject.ToString();
        }

        public static string ToRawJsonSection(string rawJson, params string[] subSections)
        {
            var jObject = JObject.Parse(rawJson);

            foreach (var subSection in subSections)
                jObject = (JObject)jObject[subSection];

            return jObject.ToString();
        }
    }
}