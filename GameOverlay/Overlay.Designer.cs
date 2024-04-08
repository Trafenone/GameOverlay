namespace GameOverlay
{
    partial class Overlay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Overlay));
            FirstSpellPicture = new PictureBox();
            SecondSpellPicture = new PictureBox();
            ThirdSpellPicture = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)FirstSpellPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SecondSpellPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ThirdSpellPicture).BeginInit();
            SuspendLayout();
            // 
            // FirstSpellPicture
            // 
            FirstSpellPicture.Image = (Image)resources.GetObject("FirstSpellPicture.Image");
            FirstSpellPicture.Location = new Point(48, 42);
            FirstSpellPicture.Name = "FirstSpellPicture";
            FirstSpellPicture.Size = new Size(64, 64);
            FirstSpellPicture.TabIndex = 3;
            FirstSpellPicture.TabStop = false;
            FirstSpellPicture.MouseDown += PictureMouseDown;
            FirstSpellPicture.MouseMove += PictureMouseMove;
            FirstSpellPicture.MouseUp += PictureMouseUp;
            // 
            // SecondSpellPicture
            // 
            SecondSpellPicture.Image = (Image)resources.GetObject("SecondSpellPicture.Image");
            SecondSpellPicture.Location = new Point(133, 42);
            SecondSpellPicture.Name = "SecondSpellPicture";
            SecondSpellPicture.Size = new Size(64, 64);
            SecondSpellPicture.TabIndex = 4;
            SecondSpellPicture.TabStop = false;
            SecondSpellPicture.MouseDown += PictureMouseDown;
            SecondSpellPicture.MouseMove += PictureMouseMove;
            SecondSpellPicture.MouseUp += PictureMouseUp;
            // 
            // ThirdSpellPicture
            // 
            ThirdSpellPicture.Image = (Image)resources.GetObject("ThirdSpellPicture.Image");
            ThirdSpellPicture.Location = new Point(220, 42);
            ThirdSpellPicture.Name = "ThirdSpellPicture";
            ThirdSpellPicture.Size = new Size(64, 64);
            ThirdSpellPicture.TabIndex = 5;
            ThirdSpellPicture.TabStop = false;
            ThirdSpellPicture.MouseDown += PictureMouseDown;
            ThirdSpellPicture.MouseMove += PictureMouseMove;
            ThirdSpellPicture.MouseUp += PictureMouseUp;
            // 
            // Overlay
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ThirdSpellPicture);
            Controls.Add(SecondSpellPicture);
            Controls.Add(FirstSpellPicture);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Overlay";
            Text = "Overlay";
            TopMost = true;
            TransparencyKey = SystemColors.Control;
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)FirstSpellPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)SecondSpellPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)ThirdSpellPicture).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox FirstSpellPicture;
        private PictureBox SecondSpellPicture;
        private PictureBox ThirdSpellPicture;
    }
}