using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Flann;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace Ben.Tools.Development
{
    public class ImageRecognitionPoint
    {
        public float PositionX { get; set; }
        public float PositionY { get; set; }
    }

    public class ImageRecognitionMatch
    {
        public ImageRecognitionPoint TopLeftPoint { get; set; }
        public ImageRecognitionPoint TopRightPoint { get; set; }
        public ImageRecognitionPoint BottomLeftPoint { get; set; }
        public ImageRecognitionPoint BottomRightPoint { get; set; }

        public ImageRecognitionPoint MiddlePoint => new ImageRecognitionPoint()
        {
            PositionX = (TopLeftPoint.PositionX + TopRightPoint.PositionX + BottomLeftPoint.PositionX + BottomRightPoint.PositionX) / 4,
            PositionY = (TopLeftPoint.PositionY + TopRightPoint.PositionY + BottomLeftPoint.PositionY + BottomRightPoint.PositionY) / 4,
        };
    }

    public interface IImageRecognition
    {
        //IEnumerable<ImageRecognitionMatch> FindMatches(
        //    Bitmap sourceBitmap,
        //    Bitmap testBitmap,
        //    double precision,
        //    double scale,
        //    bool stopAtFirstMatch,
        //    bool blackAndWhite);

        IEnumerable<ImageRecognitionMatch> FindMatches(
            string sourcePath,
            string testPath,
            double precision,
            double scale,
            bool stopAtFirstMatch,
            bool blackAndWhite);
    }

    public class ForgeImageRecognition : IImageRecognition
    {
        public IEnumerable<ImageRecognitionMatch> FindMatches(
            string sourcePath,
            string testPath,
            double precision,
            double scale,
            bool stopAtFirstMatch,
            bool blackAndWhite)
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

                                matchesFound.Add(new ImageRecognitionMatch()
                                {
                                    BottomLeftPoint = new ImageRecognitionPoint() { PositionX = points[0].X, PositionY = points[0].Y },
                                    BottomRightPoint = new ImageRecognitionPoint() { PositionX = points[1].X, PositionY = points[1].Y },
                                    TopRightPoint = new ImageRecognitionPoint() { PositionX = points[2].X, PositionY = points[2].Y },
                                    TopLeftPoint = new ImageRecognitionPoint() { PositionX = points[3].X, PositionY = points[3].Y },
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


        //    using (var sourceBitmap = new Bitmap(sourcePath))
        //    using (var testBitmap = new Bitmap(testPath))
        //        return FindMatches(sourceBitmap, testBitmap, precision, scale, stopAtFirstMatch, blackAndWhite);
        //}

        //public IEnumerable<Rectangle> FindMatches(
        //    Bitmap sourceBitmap, 
        //    Bitmap testBitmap,
        //    double precision, 
        //    double scale, 
        //    bool stopAtFirstMatch,
        //    bool blackAndWhite)
        //{
        //    if (completeImage == detailImage) return;

        //    try
        //    {
        //        long score;
        //        long matchTime;

        //        using (Mat modelImage = CvInvoke.Imread(detailImage, ImreadModes.Color))
        //        using (Mat observedImage = CvInvoke.Imread(completeImage, ImreadModes.Color))
        //        {
        //            Mat homography;
        //            VectorOfKeyPoint modelKeyPoints;
        //            VectorOfKeyPoint observedKeyPoints;

        //            using (var matches = new VectorOfVectorOfDMatch())
        //            {
        //                Mat mask;
        //                DrawMatches.FindMatch(modelImage, observedImage, out matchTime, out modelKeyPoints, out observedKeyPoints, matches,
        //                    out mask, out homography, out score);
        //            }

        //            imgList.Add(new WeightedImages() { ImagePath = completeImage, Score = score });
        //        }
        //    }
        //    catch { }
        //}
        //    throw new System.NotImplementedException();
        //}

//        public static Mat Draw(Mat modelImage, Mat observedImage, out long matchTime, out long score)
//        {
//            Mat homography;
//            VectorOfKeyPoint modelKeyPoints;
//            VectorOfKeyPoint observedKeyPoints;

//            using (var matches = new VectorOfVectorOfDMatch())
//            {
//                Mat mask;
//                FindMatch(modelImage, observedImage, out matchTime, out modelKeyPoints, out observedKeyPoints, matches,
//                   out mask, out homography, out score);

//                //Draw the matched keypoints
//                Mat result = new Mat();
//                Features2DToolbox.DrawMatches(modelImage, modelKeyPoints, observedImage, observedKeyPoints,
//                   matches, result, new MCvScalar(255, 255, 255), new MCvScalar(255, 255, 255), mask);

//                #region draw the projected region on the image

//                if (homography != null)
//                {
//                    //draw a rectangle along the projected model
//                    Rectangle rect = new Rectangle(Point.Empty, modelImage.Size);
//                    PointF[] pts = {
//                  new PointF(rect.Left, rect.Bottom),
//                  new PointF(rect.Right, rect.Bottom),
//                  new PointF(rect.Right, rect.Top),
//                  new PointF(rect.Left, rect.Top)
//                        };
//                    pts = CvInvoke.PerspectiveTransform(pts, homography);

//#if NETFX_CORE
//               Point[] points = Extensions.ConvertAll<PointF, Point>(pts, Point.Round);
//#else
//                    var points = Array.ConvertAll(pts, Point.Round);
//#endif
//                    using (VectorOfPoint vp = new VectorOfPoint(points))
//                        CvInvoke.Polylines(result, vp, true, new MCvScalar(255, 0, 0, 255), 5);
//                }
//                #endregion

//                return result;

//            }
//        }

    }
}
