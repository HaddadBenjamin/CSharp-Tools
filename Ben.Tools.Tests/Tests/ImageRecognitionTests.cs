using BenTools.Services.ImageRecognition;
using NUnit.Framework;

namespace BenTools.Tests.Tests
{
    [TestFixture]
    public class ImageRecognitionTests
    {
        /// TODO:
        /// - installer le packet AForge.Net.
        /// - stop at first match
        [Test]
        public void ForgeImageRecognition()
        {
            var imageRecognitionService = new ForgeImageRecognitionService();
            var sourceBitmap = Properties.Resources.imageRecognitionSource;
            var testBitmaps = new[]
            {
                Properties.Resources.imageRecognitionCrop,
                Properties.Resources.imageRecognitionRotate,
                Properties.Resources.imageRecognitionScale
            };

            imageRecognitionService.DrawMatches(
                sourceBitmap: sourceBitmap,
                testBitmaps: testBitmaps,
                precision: 0.95d,
                scale: 0.25d,
                stopAtFirstMatch: true,
                blackAndWhite: true,
                displayMatches: true);

            sourceBitmap.Dispose();
            foreach (var testBitmap in testBitmaps)
                testBitmap.Dispose();
        }
    }
}
