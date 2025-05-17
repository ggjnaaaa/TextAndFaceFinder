using Emgu.CV;
using LR3.Interfaces;
using LR3.Services.Detection;

namespace LR3.Services
{
    public static class DetectionFacade
    {
        public static List<Rectangle> DetectFaces(Mat frame, string cascadePath) => Detect(frame, new FaceDetectionService(cascadePath));
        public static List<Rectangle> DetectText(Mat frame) => Detect(frame, new TextDetectionService());

        private static List<Rectangle> Detect(Mat frame, IDetectionService detectionService)
        {
            if (frame == null)
                throw new ArgumentNullException(nameof(frame));
            return detectionService.Apply(frame);
        }
    }
}
