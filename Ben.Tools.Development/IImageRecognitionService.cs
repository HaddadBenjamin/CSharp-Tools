using System.Collections.Generic;
using Ben.Tools.Development.Services.Models;

namespace Ben.Tools.Development
{
    public interface IImageRecognitionService
    {
        //IEnumerable<ImageRecognitionMatch> FindMatches(
        //    Bitmap sourceBitmap,
        //    Bitmap testBitmap,
        //    double precision,
        //    double scale,
        //    bool stopAtFirstMatch,
        //    bool blackAndWhite);

        IEnumerable<ImageRecognitionMatch> FindMatches(
            string sourcePath,
            string testPath,
            double precision,
            double scale,
            bool stopAtFirstMatch,
            bool blackAndWhite);
    }
}
