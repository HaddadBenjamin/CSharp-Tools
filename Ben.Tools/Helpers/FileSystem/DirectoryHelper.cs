using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BenTools.Helpers.FileSystem
{
    public static class DirectoryHelper
    {
        public static IEnumerable<string> GetSubDirectories(string directory, bool fullPath = true) =>
            new DirectoryInfo(directory).EnumerateDirectories().Select(sampleDirectory => fullPath ? sampleDirectory.FullName : sampleDirectory.Name);
    }
}
