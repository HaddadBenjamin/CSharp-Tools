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
        public void RotateAndScale()
        {
            var imageRecognition = new ForgeImageRecognitionService();
            var testBitmaps = new[]
            {
                Properties.Resources.imageRecognitionTest,
                Properties.Resources.imageRecognitionTest2,
                Properties.Resources.imageRecognitionTest3
            };

            using (var sourceBitmap = Properties.Resources.imageRecognitionSource)
            {
                var index = 1;
                foreach (var testBitmap in testBitmaps)
                {
                    using (var destinationImage = (Image)sourceBitmap.Clone())
                    using (var newImage = new Bitmap(destinationImage))
                    using (var graphics = Graphics.FromImage(newImage))
                    {
                        var resultImagePath = Path.Combine(Path.GetTempPath(), $"image_recognition_result_{index}.jpg");
                        var firstMatch = imageRecognition.FindMatches(
                                sourceBitmap: sourceBitmap,
                                testBitmap: testBitmap,
                                precision: 0.95d,
                                scale: 0.25d,
                                stopAtFirstMatch: true,
                                blackAndWhite: true)
                            .FirstOrDefault();

                        graphics.DrawRectangle(Pens.Aqua, firstMatch);
                        newImage.Save(resultImagePath, ImageFormat.Png);

                        Process.Start(resultImagePath);
                    }

                    testBitmap.Dispose();

                    index++;
                }
            }
        }
    }
}
