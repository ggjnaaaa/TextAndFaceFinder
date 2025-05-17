using Emgu.CV.UI;

namespace LR3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            originalImageBox = new ImageBox();
            resultImageBox = new ImageBox();
            resetBtn = new Button();
            loadFileButton = new Button();
            StopPlayVideo = new Button();
            videoTimer = new System.Windows.Forms.Timer(components);
            TextDetect = new Button();
            FacesDetect = new Button();
            WebCamera = new Button();
            MaskLoad = new Button();
            RecognizedText = new TextBox();
            CopyText = new Button();
            ((System.ComponentModel.ISupportInitialize)originalImageBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)resultImageBox).BeginInit();
            SuspendLayout();
            // 
            // originalImageBox
            // 
            originalImageBox.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            originalImageBox.Location = new Point(12, 12);
            originalImageBox.Name = "originalImageBox";
            originalImageBox.Size = new Size(761, 407);
            originalImageBox.SizeMode = PictureBoxSizeMode.Zoom;
            originalImageBox.TabIndex = 2;
            originalImageBox.TabStop = false;
            // 
            // resultImageBox
            // 
            resultImageBox.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            resultImageBox.Location = new Point(12, 431);
            resultImageBox.Name = "resultImageBox";
            resultImageBox.Size = new Size(761, 407);
            resultImageBox.SizeMode = PictureBoxSizeMode.Zoom;
            resultImageBox.TabIndex = 3;
            resultImageBox.TabStop = false;
            resultImageBox.MouseClick += resultImageBox_MouseClick;
            resultImageBox.MouseMove += resultImageBox_MouseMove;
            // 
            // resetBtn
            // 
            resetBtn.BackColor = SystemColors.Control;
            resetBtn.ForeColor = SystemColors.ActiveCaptionText;
            resetBtn.Location = new Point(779, 140);
            resetBtn.Name = "resetBtn";
            resetBtn.Size = new Size(148, 23);
            resetBtn.TabIndex = 7;
            resetBtn.Text = "Сбросить изменения";
            resetBtn.UseVisualStyleBackColor = false;
            resetBtn.Click += resetBtn_Click;
            // 
            // loadFileButton
            // 
            loadFileButton.BackColor = SystemColors.Control;
            loadFileButton.ForeColor = SystemColors.ActiveCaptionText;
            loadFileButton.Location = new Point(779, 13);
            loadFileButton.Name = "loadFileButton";
            loadFileButton.Size = new Size(119, 23);
            loadFileButton.TabIndex = 6;
            loadFileButton.Text = "Загрузить файл";
            loadFileButton.UseVisualStyleBackColor = false;
            loadFileButton.Click += loadImageButton_Click;
            // 
            // StopPlayVideo
            // 
            StopPlayVideo.BackColor = SystemColors.Control;
            StopPlayVideo.Enabled = false;
            StopPlayVideo.ForeColor = SystemColors.ActiveCaptionText;
            StopPlayVideo.Location = new Point(779, 42);
            StopPlayVideo.Name = "StopPlayVideo";
            StopPlayVideo.Size = new Size(119, 23);
            StopPlayVideo.TabIndex = 8;
            StopPlayVideo.Text = "Запустить видео";
            StopPlayVideo.UseVisualStyleBackColor = false;
            StopPlayVideo.Click += StopPlayVideo_Click;
            // 
            // videoTimer
            // 
            videoTimer.Enabled = true;
            videoTimer.Interval = 20;
            videoTimer.Tick += videoTimer_Tick;
            // 
            // TextDetect
            // 
            TextDetect.BackColor = SystemColors.Control;
            TextDetect.ForeColor = SystemColors.ActiveCaptionText;
            TextDetect.Location = new Point(779, 208);
            TextDetect.Name = "TextDetect";
            TextDetect.Size = new Size(90, 23);
            TextDetect.TabIndex = 9;
            TextDetect.Text = "Найти текст";
            TextDetect.UseVisualStyleBackColor = false;
            TextDetect.Click += TextDetect_Click;
            // 
            // FacesDetect
            // 
            FacesDetect.BackColor = SystemColors.Control;
            FacesDetect.ForeColor = SystemColors.ActiveCaptionText;
            FacesDetect.Location = new Point(779, 237);
            FacesDetect.Name = "FacesDetect";
            FacesDetect.Size = new Size(90, 23);
            FacesDetect.TabIndex = 10;
            FacesDetect.Text = "Найти лица";
            FacesDetect.UseVisualStyleBackColor = false;
            FacesDetect.Click += FacesDetect_Click;
            // 
            // WebCamera
            // 
            WebCamera.BackColor = SystemColors.Control;
            WebCamera.ForeColor = SystemColors.ActiveCaptionText;
            WebCamera.Location = new Point(779, 71);
            WebCamera.Name = "WebCamera";
            WebCamera.Size = new Size(154, 23);
            WebCamera.TabIndex = 11;
            WebCamera.Text = "Включить веб камеру";
            WebCamera.UseVisualStyleBackColor = false;
            WebCamera.Click += WebCamera_Click;
            // 
            // MaskLoad
            // 
            MaskLoad.BackColor = SystemColors.Control;
            MaskLoad.ForeColor = SystemColors.ActiveCaptionText;
            MaskLoad.Location = new Point(779, 266);
            MaskLoad.Name = "MaskLoad";
            MaskLoad.Size = new Size(117, 23);
            MaskLoad.TabIndex = 12;
            MaskLoad.Text = "Загрузить маску";
            MaskLoad.UseVisualStyleBackColor = false;
            MaskLoad.Click += MaskLoad_Click;
            // 
            // RecognizedText
            // 
            RecognizedText.Location = new Point(779, 431);
            RecognizedText.Multiline = true;
            RecognizedText.Name = "RecognizedText";
            RecognizedText.Size = new Size(266, 374);
            RecognizedText.TabIndex = 13;
            // 
            // CopyText
            // 
            CopyText.Location = new Point(847, 811);
            CopyText.Name = "CopyText";
            CopyText.Size = new Size(136, 23);
            CopyText.TabIndex = 14;
            CopyText.Text = "Скопировать текст";
            CopyText.UseVisualStyleBackColor = true;
            CopyText.Click += CopyText_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1057, 846);
            Controls.Add(CopyText);
            Controls.Add(RecognizedText);
            Controls.Add(MaskLoad);
            Controls.Add(WebCamera);
            Controls.Add(FacesDetect);
            Controls.Add(TextDetect);
            Controls.Add(StopPlayVideo);
            Controls.Add(resetBtn);
            Controls.Add(loadFileButton);
            Controls.Add(resultImageBox);
            Controls.Add(originalImageBox);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)originalImageBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)resultImageBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Emgu.CV.UI.ImageBox originalImageBox;
        private Emgu.CV.UI.ImageBox resultImageBox;
        private Button resetBtn;
        private Button loadFileButton;
        private Button StopPlayVideo;
        private System.Windows.Forms.Timer videoTimer;
        private Button TextDetect;
        private Button FacesDetect;
        private Button WebCamera;
        private Button MaskLoad;
        private TextBox RecognizedText;
        private Button CopyText;
    }
}
