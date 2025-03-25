namespace VisionSystem
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BtnFolder = new System.Windows.Forms.Button();
            this.BtnInsp = new System.Windows.Forms.Button();
            this.RightBtnPanel2 = new System.Windows.Forms.Panel();
            this.BtnSetup = new System.Windows.Forms.Button();
            this.RightBtnPanel = new System.Windows.Forms.Panel();
            this.BtnExit = new System.Windows.Forms.Button();
            this.MDisplay = new Cognex.VisionPro.CogRecordDisplay();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.LogList = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.InspDelay = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCurrentImage = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TopPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.RightBtnPanel2.SuspendLayout();
            this.RightBtnPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MDisplay)).BeginInit();
            this.LeftPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InspDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.TopPanel.Controls.Add(this.panel2);
            this.TopPanel.Controls.Add(this.RightBtnPanel2);
            this.TopPanel.Controls.Add(this.RightBtnPanel);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(1680, 56);
            this.TopPanel.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel2.Controls.Add(this.BtnFolder);
            this.panel2.Controls.Add(this.BtnInsp);
            this.panel2.Location = new System.Drawing.Point(1037, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(192, 56);
            this.panel2.TabIndex = 5;
            // 
            // BtnFolder
            // 
            this.BtnFolder.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnFolder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BtnFolder.ForeColor = System.Drawing.Color.White;
            this.BtnFolder.Location = new System.Drawing.Point(0, 0);
            this.BtnFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnFolder.Name = "BtnFolder";
            this.BtnFolder.Size = new System.Drawing.Size(88, 56);
            this.BtnFolder.TabIndex = 5;
            this.BtnFolder.Text = "Specify\r\nFolder";
            this.BtnFolder.UseVisualStyleBackColor = true;
            this.BtnFolder.Click += new System.EventHandler(this.BtnFolder_Click);
            // 
            // BtnInsp
            // 
            this.BtnInsp.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnInsp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnInsp.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BtnInsp.ForeColor = System.Drawing.Color.White;
            this.BtnInsp.Location = new System.Drawing.Point(104, 0);
            this.BtnInsp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnInsp.Name = "BtnInsp";
            this.BtnInsp.Size = new System.Drawing.Size(88, 56);
            this.BtnInsp.TabIndex = 2;
            this.BtnInsp.Text = "Auto\r\nInspection";
            this.BtnInsp.UseVisualStyleBackColor = true;
            this.BtnInsp.Click += new System.EventHandler(this.BtnInsp_Click);
            // 
            // RightBtnPanel2
            // 
            this.RightBtnPanel2.Controls.Add(this.BtnSetup);
            this.RightBtnPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightBtnPanel2.Location = new System.Drawing.Point(1400, 0);
            this.RightBtnPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RightBtnPanel2.Name = "RightBtnPanel2";
            this.RightBtnPanel2.Size = new System.Drawing.Size(140, 56);
            this.RightBtnPanel2.TabIndex = 3;
            // 
            // BtnSetup
            // 
            this.BtnSetup.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnSetup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnSetup.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BtnSetup.ForeColor = System.Drawing.Color.White;
            this.BtnSetup.Location = new System.Drawing.Point(79, 0);
            this.BtnSetup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnSetup.Name = "BtnSetup";
            this.BtnSetup.Size = new System.Drawing.Size(61, 56);
            this.BtnSetup.TabIndex = 2;
            this.BtnSetup.Text = "Setup";
            this.BtnSetup.UseVisualStyleBackColor = true;
            this.BtnSetup.Click += new System.EventHandler(this.BtnSetup_Click);
            // 
            // RightBtnPanel
            // 
            this.RightBtnPanel.Controls.Add(this.BtnExit);
            this.RightBtnPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightBtnPanel.Location = new System.Drawing.Point(1540, 0);
            this.RightBtnPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RightBtnPanel.Name = "RightBtnPanel";
            this.RightBtnPanel.Size = new System.Drawing.Size(140, 56);
            this.RightBtnPanel.TabIndex = 2;
            // 
            // BtnExit
            // 
            this.BtnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnExit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BtnExit.ForeColor = System.Drawing.Color.White;
            this.BtnExit.Location = new System.Drawing.Point(79, 0);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(61, 56);
            this.BtnExit.TabIndex = 1;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // MDisplay
            // 
            this.MDisplay.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.MDisplay.ColorMapLowerRoiLimit = 0D;
            this.MDisplay.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.MDisplay.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.MDisplay.ColorMapUpperRoiLimit = 1D;
            this.MDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MDisplay.DoubleTapZoomCycleLength = 2;
            this.MDisplay.DoubleTapZoomSensitivity = 2.5D;
            this.MDisplay.Location = new System.Drawing.Point(0, 0);
            this.MDisplay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MDisplay.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.MDisplay.MouseWheelSensitivity = 1D;
            this.MDisplay.Name = "MDisplay";
            this.MDisplay.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MDisplay.OcxState")));
            this.MDisplay.Size = new System.Drawing.Size(1037, 808);
            this.MDisplay.TabIndex = 1;
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.MDisplay);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 56);
            this.LeftPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(1037, 808);
            this.LeftPanel.TabIndex = 2;
            // 
            // LogList
            // 
            this.LogList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogList.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.LogList.FormattingEnabled = true;
            this.LogList.HorizontalScrollbar = true;
            this.LogList.ItemHeight = 20;
            this.LogList.Location = new System.Drawing.Point(0, 0);
            this.LogList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LogList.Name = "LogList";
            this.LogList.Size = new System.Drawing.Size(643, 184);
            this.LogList.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LogList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(1037, 680);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(643, 184);
            this.panel1.TabIndex = 4;
            // 
            // InspDelay
            // 
            this.InspDelay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InspDelay.DecimalPlaces = 1;
            this.InspDelay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.InspDelay.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.InspDelay.Location = new System.Drawing.Point(1211, 75);
            this.InspDelay.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.InspDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.InspDelay.Name = "InspDelay";
            this.InspDelay.Size = new System.Drawing.Size(44, 19);
            this.InspDelay.TabIndex = 5;
            this.InspDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.InspDelay.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.InspDelay.ValueChanged += new System.EventHandler(this.InspDelay_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1042, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Auto Inspection Delay : ";
            // 
            // TxtCurrentImage
            // 
            this.TxtCurrentImage.AutoSize = true;
            this.TxtCurrentImage.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.TxtCurrentImage.Location = new System.Drawing.Point(1164, 650);
            this.TxtCurrentImage.Name = "TxtCurrentImage";
            this.TxtCurrentImage.Size = new System.Drawing.Size(39, 21);
            this.TxtCurrentImage.TabIndex = 10;
            this.TxtCurrentImage.Text = "Null";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label2.Location = new System.Drawing.Point(1046, 650);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "Current Image :";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.ClientSize = new System.Drawing.Size(1680, 864);
            this.Controls.Add(this.TxtCurrentImage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InspDelay);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.LeftPanel);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vision System";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.TopPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.RightBtnPanel2.ResumeLayout(false);
            this.RightBtnPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MDisplay)).EndInit();
            this.LeftPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InspDelay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Panel RightBtnPanel;
        private System.Windows.Forms.Button BtnSetup;
        private System.Windows.Forms.Panel RightBtnPanel2;
        private Cognex.VisionPro.CogRecordDisplay MDisplay;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.ListBox LogList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BtnInsp;
        private System.Windows.Forms.Button BtnFolder;
        private System.Windows.Forms.NumericUpDown InspDelay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label TxtCurrentImage;
        private System.Windows.Forms.Label label2;
    }
}

