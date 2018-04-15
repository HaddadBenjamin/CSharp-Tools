using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Ben.Tools.Extensions.Streams
{
    public static class ImageExtension
    {
        public static Image SquareImage(this Image originalImage)
        {
            var largestDimension = Math.Max(originalImage.Height, originalImage.Width);
            var squareSize = new Size(largestDimension, largestDimension);
            var squareImage = new Bitmap(squareSize.Width, squareSize.Height);

            using (var graphics = Graphics.FromImage(squareImage))
            {
                graphics.FillRectangle(Brushes.White, 0, 0, squareSize.Width, squareSize.Height);
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                graphics.DrawImage(
                    originalImage,
                    squareSize.Width / 2 - originalImage.Width / 2,
                    squareSize.Height / 2 - originalImage.Height / 2,
                    originalImage.Width, originalImage.Height);
            }

            return squareImage;
        }
    }
}