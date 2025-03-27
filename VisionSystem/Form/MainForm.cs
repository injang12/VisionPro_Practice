namespace VisionSystem
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        public MainForm() => InitializeComponent();
        void MainForm_Load(object sender, System.EventArgs e) => FileManager.Instance.ParamLoad(SetupForm.Instance.PDisplay, SetupForm.Instance.ADisplay);
        void BtnExit_Click(object sender, System.EventArgs e) => this.Dispose();
        void BtnSetup_Click(object sender, System.EventArgs e) => SetupForm.Instance.ShowDialog();
        void BtnFolder_Click(object sender, System.EventArgs e) => FileManager.Instance.SpecifyFolder(LogList, false);
        void BtnInsp_Click(object sender, System.EventArgs e) => ToolManager.Instance.AutoInsp(MDisplay, LogList, BtnSetup, BtnFolder, BtnExit, TxtCurrentImage);
        void InspDelay_ValueChanged(object sender, System.EventArgs e) => DataStore.InspDelay = (double)InspDelay.Value;
    }
}