using System.Windows.Forms;

namespace VisionSystem
{
    public partial class SetupForm : Form
    {
        public static SetupForm Instance { get; private set; } = new SetupForm();

        readonly ToolManager.PMAlign PMAlign = ToolManager.PMAlign.Instance;
        readonly DataStore.Pattern Pattern = DataStore.Pattern.Instance;
        readonly ToolManager TManager = ToolManager.Instance;
        readonly FileManager FManager = FileManager.Instance;
        new readonly DataStore.Region Region = DataStore.Region.Instance;

        public SetupForm() => InitializeComponent();
        void SetupForm_Load(object sender, System.EventArgs e) => this.CenterToScreen();
        void BtnFolder_Click(object sender, System.EventArgs e) => FManager.TurnImageOver(SDisplay, LogList, TxtCurrentImage, true);
        void BtnTurnImage_Click(object sender, System.EventArgs e) => FManager.TurnImageOver(SDisplay, LogList, TxtCurrentImage, false, (Button)sender);
        void BtnSave_Click(object sender, System.EventArgs e) => FManager.ParamSave(LogList);
        void BtnExit_Click(object sender, System.EventArgs e) => this.Hide();
        void SetupDisplay_DoubleClick(object sender, System.EventArgs e) => FManager.Load_Image(SDisplay, LogList);
        void Numeric_ValueChanged(object sender, System.EventArgs e) => Utilities.ValueChange((NumericUpDown)sender);
        void BtnRegion_Click(object sender, System.EventArgs e) => GraphicManager.InitRegion(SDisplay, (Button)sender);
        void BtnTrain_Click(object sender, System.EventArgs e) => PMAlign.PatternTrain(SDisplay, PDisplay, ADisplay, LogList, (Button)sender);
        void BtnARun_Click(object sender, System.EventArgs e) => PMAlign.PatternRun(SDisplay, Pattern.APattern, Region.ARegion, Pattern.AAngle, Pattern.AThreshold, "Run", LogList);
        void BtnPRun_Click(object sender, System.EventArgs e) => PMAlign.PatternRun(SDisplay, Pattern.PPattern, Region.PRegion, Pattern.PAngle, Pattern.PThreshold, "Run", LogList);
        void BtnManualRun_Click(object sender, System.EventArgs e) => TManager.RunManual(SDisplay, LogList);
        void ChkBox_Check(object sender, System.EventArgs e) => Utilities.SelectedCheckBox(SDisplay, (CheckBox)sender, ChkAngle, ChkPoint, PGroup, AGroup);
    }
}