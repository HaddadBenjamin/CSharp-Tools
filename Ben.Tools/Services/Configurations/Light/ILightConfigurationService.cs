namespace Ben.Tools.Services.Configurations
{
    public interface ILightConfigurationService
    {
        SectionType ToClass<SectionType>(string filename);
    }
}