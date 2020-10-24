using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AForge.Imaging;

namespace Ben.Tools.Development.Services
{
    /// <summary>
    /// Uses AForge.Net package.
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
                return new ExhaustiveTemplateMatching()
                    .ProcessImage(sourceBitmap, testBitmap)
                    .Where(matching => matching.Similarity >= Convert.ToSingle(precision))
                    .Select(match => match.Rectangle);

            // TODO : stopAtFirst (reprendre l'algorithme).
        }
    }
}