using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Ben.Tools.Development
{
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