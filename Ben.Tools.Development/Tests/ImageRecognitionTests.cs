using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Ben.Tools.Development
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
                Properties.Resources.imageRecognitionTest,
                Properties.Resources.imageRecognitionRotateTest,
                Properties.Resources.imageRecognitionScaleTest
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
