using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Ben.Tools.Development
{
    public abstract class AImageRecognitionService : IImageRecognitionService
    {
        protected Bitmap UpdateBitmap(Bitmap bitmap, double scale, bool blackAndWhite) => 
            bitmap.ToBlackAndWhite()
                  .Resize(scale);

        public abstract IEnumerable<Rectangle> FindMatches(
            Bitmap sourceBitmap,
            Bitmap testBitmap,
            double precision,
            double scale,
            bool stopAtFirstMatch,
            bool blackAndWhite);

        public IEnumerable<Rectangle> FindMatches(
            string sourceImagePath, 
            string testImagePath, 
            double precision, 
            double scale,
            bool stopAtFirstMatch,
            bool blackAndWhite)
        {
            using (var sourceBitmap = new Bitmap(sourceImagePath))
            using (var testBitmap = new Bitmap(testImagePath))
                return FindMatches(sourceBitmap, testBitmap, precision, scale, stopAtFirstMatch, blackAndWhite);
        }

        public IEnumerable<ImageRecognitionMatch> FindMatches(
            Bitmap sourceBitmap, 
            IEnumerable<Bitmap> testBitmaps, 
            double precision, 
            double scale,
            bool stopAtFirstMatch, 
            bool blackAndWhite) =>
                testBitmaps.Select((testBitmap, index) => new ImageRecognitionMatch()
                {
                    Index = index,
                    Matches = FindMatches(sourceBitmap, testBitmap, precision, scale, stopAtFirstMatch, blackAndWhite)
                });

        public IEnumerable<ImageRecognitionMatch> FindMatches(
            string sourceImagePath,
            IEnumerable<string> testImagePaths,
            double precision,
            double scale,
            bool stopAtFirstMatch,
            bool blackAndWhite)
        {
            var sourceBitmap = new Bitmap(sourceImagePath);
            var testBitmaps = testImagePaths.Select(testImagePath => new Bitmap(testImagePath));

            try
            {
                return FindMatches(sourceBitmap, testBitmaps, precision, scale, stopAtFirstMatch, blackAndWhite);
            }
            finally
            {
                sourceBitmap.Dispose();

                foreach (var testBitmap in testBitmaps)
                    testBitmap.Dispose();
            }
        }

        public Rectangle FindFirstMatch(
            Bitmap sourceBitmap, 
            Bitmap testBitmap, 
            double precision, 
            double scale, 
            bool blackAndWhite) =>
                FindMatches(sourceBitmap, testBitmap, precision, scale, true, blackAndWhite).FirstOrDefault();

        public Rectangle FindFirstMatch(
            string sourceImagePath, 
            string testImagePath, 
            double precision, 
            double scale,
            bool blackAndWhite) =>
                FindMatches(sourceImagePath, testImagePath, precision, scale, true, blackAndWhite).FirstOrDefault();

        public void DrawMatches(
            Bitmap sourceBitmap, 
            IEnumerable<Bitmap> testBitmaps, 
            double precision, 
            double scale,
            bool stopAtFirstMatch, 
            bool blackAndWhite, 
            bool displayMatches)
        {
            var index = 1;
            foreach (var testBitmap in testBitmaps)
            {
                using (var destinationImage = (Image)sourceBitmap.Clone())
                using (var newImage = new Bitmap(destinationImage))
                using (var graphics = Graphics.FromImage(newImage))
                {
                    var resultImagePath = Path.Combine(Path.GetTempPath(), $"image_recognition_result_{index}.jpg");
                    var firstMatch = FindMatches(sourceBitmap, testBitmap, precision, scale, stopAtFirstMatch, blackAndWhite)
                                                .FirstOrDefault();

                    graphics.DrawRectangle(Pens.Red, firstMatch);
                    newImage.Save(resultImagePath, ImageFormat.Jpeg);

                    if (displayMatches)
                        Process.Start(resultImagePath);
                }

                testBitmap.Dispose();

                index++;
            }
        }
    }
}