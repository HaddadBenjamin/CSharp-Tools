using System.Linq;
using NUnit.Framework;

namespace Ben.Tools.Development
{
    [TestFixture]
    public class ImageRecognitionTests
    {
        [Test]
        public void RotateAndScale()
        {
            var imageRecognition = new ForgeImageRecognitionService();

            var matches = imageRecognition.FindMatches(
                @"C:\Users\hadda\Desktop\Benjamin HADDAD (5).jpg",
                @"C:\Users\hadda\Desktop\Profile2.jpg",
                0.99d,
                1.0d,
                false,
                false);

            var middle = matches.First().MiddlePoint;
        }
    }
}
