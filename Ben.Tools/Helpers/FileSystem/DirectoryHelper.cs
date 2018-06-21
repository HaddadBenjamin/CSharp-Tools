using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BenTools.Helpers.FileSystem
{
    public static class DirectoryHelper
    {
        public static void Reset(string directory, bool deleteRecursively = true)
        {
            if (Directory.Exists(directory))
                Directory.Delete(directory, deleteRecursively);

            Directory.CreateDirectory(directory);
        }

        public static IEnumerable<string> GetSubDirectories(string directory, bool fullPath = true) =>
            new DirectoryInfo(directory)
                .EnumerateDirectories()
                .Select(sampleDirectory => fullPath ? sampleDirectory.FullName : sampleDirectory.Name);
    }
}
