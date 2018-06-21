namespace BenTools.Services.Configurations.Light
{
    public interface ILightConfigurationService
    {
        SectionType ToClass<SectionType>(string filename);
    }
}