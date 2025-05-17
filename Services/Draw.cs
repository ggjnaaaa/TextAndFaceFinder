using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace LR3.Services
{
    public static class Draw
    {
        public static void DrawRegions(Mat frame, List<Rectangle> regions, Rectangle? activeRegion = null)
        {
            if (frame == null || frame.IsEmpty || regions == null)
                return;

            var image = frame.ToImage<Bgr, byte>();

            DrawRegions(image, regions, activeRegion);

            image.Mat.CopyTo(frame);
        }

        public static void DrawRegions(Image<Bgr, byte> image, List<Rectangle> regions, Rectangle? activeRegion = null)
        {
            int baseSize = Math.Min(image.Width, image.Height);
            int thickness = Math.Max(1, Math.Min(20, baseSize / 150));

            foreach (var rect in regions)
            {
                var color = (activeRegion.HasValue && rect == activeRegion.Value)
                            ? new Bgr(Color.LimeGreen)
                            : new Bgr(Color.DarkBlue);

                image.Draw(rect, color, thickness);
            }
        }

        public static void DrawMasks(Mat frame, Image<Bgr, byte> mask, List<Rectangle> regions)
        {
            if (frame == null || frame.IsEmpty || regions == null)
                return;

            var image = frame.ToImage<Bgr, byte>();

            DrawMasks(image, mask, regions);

            image.Mat.CopyTo(frame);
        }

        public static void DrawMasks(Image<Bgr, byte> image, Image<Bgr, byte> mask, List<Rectangle> regions)
        {
            foreach (var rect in regions)
            {
                var resizedMask = mask.Convert<Bgr, byte>().Resize(rect.Width, rect.Height, Inter.Linear);

                var roi = image.GetSubRect(rect);
                resizedMask.CopyTo(roi);
            }
        }
    }
}
