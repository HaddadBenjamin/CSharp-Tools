namespace Ben.Tools.Development.Services.Models
{
    public class ImageRecognitionMatch
    {
        public ImageRecognitionPoint TopLeftPoint { get; set; }
        public ImageRecognitionPoint TopRightPoint { get; set; }
        public ImageRecognitionPoint BottomLeftPoint { get; set; }
        public ImageRecognitionPoint BottomRightPoint { get; set; }

        public ImageRecognitionPoint MiddlePoint => new ImageRecognitionPoint()
        {
            X = (TopLeftPoint.X + TopRightPoint.X + BottomLeftPoint.X + BottomRightPoint.X) / 4,
            Y = (TopLeftPoint.Y + TopRightPoint.Y + BottomLeftPoint.Y + BottomRightPoint.Y) / 4,
        };

        public float X => TopLeftPoint.X;
        public float Y => TopLeftPoint.Y;
        public float Width => TopRightPoint.X - TopLeftPoint.X;
        public float Height => BottomLeftPoint.X - TopLeftPoint.X;
    }
}