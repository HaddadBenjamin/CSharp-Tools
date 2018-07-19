using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using AForge.Imaging;

namespace Ben.Tools.Development
{
    /// <summary>
    /// Don't work correctly when the scale or the rotation are different.
    /// </summary>
    public class ForgeImageRecognitionService : AImageRecognitionService
    {
        public override IEnumerable<Rectangle> FindMatches(
            Bitmap sourceBitmap, 
            Bitmap testBitmap, 
            double precision, 
            double scale, 
            bool stopAtFirstMatch,
            bool blackAndWhite)
        {
            using (var updatedSource = UpdateBitmap(sourceBitmap, scale, blackAndWhite))
            using (var updatedTest = UpdateBitmap(testBitmap, scale, blackAndWhite))
                return new ExhaustiveTemplateMatching(Convert.ToSingle(precision))
                        .ProcessImage(sourceBitmap, testBitmap)
                        .Select(match => match.Rectangle);
       
            // stopAtFirst (reprendre l'algorithme).
        }
    }

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
    }

    public static class BitmapExtension
    {
        public static Bitmap ToBlackAndWhite(this Bitmap bitmap)
        {
            for (var pixelColumn = 0; pixelColumn < bitmap.Height; pixelColumn++)
            for (var pixelLine = 0; pixelLine < bitmap.Width; pixelLine++)
            {
                var pixelColor = bitmap.GetPixel(pixelLine, pixelColumn);
                var rgbRatio = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                bitmap.SetPixel(pixelLine, pixelColumn, Color.FromArgb(rgbRatio, rgbRatio, rgbRatio));
            }

            return bitmap;
        }

        public static Bitmap Resize(this Bitmap bitmap, double scaleRatio, bool highQuality = true) =>
            bitmap.Resize(Convert.ToInt32(bitmap.Width * scaleRatio), Convert.ToInt32(bitmap.Height * scaleRatio), highQuality);

        public static Bitmap Resize(this Bitmap bitmap, int width, int height, bool highQuality = true)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                if (highQuality)
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                }

                using (var imageAttributes = new ImageAttributes())
                {
                    imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttributes);
                }
            }

            return destImage;
        }
    }
}