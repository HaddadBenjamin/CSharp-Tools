using System;
using System.Drawing;

namespace BenTools.Helpers
{
    public static class ColorHelper
    {
        public static Random Random = new Random();

        public static Color GenerateColor() => Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256));
    }
}