using System.Collections.Generic;
using System.Drawing;

namespace BenTools.Services.ImageRecognition
{
    public class ImageRecognitionMatch
    {
        public int Index { get; set; }
        public IEnumerable<Rectangle> Matches{ get; set; }
    }
}