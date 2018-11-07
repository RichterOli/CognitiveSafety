namespace SafetyPerformanceTests
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.filter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonTakePicture = new System.Windows.Forms.Button();
            this.buttonHistogramm = new System.Windows.Forms.Button();
            this.histogramBox1 = new Emgu.CV.UI.HistogramBox();
            this.histogramBox2 = new Emgu.CV.UI.HistogramBox();
            this.histogramBox3 = new Emgu.CV.UI.HistogramBox();
            this.imgbOriginal = new Emgu.CV.UI.ImageBox();
            this.imgbProcessed = new Emgu.CV.UI.ImageBox();
            this.tbArea = new System.Windows.Forms.TextBox();
            this.tbRedMin = new System.Windows.Forms.TextBox();
            this.tbRedMax = new System.Windows.Forms.TextBox();
            this.tbGreenMax = new System.Windows.Forms.TextBox();
            this.tbGreenMin = new System.Windows.Forms.TextBox();
            this.tbBlueMin = new System.Windows.Forms.TextBox();
            this.tbBlueMax = new System.Windows.Forms.TextBox();
            this.lbRed = new System.Windows.Forms.Label();
            this.lbGreen = new System.Windows.Forms.Label();
            this.lbBlue = new System.Windows.Forms.Label();
            this.lbMin = new System.Windows.Forms.Label();
            this.lbMax = new System.Windows.Forms.Label();
            this.gbColorFilterVal = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgbOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgbProcessed)).BeginInit();
            this.gbColorFilterVal.SuspendLayout();
            this.SuspendLayout();
            // 
            // filter
            // 
            this.filter.Location = new System.Drawing.Point(12, 12);
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(42, 27);
            this.filter.TabIndex = 2;
            this.filter.Text = "Filter";
            this.filter.UseVisualStyleBackColor = true;
            this.filter.Click += new System.EventHandler(this.filter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(223, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 42);
            this.label1.TabIndex = 3;
            this.label1.Text = "Live Video";
            // 
            // buttonTakePicture
            // 
            this.buttonTakePicture.Location = new System.Drawing.Point(60, 12);
            this.buttonTakePicture.Name = "buttonTakePicture";
            this.buttonTakePicture.Size = new System.Drawing.Size(77, 27);
            this.buttonTakePicture.TabIndex = 5;
            this.buttonTakePicture.Text = "Take Picture";
            this.buttonTakePicture.UseVisualStyleBackColor = true;
            this.buttonTakePicture.Click += new System.EventHandler(this.buttonTakePicture_Click);
            // 
            // buttonHistogramm
            // 
            this.buttonHistogramm.Location = new System.Drawing.Point(143, 12);
            this.buttonHistogramm.Name = "buttonHistogramm";
            this.buttonHistogramm.Size = new System.Drawing.Size(71, 27);
            this.buttonHistogramm.TabIndex = 6;
            this.buttonHistogramm.Text = "Histogramm";
            this.buttonHistogramm.UseVisualStyleBackColor = true;
            this.buttonHistogramm.Click += new System.EventHandler(this.buttonHistogramm_Click);
            // 
            // histogramBox1
            // 
            this.histogramBox1.Location = new System.Drawing.Point(24, 475);
            this.histogramBox1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.histogramBox1.Name = "histogramBox1";
            this.histogramBox1.Size = new System.Drawing.Size(470, 325);
            this.histogramBox1.TabIndex = 7;
            // 
            // histogramBox2
            // 
            this.histogramBox2.Location = new System.Drawing.Point(510, 475);
            this.histogramBox2.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.histogramBox2.Name = "histogramBox2";
            this.histogramBox2.Size = new System.Drawing.Size(470, 325);
            this.histogramBox2.TabIndex = 8;
            // 
            // histogramBox3
            // 
            this.histogramBox3.Location = new System.Drawing.Point(996, 475);
            this.histogramBox3.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.histogramBox3.Name = "histogramBox3";
            this.histogramBox3.Size = new System.Drawing.Size(470, 325);
            this.histogramBox3.TabIndex = 9;
            // 
            // imgbOriginal
            // 
            this.imgbOriginal.Location = new System.Drawing.Point(12, 45);
            this.imgbOriginal.Name = "imgbOriginal";
            this.imgbOriginal.Size = new System.Drawing.Size(720, 400);
            this.imgbOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgbOriginal.TabIndex = 2;
            this.imgbOriginal.TabStop = false;
            // 
            // imgbProcessed
            // 
            this.imgbProcessed.Location = new System.Drawing.Point(788, 45);
            this.imgbProcessed.Name = "imgbProcessed";
            this.imgbProcessed.Size = new System.Drawing.Size(720, 400);
            this.imgbProcessed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgbProcessed.TabIndex = 2;
            this.imgbProcessed.TabStop = false;
            // 
            // tbArea
            // 
            this.tbArea.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbArea.Location = new System.Drawing.Point(418, 475);
            this.tbArea.Multiline = true;
            this.tbArea.Name = "tbArea";
            this.tbArea.ReadOnly = true;
            this.tbArea.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbArea.Size = new System.Drawing.Size(673, 291);
            this.tbArea.TabIndex = 10;
            // 
            // tbRedMin
            // 
            this.tbRedMin.Location = new System.Drawing.Point(68, 49);
            this.tbRedMin.Name = "tbRedMin";
            this.tbRedMin.Size = new System.Drawing.Size(55, 44);
            this.tbRedMin.TabIndex = 11;
            // 
            // tbRedMax
            // 
            this.tbRedMax.Location = new System.Drawing.Point(171, 49);
            this.tbRedMax.Name = "tbRedMax";
            this.tbRedMax.Size = new System.Drawing.Size(55, 44);
            this.tbRedMax.TabIndex = 12;
            // 
            // tbGreenMax
            // 
            this.tbGreenMax.Location = new System.Drawing.Point(171, 107);
            this.tbGreenMax.Name = "tbGreenMax";
            this.tbGreenMax.Size = new System.Drawing.Size(55, 44);
            this.tbGreenMax.TabIndex = 14;
            // 
            // tbGreenMin
            // 
            this.tbGreenMin.Location = new System.Drawing.Point(68, 107);
            this.tbGreenMin.Name = "tbGreenMin";
            this.tbGreenMin.Size = new System.Drawing.Size(55, 44);
            this.tbGreenMin.TabIndex = 13;
            // 
            // tbBlueMin
            // 
            this.tbBlueMin.Location = new System.Drawing.Point(68, 169);
            this.tbBlueMin.Name = "tbBlueMin";
            this.tbBlueMin.Size = new System.Drawing.Size(55, 44);
            this.tbBlueMin.TabIndex = 15;
            // 
            // tbBlueMax
            // 
            this.tbBlueMax.Location = new System.Drawing.Point(171, 169);
            this.tbBlueMax.Name = "tbBlueMax";
            this.tbBlueMax.Size = new System.Drawing.Size(55, 44);
            this.tbBlueMax.TabIndex = 16;
            // 
            // lbRed
            // 
            this.lbRed.AutoSize = true;
            this.lbRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRed.Location = new System.Drawing.Point(17, 51);
            this.lbRed.Name = "lbRed";
            this.lbRed.Size = new System.Drawing.Size(86, 38);
            this.lbRed.TabIndex = 17;
            this.lbRed.Text = "Red:";
            // 
            // lbGreen
            // 
            this.lbGreen.AutoSize = true;
            this.lbGreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGreen.Location = new System.Drawing.Point(17, 109);
            this.lbGreen.Name = "lbGreen";
            this.lbGreen.Size = new System.Drawing.Size(117, 38);
            this.lbGreen.TabIndex = 18;
            this.lbGreen.Text = "Green:";
            // 
            // lbBlue
            // 
            this.lbBlue.AutoSize = true;
            this.lbBlue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBlue.Location = new System.Drawing.Point(19, 170);
            this.lbBlue.Name = "lbBlue";
            this.lbBlue.Size = new System.Drawing.Size(91, 38);
            this.lbBlue.TabIndex = 19;
            this.lbBlue.Text = "Blue:";
            // 
            // lbMin
            // 
            this.lbMin.AutoSize = true;
            this.lbMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMin.Location = new System.Drawing.Point(80, 30);
            this.lbMin.Name = "lbMin";
            this.lbMin.Size = new System.Drawing.Size(69, 38);
            this.lbMin.TabIndex = 20;
            this.lbMin.Text = "Min";
            // 
            // lbMax
            // 
            this.lbMax.AutoSize = true;
            this.lbMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMax.Location = new System.Drawing.Point(183, 30);
            this.lbMax.Name = "lbMax";
            this.lbMax.Size = new System.Drawing.Size(79, 38);
            this.lbMax.TabIndex = 21;
            this.lbMax.Text = "Max";
            // 
            // gbColorFilterVal
            // 
            this.gbColorFilterVal.Controls.Add(this.lbMin);
            this.gbColorFilterVal.Controls.Add(this.lbMax);
            this.gbColorFilterVal.Controls.Add(this.tbRedMin);
            this.gbColorFilterVal.Controls.Add(this.tbRedMax);
            this.gbColorFilterVal.Controls.Add(this.lbBlue);
            this.gbColorFilterVal.Controls.Add(this.tbGreenMax);
            this.gbColorFilterVal.Controls.Add(this.lbGreen);
            this.gbColorFilterVal.Controls.Add(this.tbGreenMin);
            this.gbColorFilterVal.Controls.Add(this.lbRed);
            this.gbColorFilterVal.Controls.Add(this.tbBlueMin);
            this.gbColorFilterVal.Controls.Add(this.tbBlueMax);
            this.gbColorFilterVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbColorFilterVal.Location = new System.Drawing.Point(81, 530);
            this.gbColorFilterVal.Name = "gbColorFilterVal";
            this.gbColorFilterVal.Size = new System.Drawing.Size(247, 215);
            this.gbColorFilterVal.TabIndex = 22;
            this.gbColorFilterVal.TabStop = false;
            this.gbColorFilterVal.Text = "Color Filter Values";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1235, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 24);
            this.button1.TabIndex = 4;
            this.button1.Text = "Motion Detection";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1468, 712);
            this.Controls.Add(this.gbColorFilterVal);
            this.Controls.Add(this.tbArea);
            this.Controls.Add(this.imgbProcessed);
            this.Controls.Add(this.imgbOriginal);
            this.Controls.Add(this.histogramBox3);
            this.Controls.Add(this.histogramBox2);
            this.Controls.Add(this.histogramBox1);
            this.Controls.Add(this.buttonHistogramm);
            this.Controls.Add(this.buttonTakePicture);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.filter);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Load += new System.EventHandler(this.onFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.imgbOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgbProcessed)).EndInit();
            this.gbColorFilterVal.ResumeLayout(false);
            this.gbColorFilterVal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button filter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonTakePicture;
        private System.Windows.Forms.Button buttonHistogramm;
        private Emgu.CV.UI.HistogramBox histogramBox1;
        private Emgu.CV.UI.HistogramBox histogramBox2;
        private Emgu.CV.UI.HistogramBox histogramBox3;
        private Emgu.CV.UI.ImageBox imgbOriginal;
        private Emgu.CV.UI.ImageBox imgbProcessed;
        private System.Windows.Forms.TextBox tbArea;
        private System.Windows.Forms.TextBox tbRedMin;
        private System.Windows.Forms.TextBox tbRedMax;
        private System.Windows.Forms.TextBox tbGreenMax;
        private System.Windows.Forms.TextBox tbGreenMin;
        private System.Windows.Forms.TextBox tbBlueMin;
        private System.Windows.Forms.TextBox tbBlueMax;
        private System.Windows.Forms.Label lbRed;
        private System.Windows.Forms.Label lbGreen;
        private System.Windows.Forms.Label lbBlue;
        private System.Windows.Forms.Label lbMin;
        private System.Windows.Forms.Label lbMax;
        private System.Windows.Forms.GroupBox gbColorFilterVal;
        private System.Windows.Forms.Button button1;
    }
}

