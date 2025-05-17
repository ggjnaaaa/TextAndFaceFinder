using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using LR3.Interfaces;

namespace LR3.Services.Detection
{
    internal class TextDetectionService : IDetectionService
    {
        public List<Rectangle> Apply(Mat frame)
        {
            var rectangles = new List<Rectangle>();
            if (frame == null || frame.IsEmpty)
                return rectangles;

            var hsv = new Mat();
            CvInvoke.CvtColor(frame, hsv, ColorConversion.Bgr2Hsv);

            var hsvImage = hsv.ToImage<Hsv, byte>();

            var mask = hsvImage.Split()[1]; // канал Saturation
            CvInvoke.Threshold(mask, mask, 50, 255, ThresholdType.Binary);

            mask._Dilate(2);
            mask._Erode(1);

            using var contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(mask, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            for (int i = 0; i < contours.Size; i++)
            {
                var area = CvInvoke.ContourArea(contours[i]);
                if (area < 30) continue;

                var rect = CvInvoke.BoundingRectangle(contours[i]);

                if (rect.Width > frame.Width * 0.5 && rect.Height > frame.Height * 0.5)
                    continue;

                rectangles.Add(rect);
            }

            return rectangles;
        }
    }
}
