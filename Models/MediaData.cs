using Emgu.CV.Structure;
using Emgu.CV;

namespace LR3.Models
{
    public class MediaData
    {
        public string FilePath { get; set; }
        public bool IsVideo { get; set; }
        public Image<Bgr, byte> Image { get; set; }
    }
}
