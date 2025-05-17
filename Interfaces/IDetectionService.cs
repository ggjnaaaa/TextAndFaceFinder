using Emgu.CV;

namespace LR3.Interfaces
{
    public interface IDetectionService
    {
        List<Rectangle> Apply(Mat frame);
    }
}
