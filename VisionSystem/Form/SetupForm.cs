namespace VisionSystem
{
    public partial class SetupForm : System.Windows.Forms.Form
    {
        public static SetupForm Instance { get; private set; } = new SetupForm();

        readonly ToolManager.PMAlign Pattern = ToolManager.PMAlign.Instance;
        readonly FileManager FManager = FileManager.Instance;

        public SetupForm() => InitializeComponent();
        void SetupForm_Load(object sender, System.EventArgs e) => this.CenterToScreen();
        void BtnFolder_Click(object sender, System.EventArgs e) => FManager.ImageChange(SDisplay, LogList, LabelImageName, true);
        void TurnImage(object sender, System.EventArgs e) => FManager.ImageChange(SDisplay, LogList, LabelImageName, false, (System.Windows.Forms.Button)sender);
        void BtnSave_Click(object sender, System.EventArgs e) => FManager.ParamSave(LogList);
        void BtnExit_Click(object sender, System.EventArgs e) => this.Hide();
        void SetupDisplay_DoubleClick(object sender, System.EventArgs e) => FManager.Load_Image(SDisplay, LogList);
        void Numeric_ValueChanged(object sender, System.EventArgs e) => Util.ValueChange((System.Windows.Forms.NumericUpDown)sender);
        void BtnRegion_Click(object sender, System.EventArgs e) => GraphicManager.InitRegion(SDisplay, (System.Windows.Forms.Button)sender);
        void BtnTrain_Click(object sender, System.EventArgs e) => Pattern.Train(SDisplay, PDisplay, ADisplay, LogList, (System.Windows.Forms.Button)sender);
        void BtnRun_Click(object sender, System.EventArgs e) => Pattern.Run(SDisplay, (sender as System.Windows.Forms.Control)?.Name, "Run", LogList);
        void BtnManualRun_Click(object sender, System.EventArgs e) => ToolManager.RunManual(SDisplay, LogList);
        void SelectChkBox(object sender, System.EventArgs e) => Util.ShowGroupBox(SDisplay, (System.Windows.Forms.CheckBox)sender, ChkAngle, ChkPoint, PGroup, AGroup);
    }
}