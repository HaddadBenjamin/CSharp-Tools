using System.Collections.Generic;
using System.Drawing;

namespace Ben.Tools.Development.Services
{
    public class ImageRecognitionMatch
    {
        public int Index { get; set; }
        public IEnumerable<Rectangle> Matches { get; set; }
    }
}