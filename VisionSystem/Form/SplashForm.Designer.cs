namespace VisionSystem
{
    partial class SplashForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LblPercent = new System.Windows.Forms.Label();
            this.LblStatus = new System.Windows.Forms.Label();
            this.LblVersion = new System.Windows.Forms.Label();
            this.LblTitle = new System.Windows.Forms.Label();
            this.LoadingBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(228, 274);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(233, 98);
            this.pictureBox1.TabIndex = 121;
            this.pictureBox1.TabStop = false;
            // 
            // LblPercent
            // 
            this.LblPercent.AutoSize = true;
            this.LblPercent.BackColor = System.Drawing.Color.Transparent;
            this.LblPercent.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.LblPercent.Location = new System.Drawing.Point(56, 230);
            this.LblPercent.Name = "LblPercent";
            this.LblPercent.Size = new System.Drawing.Size(30, 17);
            this.LblPercent.TabIndex = 120;
            this.LblPercent.Text = "0 %";
            this.LblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblPercent.Visible = false;
            // 
            // LblStatus
            // 
            this.LblStatus.AutoSize = true;
            this.LblStatus.BackColor = System.Drawing.Color.Transparent;
            this.LblStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.LblStatus.ForeColor = System.Drawing.Color.Gray;
            this.LblStatus.Location = new System.Drawing.Point(55, 182);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(122, 17);
            this.LblStatus.TabIndex = 119;
            this.LblStatus.Text = "Loading Program....";
            // 
            // LblVersion
            // 
            this.LblVersion.BackColor = System.Drawing.Color.Transparent;
            this.LblVersion.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.LblVersion.ForeColor = System.Drawing.Color.Gray;
            this.LblVersion.Location = new System.Drawing.Point(552, 183);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(84, 16);
            this.LblVersion.TabIndex = 118;
            this.LblVersion.Text = "Ver.xx.xx.xx";
            this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTitle
            // 
            this.LblTitle.BackColor = System.Drawing.Color.Transparent;
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold);
            this.LblTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.LblTitle.Location = new System.Drawing.Point(12, 48);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(668, 71);
            this.LblTitle.TabIndex = 117;
            this.LblTitle.Text = "Vision System";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoadingBar
            // 
            this.LoadingBar.Location = new System.Drawing.Point(52, 203);
            this.LoadingBar.Name = "LoadingBar";
            this.LoadingBar.Size = new System.Drawing.Size(587, 24);
            this.LoadingBar.Step = 1;
            this.LoadingBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.LoadingBar.TabIndex = 116;
            // 
            // SplashForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(692, 420);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LblPercent);
            this.Controls.Add(this.LblStatus);
            this.Controls.Add(this.LblVersion);
            this.Controls.Add(this.LblTitle);
            this.Controls.Add(this.LoadingBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplashForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashForm";
            this.Load += new System.EventHandler(this.SplashForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label LblPercent;
        public System.Windows.Forms.Label LblStatus;
        private System.Windows.Forms.Label LblVersion;
        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.ProgressBar LoadingBar;
    }
}