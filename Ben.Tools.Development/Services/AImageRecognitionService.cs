using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace Ben.Tools.Development
{
    public abstract class AImageRecognitionService : IImageRecognitionService
    {
        protected Bitmap UpdateBitmap(Bitmap bitmap, double scale, bool blackAndWhite)
        {
            return bitmap.ToBlackAndWhite()
                         .Resize(scale);
        }

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
    }
}