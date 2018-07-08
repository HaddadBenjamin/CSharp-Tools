using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace BenTools.Extensions.Streams
{
    public static class BitmapExtension
    {
        public static Bitmap UpdatePixelFormat(this Bitmap bitmap, PixelFormat pixelFormat, bool disposeBitmap = true)
        {
            var newBitmap = new Bitmap(bitmap.Width, bitmap.Height, pixelFormat);
            newBitmap.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);

            using (var graphics = Graphics.FromImage(newBitmap))
                graphics.DrawImage(bitmap, 0, 0);

            if (disposeBitmap)
                bitmap.Dispose();

            return newBitmap;
        }

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

        public static Rectangle GetRectangle(this Bitmap bitmap) =>  new Rectangle(0, 0, bitmap.Width, bitmap.Height);

        public static MemoryStream ToMemoryStream(this Bitmap bitmap, ImageFormat imageFormat)
        {
            var memoryStream = new MemoryStream();

            bitmap.Save(memoryStream, imageFormat);

            return memoryStream;
        }

        public static MemoryStream ToMemoryStream(this Bitmap bitmap) => bitmap.ToMemoryStream(ImageFormat.Png);

        public static byte[] ToBytes(this Bitmap bitmap, ImageFormat imageFormat)
        {
            using (var memoryStream = bitmap.ToMemoryStream(imageFormat))
                return memoryStream.ToArray();
        }

        public static byte[] ToBytes(this Bitmap bitmap) => bitmap.ToBytes(ImageFormat.Png);

        /// <summary>
        ///     Affiche votre bitmap à l'intérieur d'un rectangle blanc.
        ///     Ce procédé est souvent utilisé pour s'assurer qu'une image ne dépasse pas une certaine taille.
        /// </summary>
        public static Bitmap ToSquareImage(this Bitmap originalBitmap)
        {
            var largestDimension = Math.Max(originalBitmap.Height, originalBitmap.Width);
            var squareSize = new Size(largestDimension, largestDimension);
            var squareImage = new Bitmap(squareSize.Width, squareSize.Height);

            using (var graphics = Graphics.FromImage(squareImage))
            {
                graphics.FillRectangle(Brushes.White, 0, 0, squareSize.Width, squareSize.Height);

                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                graphics.DrawImage(
                    originalBitmap,
                    squareSize.Width / 2 - originalBitmap.Width / 2,
                    squareSize.Height / 2 - originalBitmap.Height / 2,
                    originalBitmap.Width, originalBitmap.Height);
            }

            return squareImage;
        }
    }
}
