using Emgu.CV.Structure;
using Emgu.CV;
using LR3.Services;

namespace LR3.Models
{
    internal class ImageEditor : MediaEditorBase
    {
        public Image<Bgr, byte> OriginalImage { get; set; }
        public Image<Bgr, byte> ProcessedImage { get; set; }

        public override Image<Bgr, byte> GetOriginal() => OriginalImage.Clone();
        public override Image<Bgr, byte> GetProcessed() => ProcessedImage.Clone();
        public override void ResetChanges()
        {
            ProcessedImage = OriginalImage.Clone();
            reserFlags();
            OnUpdateInvoke();
        }

        public ImageEditor(Image<Bgr, byte> image)
        {
            OriginalImage = image;
            ProcessedImage = image.Clone();
        }

        protected override void UpdateProcessedImage()
        {
            ProcessedImage = GetOriginal();
            Draw.DrawRegions(ProcessedImage, DetectedRegions, ActiveRegion);
        }
        protected override void ReadTextFromRegion(Rectangle region)
        {
            if (region.IsEmpty)
                return;
            var text = recognitionService.Recognize(OriginalImage.Mat, region);
            MessageBox.Show(text, "Распознанный текст", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OnTextRecognized?.Invoke(text);
        }
        public override void DetectFaces()
        {
            base.DetectFaces();
            ProcessedImage = GetOriginal();
            DetectedRegions = DetectionFacade.DetectFaces(OriginalImage.Mat.Clone(), Path.Combine(TempFolderPath, "haarcascade_frontalface_default.xml"));
            Draw.DrawRegions(ProcessedImage, DetectedRegions, ActiveRegion);
            OnUpdateInvoke();
        }

        public override void DetectText()
        {
            base.DetectText();
            ProcessedImage = GetOriginal();
            DetectedRegions = DetectionFacade.DetectText(OriginalImage.Mat.Clone());
            Draw.DrawRegions(ProcessedImage, DetectedRegions, ActiveRegion);
            OnUpdateInvoke();
        }

        public override void EnableMask(Image<Bgr, byte> mask)
        {
            base.EnableMask(mask);
            ProcessedImage = GetOriginal();
            DetectedRegions = DetectionFacade.DetectFaces(OriginalImage.Mat.Clone(), Path.Combine(TempFolderPath, "haarcascade_frontalface_default.xml"));
            Draw.DrawMasks(ProcessedImage, mask, DetectedRegions);
            OnUpdateInvoke();
        }
    }
}
