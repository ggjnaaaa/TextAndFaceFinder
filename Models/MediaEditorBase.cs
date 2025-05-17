using Emgu.CV;
using Emgu.CV.Structure;
using LR3.Services;

namespace LR3.Models
{
    internal abstract class MediaEditorBase : IDisposable
    {
        public event Action OnUpdate;
        public Action<string> OnTextRecognized;
        public bool IsVideo { get; protected set; } = false;
        public List<Rectangle> DetectedRegions { get; protected set; } = new();
        public Rectangle? ActiveRegion { get; protected set; } = null;

        protected bool _isTextDetectionEnabled = false;
        protected bool _isFacesDetectionEnabled = false;
        protected bool _useMask = false;

        protected Image<Bgr, byte>? _maskImage = null;
        protected TextRecognitionService recognitionService;
        protected readonly string TempFolderPath;

        protected MediaEditorBase()
        {
            TempFolderPath = TempFolderCopier.GetOrCopyToTemp("Resources");
            recognitionService = new TextRecognitionService(Path.Combine(TempFolderPath, "tessdata"));
        }

        public virtual void DetectFaces()
        {
            _isFacesDetectionEnabled = true;
            _isTextDetectionEnabled = false;
            _useMask = false;
        }
        public virtual void DetectText()
        {
            _isFacesDetectionEnabled = false;
            _isTextDetectionEnabled = true;
            _useMask = false;
        }
        public virtual void EnableMask(Image<Bgr, byte> mask)
        {
            _maskImage = mask;
            _useMask = true;
        }
        protected void reserFlags()
        {
            _isTextDetectionEnabled = false;
            _isFacesDetectionEnabled = false;
            _useMask = false;
        }

        protected void OnUpdateInvoke()
        {
            OnUpdate?.Invoke();
        }

        public abstract Image<Bgr, byte> GetOriginal();
        public abstract Image<Bgr, byte> GetProcessed();
        public abstract void ResetChanges();
        protected abstract void UpdateProcessedImage();
        protected abstract void ReadTextFromRegion(Rectangle region);
        public void Dispose() { }

        public virtual void LeftClick(int x, int y)
        {
            if (!_isTextDetectionEnabled)
                return;

            var closest = findRectangle(x, y);

            if (closest != null)
            {
                ActiveRegion = closest;
                ReadTextFromRegion(closest.Value);
                UpdateProcessedImage();
                OnUpdateInvoke();
            }
            else
            {
                MessageBox.Show("Текст не найден", "Распознанный текст", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnTextRecognized?.Invoke("");
            }
        }
        public virtual void HighlightRectangle(int x, int y)
        {
            var closest = findRectangle(x, y);

            if (closest != null)
            {
                ActiveRegion = closest;
                UpdateProcessedImage();
                OnUpdateInvoke();
            }
        }
        private Rectangle? findRectangle(int x, int y, int maxSearchRadius = 2)
        {
            if (DetectedRegions.Count == 0)
                return null;

            var point = new Point(x, y);
            Rectangle? closest = null;
            double minDistance = double.MaxValue;

            foreach (var region in DetectedRegions)
            {
                if (region.Contains(point))
                {
                    return region;
                }

                int dx = 0;
                if (x < region.Left)
                    dx = region.Left - x;
                else if (x > region.Right)
                    dx = x - region.Right;

                int dy = 0;
                if (y < region.Top)
                    dy = region.Top - y;
                else if (y > region.Bottom)
                    dy = y - region.Bottom;

                double distance = Math.Sqrt(dx * dx + dy * dy);

                if (distance < minDistance && distance <= maxSearchRadius)
                {
                    minDistance = distance;
                    closest = region;
                }
            }

            return closest;
        }

    }
}
