using Emgu.CV;
using Emgu.CV.Structure;
using LR3.Services;

namespace LR3.Models
{
    internal class VideoEditor : MediaEditorBase
    {
        public string? FilePath { get; set; }
        public VideoCapture? Capture { get; set; }
        public bool IsPaused { get; protected set; } = true;

        public Mat CurrentFrame { get; private set; } = new();
        public Mat ProcessedFrame { get; private set; } = new();

        public override Image<Bgr, byte> GetOriginal() => CurrentFrame.ToImage<Bgr, byte>().Clone();
        public override Image<Bgr, byte> GetProcessed() => ProcessedFrame.ToImage<Bgr, byte>().Clone();

        private bool _detectionRunning = false;
        private readonly object _detectionLock = new();

        protected VideoEditor() { }
        public VideoEditor(string filePath)
        {
            IsVideo = true;
            FilePath = filePath;
            Capture = new VideoCapture(filePath);

            if (!Capture.IsOpened)
                throw new InvalidOperationException("Не удалось открыть видеофайл");

            DetectText();
            updateFrames();
        }

        public override void ResetChanges()
        {
            ProcessedFrame = CurrentFrame.Clone();
            reserFlags();
            OnUpdateInvoke();
        }

        public void PlayPause()
        {
            IsPaused = !IsPaused;
        }

        public void Update()
        {
            if (IsPaused) return;

            updateFrames();
        }

        protected override void UpdateProcessedImage()
        {
            if (_useMask && _maskImage != null)
            {
                Draw.DrawMasks(ProcessedFrame, _maskImage, DetectedRegions);
            }
            else
            {
                Draw.DrawRegions(ProcessedFrame, DetectedRegions, ActiveRegion);
            }
        }

        protected override void ReadTextFromRegion(Rectangle region)
        {
            if (region.IsEmpty)
                return;
            var text = recognitionService.Recognize(CurrentFrame, region);
            MessageBox.Show(text, "Распознанный текст", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OnTextRecognized?.Invoke(text);
        }

        protected void updateFrames()
        {
            if (Capture == null || !Capture.Grab()) return;

            Capture.Retrieve(CurrentFrame);
            ProcessedFrame = CurrentFrame.Clone();

            if (!_isTextDetectionEnabled && !_isFacesDetectionEnabled && !_useMask)
            {
                UpdateProcessedImage();
                OnUpdateInvoke();
                return;
            }

            if (!_detectionRunning)
            {
                _detectionRunning = true;

                var frameClone = CurrentFrame.Clone();
                Task.Run(() =>
                {
                    List<Rectangle> regions = new();

                    if (_isFacesDetectionEnabled || _useMask)
                    {
                        var cascadePath = Path.Combine(TempFolderPath, "haarcascade_frontalface_default.xml");
                        regions = DetectionFacade.DetectFaces(frameClone, cascadePath);
                    }
                    else if (_isTextDetectionEnabled)
                    {
                        regions = DetectionFacade.DetectText(frameClone);
                    }

                    lock (_detectionLock)
                    {
                        DetectedRegions = regions;
                    }

                    _detectionRunning = false;
                });
            }

            lock (_detectionLock)
            {
                UpdateProcessedImage();
            }

            OnUpdateInvoke();
        }

        public void Dispose()
        {
            Capture?.Dispose();
        }
    }
}
