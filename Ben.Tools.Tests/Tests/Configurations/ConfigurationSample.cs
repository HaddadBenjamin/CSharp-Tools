using Newtonsoft.Json;

namespace BenTools.Tests.Tests.Configurations
{
    public class ConfigurationSample
    {
        public string String { get; set; }
        public int Number { get; set; }
        public bool Boolean { get; set; }
        public string Null { get; set; }
        public string[] Array { get; set; }
        public Class Class { get; set; }
        public SubSection SubSection { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string RequieredField { get; set; }

        [JsonProperty]
        public string PrivateSetter { get; private set; }

        public string FieldThatDontExistInDefaultConfiguration { get; set; }
        public string OverrideField { get; set; }
    }

    public class Class
    {
        public string String { get; set; }
    }

    public class NotMergedConfigurationSample
    {
        public string OverrideField { get; set; }
    }

    public class SubSection
    {
        public Class Class { get; set; }
    }
}