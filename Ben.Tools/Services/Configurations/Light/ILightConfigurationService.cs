using System.Collections.Generic;

namespace BenTools.Services.Configurations.Light
{
    public interface ILightConfigurationService
    {
        SectionType ToClass<SectionType>(string filename, IEnumerable<string> subSection);
    }
}