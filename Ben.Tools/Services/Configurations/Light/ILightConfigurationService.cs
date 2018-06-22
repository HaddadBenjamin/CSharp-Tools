namespace BenTools.Services.Configurations.Light
{
    public interface ILightConfigurationService
    {
        SectionType ToClass<SectionType>(string filename, params string[] subSection);
    }
}