using Emgu.CV.Structure;
using Emgu.CV;
using LR3.Models;

namespace LR3.Services
{
    /// <summary>
    /// Сервис для загрузки изображений.
    /// </summary>
    public static class MediaLoader
    {
        /// <summary>
        /// Загружает изображение или видео с помощью диалогового окна.
        /// </summary>
        public static MediaData LoadMedia()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Медиафайлы|*.jpg;*.png;*.bmp;*.avi;*.mp4;*.mov";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ext = Path.GetExtension(openFileDialog.FileName).ToLower();

                    if (ext == ".jpg" || ext == ".png" || ext == ".bmp")
                    {
                        return new MediaData
                        {
                            FilePath = openFileDialog.FileName,
                            IsVideo = false,
                            Image = new Image<Bgr, byte>(openFileDialog.FileName)
                        };
                    }
                    else // видео
                    {
                        return new MediaData
                        {
                            FilePath = openFileDialog.FileName,
                            IsVideo = true
                        };
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Загружает изображение (маску) без альфа-канала.
        /// </summary>
        /// <returns>Маска в формате BGR, или null, если изображение не выбрано.</returns>
        public static Image<Bgr, byte>? LoadMask()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Выберите изображение-маску";
                openFileDialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var img = CvInvoke.Imread(openFileDialog.FileName, Emgu.CV.CvEnum.ImreadModes.Color);
                    return img.ToImage<Bgr, byte>();
                }
            }

            return null;
        }
    }
}
