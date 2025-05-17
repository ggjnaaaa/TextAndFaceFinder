using Emgu.CV;

namespace LR3.Models
{
    internal class CameraEditor : VideoEditor
    {
        public CameraEditor()
        {
            IsVideo = true;
            Capture = new VideoCapture(0);
            IsPaused = false;

            if (!Capture.IsOpened)
                throw new InvalidOperationException("Не удалось открыть видеофайл");

            updateFrames();
        }
    }
}
