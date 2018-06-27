namespace BenTools.Services.Configurations.Light.Options
{
    public class ConfigurationPathsResult
    {
        public readonly string Default;
        public readonly string Current;
        public readonly string Destination;

        public ConfigurationPathsResult(string @default, string current, string destination)
        {
            Default = @default;
            Current = current;
            Destination = destination;
        }
    }
}