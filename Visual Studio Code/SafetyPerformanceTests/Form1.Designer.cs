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
            this.liveVideo = new System.Windows.Forms.Button();
            this.livePicture = new System.Windows.Forms.PictureBox();
            this.filter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.livePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // liveVideo
            // 
            this.liveVideo.Location = new System.Drawing.Point(12, 12);
            this.liveVideo.Name = "liveVideo";
            this.liveVideo.Size = new System.Drawing.Size(70, 30);
            this.liveVideo.TabIndex = 0;
            this.liveVideo.Text = "Live Video";
            this.liveVideo.UseVisualStyleBackColor = true;
            this.liveVideo.Click += new System.EventHandler(this.ExecuteStartLiveVideoCommand);
            // 
            // livePicture
            // 
            this.livePicture.Location = new System.Drawing.Point(12, 57);
            this.livePicture.Name = "livePicture";
            this.livePicture.Size = new System.Drawing.Size(1935, 680);
            this.livePicture.TabIndex = 1;
            this.livePicture.TabStop = false;
            // 
            // filter
            // 
            this.filter.Location = new System.Drawing.Point(123, 12);
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(70, 30);
            this.filter.TabIndex = 2;
            this.filter.Text = "Filter";
            this.filter.UseVisualStyleBackColor = true;
            this.filter.Click += new System.EventHandler(this.filter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(222, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 46);
            this.label1.TabIndex = 3;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1468, 712);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filter);
            this.Controls.Add(this.livePicture);
            this.Controls.Add(this.liveVideo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(1500, 900);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.livePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button liveVideo;
        private System.Windows.Forms.PictureBox livePicture;
        private System.Windows.Forms.Button filter;
        private System.Windows.Forms.Label label1;
    }
}

