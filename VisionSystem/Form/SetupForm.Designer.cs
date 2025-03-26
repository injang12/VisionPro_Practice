namespace VisionSystem
{
    partial class SetupForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.BtnPreImage = new System.Windows.Forms.Button();
            this.BtnFolder = new System.Windows.Forms.Button();
            this.BtnNextImage = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.BtnManualRun = new System.Windows.Forms.Button();
            this.RightBtnPanel = new System.Windows.Forms.Panel();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.SDisplay = new Cognex.VisionPro.CogRecordDisplay();
            this.panel3 = new System.Windows.Forms.Panel();
            this.LogList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtCurrentImage = new System.Windows.Forms.Label();
            this.ChkPoint = new System.Windows.Forms.CheckBox();
            this.ChkAngle = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PGroup = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PDisplay = new Cognex.VisionPro.CogRecordDisplay();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnPTrainRegion = new System.Windows.Forms.Button();
            this.BtnPTrain = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.NumPAngle = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.NumPThreshold = new System.Windows.Forms.NumericUpDown();
            this.BtnPRun = new System.Windows.Forms.Button();
            this.BtnPRegion = new System.Windows.Forms.Button();
            this.AGroup = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ADisplay = new Cognex.VisionPro.CogRecordDisplay();
            this.label8 = new System.Windows.Forms.Label();
            this.BtnATrainRegion = new System.Windows.Forms.Button();
            this.BtnATrain = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.NumAAngle = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.NumAThreshold = new System.Windows.Forms.NumericUpDown();
            this.BtnARun = new System.Windows.Forms.Button();
            this.BtnARegion = new System.Windows.Forms.Button();
            this.PnlPattern = new System.Windows.Forms.Panel();
            this.TopPanel.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.RightBtnPanel.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SDisplay)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.PGroup.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumPAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumPThreshold)).BeginInit();
            this.AGroup.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ADisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumAAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumAThreshold)).BeginInit();
            this.PnlPattern.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.TopPanel.Controls.Add(this.panel5);
            this.TopPanel.Controls.Add(this.panel4);
            this.TopPanel.Controls.Add(this.RightBtnPanel);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(1680, 56);
            this.TopPanel.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel5.Controls.Add(this.BtnPreImage);
            this.panel5.Controls.Add(this.BtnFolder);
            this.panel5.Controls.Add(this.BtnNextImage);
            this.panel5.Location = new System.Drawing.Point(1037, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(289, 56);
            this.panel5.TabIndex = 6;
            // 
            // BtnPreImage
            // 
            this.BtnPreImage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnPreImage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BtnPreImage.ForeColor = System.Drawing.Color.White;
            this.BtnPreImage.Location = new System.Drawing.Point(104, 0);
            this.BtnPreImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnPreImage.Name = "BtnPreImage";
            this.BtnPreImage.Size = new System.Drawing.Size(88, 56);
            this.BtnPreImage.TabIndex = 6;
            this.BtnPreImage.Text = "Pre\r\nImage";
            this.BtnPreImage.UseVisualStyleBackColor = true;
            this.BtnPreImage.Click += new System.EventHandler(this.BtnTurnImage_Click);
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
            // BtnNextImage
            // 
            this.BtnNextImage.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnNextImage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnNextImage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BtnNextImage.ForeColor = System.Drawing.Color.White;
            this.BtnNextImage.Location = new System.Drawing.Point(201, 0);
            this.BtnNextImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnNextImage.Name = "BtnNextImage";
            this.BtnNextImage.Size = new System.Drawing.Size(88, 56);
            this.BtnNextImage.TabIndex = 2;
            this.BtnNextImage.Text = "Next\r\nImage";
            this.BtnNextImage.UseVisualStyleBackColor = true;
            this.BtnNextImage.Click += new System.EventHandler(this.BtnTurnImage_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.BtnManualRun);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(1356, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(120, 56);
            this.panel4.TabIndex = 3;
            // 
            // BtnManualRun
            // 
            this.BtnManualRun.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnManualRun.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnManualRun.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BtnManualRun.ForeColor = System.Drawing.Color.White;
            this.BtnManualRun.Location = new System.Drawing.Point(0, 0);
            this.BtnManualRun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnManualRun.Name = "BtnManualRun";
            this.BtnManualRun.Size = new System.Drawing.Size(75, 56);
            this.BtnManualRun.TabIndex = 2;
            this.BtnManualRun.Text = "Manual\r\nRun";
            this.BtnManualRun.UseVisualStyleBackColor = true;
            this.BtnManualRun.Click += new System.EventHandler(this.BtnManualRun_Click);
            // 
            // RightBtnPanel
            // 
            this.RightBtnPanel.Controls.Add(this.BtnSave);
            this.RightBtnPanel.Controls.Add(this.BtnExit);
            this.RightBtnPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightBtnPanel.Location = new System.Drawing.Point(1476, 0);
            this.RightBtnPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RightBtnPanel.Name = "RightBtnPanel";
            this.RightBtnPanel.Size = new System.Drawing.Size(204, 56);
            this.RightBtnPanel.TabIndex = 2;
            // 
            // BtnSave
            // 
            this.BtnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnSave.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BtnSave.ForeColor = System.Drawing.Color.White;
            this.BtnSave.Location = new System.Drawing.Point(0, 0);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(61, 56);
            this.BtnSave.TabIndex = 2;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnExit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BtnExit.ForeColor = System.Drawing.Color.White;
            this.BtnExit.Location = new System.Drawing.Point(143, 0);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(61, 56);
            this.BtnExit.TabIndex = 1;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.SDisplay);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 56);
            this.LeftPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(1037, 808);
            this.LeftPanel.TabIndex = 3;
            // 
            // SDisplay
            // 
            this.SDisplay.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.SDisplay.ColorMapLowerRoiLimit = 0D;
            this.SDisplay.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.SDisplay.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.SDisplay.ColorMapUpperRoiLimit = 1D;
            this.SDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SDisplay.DoubleTapZoomCycleLength = 2;
            this.SDisplay.DoubleTapZoomSensitivity = 2.5D;
            this.SDisplay.Location = new System.Drawing.Point(0, 0);
            this.SDisplay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SDisplay.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.SDisplay.MouseWheelSensitivity = 1D;
            this.SDisplay.Name = "SDisplay";
            this.SDisplay.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("SDisplay.OcxState")));
            this.SDisplay.Size = new System.Drawing.Size(1037, 808);
            this.SDisplay.TabIndex = 1;
            this.SDisplay.DoubleClick += new System.EventHandler(this.SetupDisplay_DoubleClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.LogList);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(1037, 680);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(643, 184);
            this.panel3.TabIndex = 6;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label2.Location = new System.Drawing.Point(1046, 650);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 21);
            this.label2.TabIndex = 7;
            this.label2.Text = "Current Image :";
            // 
            // TxtCurrentImage
            // 
            this.TxtCurrentImage.AutoSize = true;
            this.TxtCurrentImage.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.TxtCurrentImage.Location = new System.Drawing.Point(1164, 650);
            this.TxtCurrentImage.Name = "TxtCurrentImage";
            this.TxtCurrentImage.Size = new System.Drawing.Size(39, 21);
            this.TxtCurrentImage.TabIndex = 8;
            this.TxtCurrentImage.Text = "Null";
            // 
            // ChkPoint
            // 
            this.ChkPoint.AutoSize = true;
            this.ChkPoint.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ChkPoint.Location = new System.Drawing.Point(17, 20);
            this.ChkPoint.Name = "ChkPoint";
            this.ChkPoint.Size = new System.Drawing.Size(113, 25);
            this.ChkPoint.TabIndex = 9;
            this.ChkPoint.Text = "PointPattern";
            this.ChkPoint.UseVisualStyleBackColor = true;
            this.ChkPoint.CheckedChanged += new System.EventHandler(this.ChkBox_Check);
            // 
            // ChkAngle
            // 
            this.ChkAngle.AutoSize = true;
            this.ChkAngle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ChkAngle.Location = new System.Drawing.Point(17, 61);
            this.ChkAngle.Name = "ChkAngle";
            this.ChkAngle.Size = new System.Drawing.Size(118, 25);
            this.ChkAngle.TabIndex = 11;
            this.ChkAngle.Text = "AnglePattern";
            this.ChkAngle.UseVisualStyleBackColor = true;
            this.ChkAngle.CheckedChanged += new System.EventHandler(this.ChkBox_Check);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkPoint);
            this.groupBox1.Controls.Add(this.ChkAngle);
            this.groupBox1.Location = new System.Drawing.Point(1057, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(156, 109);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // PGroup
            // 
            this.PGroup.Controls.Add(this.label10);
            this.PGroup.Controls.Add(this.panel1);
            this.PGroup.Controls.Add(this.label1);
            this.PGroup.Controls.Add(this.BtnPTrainRegion);
            this.PGroup.Controls.Add(this.BtnPTrain);
            this.PGroup.Controls.Add(this.groupBox2);
            this.PGroup.Controls.Add(this.label9);
            this.PGroup.Controls.Add(this.label4);
            this.PGroup.Controls.Add(this.NumPAngle);
            this.PGroup.Controls.Add(this.label3);
            this.PGroup.Controls.Add(this.NumPThreshold);
            this.PGroup.Controls.Add(this.BtnPRun);
            this.PGroup.Controls.Add(this.BtnPRegion);
            this.PGroup.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.PGroup.Location = new System.Drawing.Point(38, 24);
            this.PGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PGroup.Name = "PGroup";
            this.PGroup.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PGroup.Size = new System.Drawing.Size(508, 268);
            this.PGroup.TabIndex = 15;
            this.PGroup.TabStop = false;
            this.PGroup.Text = "Point Pattern";
            this.PGroup.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(360, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 30);
            this.label10.TabIndex = 21;
            this.label10.Text = "Train";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PDisplay);
            this.panel1.Location = new System.Drawing.Point(369, 141);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(131, 120);
            this.panel1.TabIndex = 6;
            // 
            // PDisplay
            // 
            this.PDisplay.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.PDisplay.ColorMapLowerRoiLimit = 0D;
            this.PDisplay.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.PDisplay.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.PDisplay.ColorMapUpperRoiLimit = 1D;
            this.PDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PDisplay.DoubleTapZoomCycleLength = 2;
            this.PDisplay.DoubleTapZoomSensitivity = 2.5D;
            this.PDisplay.Location = new System.Drawing.Point(0, 0);
            this.PDisplay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PDisplay.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.PDisplay.MouseWheelSensitivity = 1D;
            this.PDisplay.Name = "PDisplay";
            this.PDisplay.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("PDisplay.OcxState")));
            this.PDisplay.Size = new System.Drawing.Size(131, 120);
            this.PDisplay.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(383, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "Train Pattern";
            // 
            // BtnPTrainRegion
            // 
            this.BtnPTrainRegion.BackColor = System.Drawing.Color.White;
            this.BtnPTrainRegion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnPTrainRegion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BtnPTrainRegion.Location = new System.Drawing.Point(293, 159);
            this.BtnPTrainRegion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnPTrainRegion.Name = "BtnPTrainRegion";
            this.BtnPTrainRegion.Size = new System.Drawing.Size(68, 26);
            this.BtnPTrainRegion.TabIndex = 9;
            this.BtnPTrainRegion.Text = "Region";
            this.BtnPTrainRegion.UseVisualStyleBackColor = false;
            this.BtnPTrainRegion.Click += new System.EventHandler(this.BtnRegion_Click);
            // 
            // BtnPTrain
            // 
            this.BtnPTrain.BackColor = System.Drawing.Color.White;
            this.BtnPTrain.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnPTrain.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BtnPTrain.Location = new System.Drawing.Point(293, 190);
            this.BtnPTrain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnPTrain.Name = "BtnPTrain";
            this.BtnPTrain.Size = new System.Drawing.Size(68, 26);
            this.BtnPTrain.TabIndex = 8;
            this.BtnPTrain.Text = "Train";
            this.BtnPTrain.UseVisualStyleBackColor = false;
            this.BtnPTrain.Click += new System.EventHandler(this.BtnTrain_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(267, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(11, 268);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(78, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 30);
            this.label9.TabIndex = 19;
            this.label9.Text = "Search";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 21);
            this.label4.TabIndex = 18;
            this.label4.Text = "Angle";
            // 
            // NumPAngle
            // 
            this.NumPAngle.Location = new System.Drawing.Point(129, 81);
            this.NumPAngle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NumPAngle.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.NumPAngle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumPAngle.Name = "NumPAngle";
            this.NumPAngle.Size = new System.Drawing.Size(72, 29);
            this.NumPAngle.TabIndex = 17;
            this.NumPAngle.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.NumPAngle.ValueChanged += new System.EventHandler(this.Numeric_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 21);
            this.label3.TabIndex = 16;
            this.label3.Text = "Threshold";
            // 
            // NumPThreshold
            // 
            this.NumPThreshold.DecimalPlaces = 1;
            this.NumPThreshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NumPThreshold.Location = new System.Drawing.Point(129, 114);
            this.NumPThreshold.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NumPThreshold.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumPThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NumPThreshold.Name = "NumPThreshold";
            this.NumPThreshold.Size = new System.Drawing.Size(72, 29);
            this.NumPThreshold.TabIndex = 15;
            this.NumPThreshold.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.NumPThreshold.ValueChanged += new System.EventHandler(this.Numeric_ValueChanged);
            // 
            // BtnPRun
            // 
            this.BtnPRun.BackColor = System.Drawing.Color.White;
            this.BtnPRun.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnPRun.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.BtnPRun.Location = new System.Drawing.Point(42, 190);
            this.BtnPRun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnPRun.Name = "BtnPRun";
            this.BtnPRun.Size = new System.Drawing.Size(68, 26);
            this.BtnPRun.TabIndex = 12;
            this.BtnPRun.Text = "Run";
            this.BtnPRun.UseVisualStyleBackColor = false;
            this.BtnPRun.Click += new System.EventHandler(this.BtnRun_Click);
            // 
            // BtnPRegion
            // 
            this.BtnPRegion.BackColor = System.Drawing.Color.White;
            this.BtnPRegion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnPRegion.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.BtnPRegion.Location = new System.Drawing.Point(42, 159);
            this.BtnPRegion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnPRegion.Name = "BtnPRegion";
            this.BtnPRegion.Size = new System.Drawing.Size(68, 26);
            this.BtnPRegion.TabIndex = 11;
            this.BtnPRegion.Text = "Region";
            this.BtnPRegion.UseVisualStyleBackColor = false;
            this.BtnPRegion.Click += new System.EventHandler(this.BtnRegion_Click);
            // 
            // AGroup
            // 
            this.AGroup.Controls.Add(this.panel2);
            this.AGroup.Controls.Add(this.label8);
            this.AGroup.Controls.Add(this.BtnATrainRegion);
            this.AGroup.Controls.Add(this.BtnATrain);
            this.AGroup.Controls.Add(this.label11);
            this.AGroup.Controls.Add(this.groupBox3);
            this.AGroup.Controls.Add(this.label12);
            this.AGroup.Controls.Add(this.label5);
            this.AGroup.Controls.Add(this.NumAAngle);
            this.AGroup.Controls.Add(this.label6);
            this.AGroup.Controls.Add(this.NumAThreshold);
            this.AGroup.Controls.Add(this.BtnARun);
            this.AGroup.Controls.Add(this.BtnARegion);
            this.AGroup.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.AGroup.Location = new System.Drawing.Point(38, 296);
            this.AGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AGroup.Name = "AGroup";
            this.AGroup.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AGroup.Size = new System.Drawing.Size(508, 268);
            this.AGroup.TabIndex = 16;
            this.AGroup.TabStop = false;
            this.AGroup.Text = "Angle Pattern";
            this.AGroup.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ADisplay);
            this.panel2.Location = new System.Drawing.Point(369, 141);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(131, 120);
            this.panel2.TabIndex = 25;
            // 
            // ADisplay
            // 
            this.ADisplay.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.ADisplay.ColorMapLowerRoiLimit = 0D;
            this.ADisplay.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.ADisplay.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.ADisplay.ColorMapUpperRoiLimit = 1D;
            this.ADisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ADisplay.DoubleTapZoomCycleLength = 2;
            this.ADisplay.DoubleTapZoomSensitivity = 2.5D;
            this.ADisplay.Location = new System.Drawing.Point(0, 0);
            this.ADisplay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ADisplay.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.ADisplay.MouseWheelSensitivity = 1D;
            this.ADisplay.Name = "ADisplay";
            this.ADisplay.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("ADisplay.OcxState")));
            this.ADisplay.Size = new System.Drawing.Size(131, 120);
            this.ADisplay.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(383, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 21);
            this.label8.TabIndex = 7;
            this.label8.Text = "Train Pattern";
            // 
            // BtnATrainRegion
            // 
            this.BtnATrainRegion.BackColor = System.Drawing.Color.White;
            this.BtnATrainRegion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnATrainRegion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BtnATrainRegion.Location = new System.Drawing.Point(293, 159);
            this.BtnATrainRegion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnATrainRegion.Name = "BtnATrainRegion";
            this.BtnATrainRegion.Size = new System.Drawing.Size(68, 26);
            this.BtnATrainRegion.TabIndex = 9;
            this.BtnATrainRegion.Text = "Region";
            this.BtnATrainRegion.UseVisualStyleBackColor = false;
            this.BtnATrainRegion.Click += new System.EventHandler(this.BtnRegion_Click);
            // 
            // BtnATrain
            // 
            this.BtnATrain.BackColor = System.Drawing.Color.White;
            this.BtnATrain.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnATrain.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BtnATrain.Location = new System.Drawing.Point(293, 190);
            this.BtnATrain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnATrain.Name = "BtnATrain";
            this.BtnATrain.Size = new System.Drawing.Size(68, 26);
            this.BtnATrain.TabIndex = 8;
            this.BtnATrain.Text = "Train";
            this.BtnATrain.UseVisualStyleBackColor = false;
            this.BtnATrain.Click += new System.EventHandler(this.BtnTrain_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(359, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 30);
            this.label11.TabIndex = 24;
            this.label11.Text = "Train";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(267, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(11, 268);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(78, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 30);
            this.label12.TabIndex = 22;
            this.label12.Text = "Search";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 21);
            this.label5.TabIndex = 18;
            this.label5.Text = "Angle";
            // 
            // NumAAngle
            // 
            this.NumAAngle.Location = new System.Drawing.Point(129, 81);
            this.NumAAngle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NumAAngle.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.NumAAngle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumAAngle.Name = "NumAAngle";
            this.NumAAngle.Size = new System.Drawing.Size(72, 29);
            this.NumAAngle.TabIndex = 17;
            this.NumAAngle.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.NumAAngle.ValueChanged += new System.EventHandler(this.Numeric_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 21);
            this.label6.TabIndex = 16;
            this.label6.Text = "Threshold";
            // 
            // NumAThreshold
            // 
            this.NumAThreshold.DecimalPlaces = 1;
            this.NumAThreshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NumAThreshold.Location = new System.Drawing.Point(129, 114);
            this.NumAThreshold.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NumAThreshold.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumAThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NumAThreshold.Name = "NumAThreshold";
            this.NumAThreshold.Size = new System.Drawing.Size(72, 29);
            this.NumAThreshold.TabIndex = 15;
            this.NumAThreshold.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.NumAThreshold.ValueChanged += new System.EventHandler(this.Numeric_ValueChanged);
            // 
            // BtnARun
            // 
            this.BtnARun.BackColor = System.Drawing.Color.White;
            this.BtnARun.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnARun.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.BtnARun.Location = new System.Drawing.Point(42, 190);
            this.BtnARun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnARun.Name = "BtnARun";
            this.BtnARun.Size = new System.Drawing.Size(68, 26);
            this.BtnARun.TabIndex = 12;
            this.BtnARun.Text = "Run";
            this.BtnARun.UseVisualStyleBackColor = false;
            this.BtnARun.Click += new System.EventHandler(this.BtnRun_Click);
            // 
            // BtnARegion
            // 
            this.BtnARegion.BackColor = System.Drawing.Color.White;
            this.BtnARegion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnARegion.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.BtnARegion.Location = new System.Drawing.Point(42, 159);
            this.BtnARegion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnARegion.Name = "BtnARegion";
            this.BtnARegion.Size = new System.Drawing.Size(68, 26);
            this.BtnARegion.TabIndex = 11;
            this.BtnARegion.Text = "Region";
            this.BtnARegion.UseVisualStyleBackColor = false;
            this.BtnARegion.Click += new System.EventHandler(this.BtnRegion_Click);
            // 
            // PnlPattern
            // 
            this.PnlPattern.Controls.Add(this.PGroup);
            this.PnlPattern.Controls.Add(this.AGroup);
            this.PnlPattern.Location = new System.Drawing.Point(1047, 191);
            this.PnlPattern.Name = "PnlPattern";
            this.PnlPattern.Size = new System.Drawing.Size(621, 427);
            this.PnlPattern.TabIndex = 17;
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.ClientSize = new System.Drawing.Size(1680, 864);
            this.Controls.Add(this.PnlPattern);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TxtCurrentImage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.LeftPanel);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SetupForm";
            this.Text = "SetupForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SetupForm_Load);
            this.TopPanel.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.RightBtnPanel.ResumeLayout(false);
            this.LeftPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SDisplay)).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.PGroup.ResumeLayout(false);
            this.PGroup.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumPAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumPThreshold)).EndInit();
            this.AGroup.ResumeLayout(false);
            this.AGroup.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ADisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumAAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumAThreshold)).EndInit();
            this.PnlPattern.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel RightBtnPanel;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Panel LeftPanel;
        public Cognex.VisionPro.CogRecordDisplay SDisplay;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListBox LogList;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button BtnManualRun;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button BtnFolder;
        private System.Windows.Forms.Button BtnNextImage;
        private System.Windows.Forms.Button BtnPreImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label TxtCurrentImage;
        private System.Windows.Forms.CheckBox ChkPoint;
        private System.Windows.Forms.CheckBox ChkAngle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox PGroup;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        public Cognex.VisionPro.CogRecordDisplay PDisplay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnPTrainRegion;
        private System.Windows.Forms.Button BtnPTrain;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown NumPAngle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown NumPThreshold;
        private System.Windows.Forms.Button BtnPRun;
        private System.Windows.Forms.Button BtnPRegion;
        private System.Windows.Forms.GroupBox AGroup;
        private System.Windows.Forms.Panel panel2;
        public Cognex.VisionPro.CogRecordDisplay ADisplay;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button BtnATrainRegion;
        private System.Windows.Forms.Button BtnATrain;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown NumAAngle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown NumAThreshold;
        private System.Windows.Forms.Button BtnARun;
        private System.Windows.Forms.Button BtnARegion;
        private System.Windows.Forms.Panel PnlPattern;
    }
}