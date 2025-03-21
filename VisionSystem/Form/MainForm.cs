﻿namespace VisionSystem
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        readonly SplashManager Splash = SplashManager.Instance;

        public MainForm()
        {
            Splash.Start();
            Splash.UpdateLoadingBar(30);
            Splash.SetStatus("Main form loading...");

            InitializeComponent();

            Splash.UpdateLoadingBar(80);
            Splash.SetStatus("Parameter loading...");
        }

        void MainForm_Load(object sender, System.EventArgs e) => FileManager.Instance.ParamLoad();
        void BtnExit_Click(object sender, System.EventArgs e) => this.Dispose();
        void BtnSetup_Click(object sender, System.EventArgs e) => SetupForm.Instance.ShowDialog();
        void BtnFolder_Click(object sender, System.EventArgs e) => FileManager.Instance.SpecifyFolder(LogList, false);
        void BtnInsp_Click(object sender, System.EventArgs e) => ToolManager.Instance.AutoInspection(MDisplay, LogList, BtnSetup, BtnFolder, BtnExit, TxtCurrentImage);
        void NumInspTime_ValueChanged(object sender, System.EventArgs e) => DataStore.Instance.InspDelay = (double)NumInspTime.Value;
    }
}