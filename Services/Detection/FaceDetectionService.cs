using Emgu.CV.CvEnum;
using Emgu.CV;
using LR3.Interfaces;

namespace LR3.Services.Detection
{
    internal class FaceDetectionService : IDetectionService
    {
        private CascadeClassifier _faceCascade;

        public FaceDetectionService(string cascadePath)
        {
            _faceCascade = new CascadeClassifier(cascadePath);
        }

        public List<Rectangle> Apply(Mat frame)
        {
            using var gray = new UMat();
            CvInvoke.CvtColor(frame, gray, ColorConversion.Bgr2Gray);
            var faces = _faceCascade.DetectMultiScale(gray, 1.1, 10);
            return faces.ToList();
        }
    }
}
