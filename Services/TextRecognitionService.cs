using Emgu.CV.OCR;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
namespace LR3.Services
{
    internal class TextRecognitionService
    {
        private Tesseract _ocr;

        public TextRecognitionService(string tessDataPath)
        {
            _ocr = new Tesseract(tessDataPath, "eng+rus", OcrEngineMode.TesseractLstmCombined);
        }

        public string Recognize(Mat frame, Rectangle region)
        {
            if (frame == null || region == Rectangle.Empty)
                return string.Empty;

            using var roiImg = new Mat(frame, region);
            _ocr.SetImage(roiImg);
            _ocr.Recognize();

            return _ocr.GetUTF8Text();
        }
    }
}
