using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Ben.Tools.Services;

namespace Ben.Tools.Helpers.Streams
{
    public static class ImageHelper
    {
         
        public static void DrawRectangle(
            Bitmap baseImageBitmap,
            string modifiedImagePath,
            Rectangle rectangle,
            Color color)
        {
            using (var baseImage = (Image)baseImageBitmap.Clone())
            using (var newImage = new Bitmap(baseImage))
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawRectangle(new Pen(color, 3), rectangle);

                newImage.Save(modifiedImagePath, ImageFormat.Png);
            }
        }

        public static void DrawRectangles(
            Bitmap baseImageBitmap,
            string modifiedImagePath,
            IEnumerable<Rectangle> rectangles,
            Bitmap bitmapToDraw)
        {
            using (Image baseImage = (Image)baseImageBitmap.Clone())
            using (Image newImage = new Bitmap(baseImage))
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                foreach (Rectangle rectangle in rectangles)
                    graphics.DrawRectangle(new Pen(RandomService.Instance.GenerateColor(), 3), rectangle);

                graphics.DrawImage(bitmapToDraw, 0, 0);

                newImage.Save(modifiedImagePath, ImageFormat.Png);
            }
        }

        public static Bitmap ResizeBitmap(
            Image image,
            int width,
            int height,
            bool highQuality = true)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

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

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        
    }
}