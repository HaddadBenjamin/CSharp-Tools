using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using NUnit.Framework;

namespace Ben.Tools.Development
{
    [TestFixture]
    public class ImageRecognitionTests
    {
        [TestCase(@"C:\Users\hadda\Desktop\Benjamin HADDAD (5).jpg", @"C:\Users\hadda\Desktop\Profile2.jpg", @"C:\Users\hadda\Desktop\imageRecognitionResult.jpeg")]
        public void RotateAndScale(string sourceImagePath, string testImagePath, string destinationImagePath)
        {
            var imageRecognition = new ForgeImageRecognitionService();
            var firstMatch = imageRecognition.FindMatches(sourceImagePath, testImagePath)
                                             .FirstOrDefault();

            using (var sourceImage = new Bitmap(sourceImagePath))
            using (var destinationImage = (Image)sourceImage.Clone())
            using (var newImage = new Bitmap(destinationImage))
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawRectangle(Pens.Aqua, new Rectangle((int)firstMatch.X, (int)firstMatch.Y, (int)firstMatch.Width, (int)firstMatch.Height));
                newImage.Save(destinationImagePath, ImageFormat.Jpeg);
            }
        }
    }
}
