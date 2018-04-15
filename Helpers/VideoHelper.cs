//using System;
//using System.Linq;
//using Microsoft.Expression.Encoder;
//using ToBe.Tools.Extensions.Streams;

//namespace ToBe.Tools.Helpers
//{
//    public static class VideoHelper
//    {
//         
//        public static byte[] GetScreenShot(
//            string videoPath,
//            TimeSpan timeElapsedSinceTheStartOfTheVideo)
//        {
//            AudioVideoFile audioVideoFile = new AudioVideoFile(videoPath);

//            return audioVideoFile.GetThumbnail(
//                    timeElapsedSinceTheStartOfTheVideo,
//                    audioVideoFile.VideoStreams.First().VideoSize)
//                .ToBytes();
//        }
//        
//    }
//}