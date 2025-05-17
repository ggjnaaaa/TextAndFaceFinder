using LR3.Services;
using LR3.Models;

namespace LR3
{
    public partial class Form1 : Form
    {
        MediaEditorBase _editor;

        public Form1()
        {
            InitializeComponent();
        }

        private VideoEditor getVideoEditor()
        {
            if (_editor == null || !_editor.IsVideo) return null;

            return (VideoEditor)_editor;
        }

        private void setVideo(MediaData mediaData)
        {
            _editor?.Dispose();
            _editor = new VideoEditor(mediaData.FilePath);
            subscribeTextRecognized();

            updateImages();

            StopPlayVideo.Enabled = true;
            changeStopPauseButtonText();
        }

        private void setPhoto(MediaData mediaData)
        {
            _editor?.Dispose();
            _editor = new ImageEditor(mediaData.Image);
            subscribeTextRecognized();

            updateImages();

            StopPlayVideo.Enabled = false;
        }

        private void updateImages()
        {
            var current = _editor.GetOriginal();
            var processed = _editor.GetProcessed();

            if (current != null && processed != null)
            {
                originalImageBox.Image = current;
                resultImageBox.Image = processed;
            }
        }

        private void subscribeTextRecognized()
        {
            if (_editor == null) return;
            _editor.OnTextRecognized = text =>
            {
                RecognizedText.Text = text;
            };
        }

        private void changeStopPauseButtonText()
        {
            var videoEditor = getVideoEditor();
            if (videoEditor == null) return;
            StopPlayVideo.Text = videoEditor.IsPaused ? "Запустить видео" : "Остановить видео";
        }

        private void videoTimer_Tick(object sender, EventArgs e)
        {
            var videoEditor = getVideoEditor();
            if (videoEditor == null || videoEditor.IsPaused) return;

            videoEditor.Update();
            updateImages();
        }

        private void resultImageBox_MouseMove(object sender, MouseEventArgs e)
        {
            _editor?.HighlightRectangle(e.X, e.Y);
        }

        private void resultImageBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _editor?.LeftClick(e.X, e.Y);
        }

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            var mediaData = MediaLoader.LoadMedia();
            if (mediaData != null)
            {
                if (mediaData.IsVideo) setVideo(mediaData);
                else setPhoto(mediaData);

                _editor.OnUpdate += updateImages;
            }
            else
            {
                MessageBox.Show("Не удалось загрузить файл.");
            }
        }

        private void StopPlayVideo_Click(object sender, EventArgs e)
        {
            var videoEditor = getVideoEditor();

            if (videoEditor == null) return;

            videoEditor.PlayPause();
            changeStopPauseButtonText();
        }

        private void TextDetect_Click(object sender, EventArgs e) => _editor.DetectText();
        private void resetBtn_Click(object sender, EventArgs e) => _editor.ResetChanges();
        private void FacesDetect_Click(object sender, EventArgs e) => _editor.DetectFaces();

        private void WebCamera_Click(object sender, EventArgs e)
        {
            _editor?.Dispose();
            _editor = new CameraEditor();
            subscribeTextRecognized();

            updateImages();

            StopPlayVideo.Enabled = true;
            changeStopPauseButtonText();
        }

        private void MaskLoad_Click(object sender, EventArgs e)
        {
            var mask = MediaLoader.LoadMask();
            if (_editor == null || mask == null) return;
            _editor?.EnableMask(mask);
        }

        private void CopyText_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(RecognizedText.Text);
        }
    }
}
