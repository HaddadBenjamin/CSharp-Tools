using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Flann;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace Ben.Tools.Development
{
    /// <summary>
    /// Ne fonctionne pas !!!
    /// </summary>
    public class ForgeImageRecognitionService : IImageRecognitionService
    {
        public IEnumerable<ImageRecognitionMatch> FindMatches(
            string sourcePath,
            string testPath,
            double precision = 0.985d,
            double scale = 0.2d,
            bool stopAtFirstMatch = true,
            bool blackAndWhite = false)
        {
            var matchesFound = new List<ImageRecognitionMatch>();

            try
            {
                long score;
                long matchTime;

                using (var modelImage = CvInvoke.Imread(sourcePath))
                using (var observedImage = CvInvoke.Imread(testPath))
                using (var matches = new VectorOfVectorOfDMatch())
                {
                    Mat homography = null;
                    VectorOfKeyPoint modelKeyPoints = new VectorOfKeyPoint();
                    VectorOfKeyPoint observedKeyPoints = new VectorOfKeyPoint();
                    Stopwatch watch;

                    using (var uModelImage = modelImage.GetUMat(AccessType.Read))
                    using (var uObservedImage = observedImage.GetUMat(AccessType.Read))
                    {
                        var featureDetector = new KAZE();
                        var modelDescriptors = new Mat();
                        var observedDescriptors = new Mat();

                        featureDetector.DetectAndCompute(uModelImage, null, modelKeyPoints, modelDescriptors, false);

                        watch = Stopwatch.StartNew();

                        featureDetector.DetectAndCompute(uObservedImage, null, observedKeyPoints, observedDescriptors, false);

                        using (var kdTreeIndexParams = new KdTreeIndexParams())
                        using (var searchParams = new SearchParams())
                        using (var flannBasedMatcher = new FlannBasedMatcher(kdTreeIndexParams, searchParams))
                        {
                            flannBasedMatcher.Add(modelDescriptors);
                            flannBasedMatcher.KnnMatch(observedDescriptors, matches, 2, null);

                            var mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);

                            mask.SetTo(new MCvScalar(255));

                            Features2DToolbox.VoteForUniqueness(matches, precision, mask);

                            score = 0;
                            for (var i = 0; i < matches.Size; i++)
                            {
                                if (mask.GetData(i)[0] == 0)
                                    continue;

                                foreach (var e in matches[i].ToArray())
                                    ++score;
                            }
                            // <----------------------------------------------

                            int nonZeroCount = CvInvoke.CountNonZero(mask);
                            if (nonZeroCount >= 4)
                            {
                                nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints, matches, mask, 1.5, 20);

                                if (nonZeroCount >= 4)
                                    homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints, observedKeyPoints, matches, mask, 2);
                            }

                            if (homography != null)
                            {
                                var rect = new Rectangle(Point.Empty, modelImage.Size);
                                var points = new[]
                                {
                                    new PointF(rect.Left, rect.Bottom),
                                    new PointF(rect.Right, rect.Bottom),
                                    new PointF(rect.Right, rect.Top),
                                    new PointF(rect.Left, rect.Top)
                                };

                                points = CvInvoke.PerspectiveTransform(points, homography);

                                float ClampPosition(float position) => position >= 0 ? position : 0;

                                var minX = ClampPosition(points.Min(point => point.X));
                                var maxX = ClampPosition(points.Max(point => point.X));
                                var minY = ClampPosition(points.Min(point => point.Y));
                                var maxY = ClampPosition(points.Max(point => point.Y));

                                matchesFound.Add(new ImageRecognitionMatch()
                                {
                                    TopLeftPoint = new ImageRecognitionPoint() { X = minX, Y = minY },
                                    TopRightPoint = new ImageRecognitionPoint() { X = maxX, Y = minY },
                                    BottomLeftPoint = new ImageRecognitionPoint() { X = maxX, Y = maxY },
                                    BottomRightPoint = new ImageRecognitionPoint() { X = maxX, Y = maxY },
                                });
                            }

                            watch.Stop();

                        }

                        matchTime = watch.ElapsedMilliseconds;
                    }
                }
            }
            catch
            {
            }

            return matchesFound;
        }
    }
}