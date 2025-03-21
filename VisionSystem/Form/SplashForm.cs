namespace VisionSystem
{
    public partial class SplashForm : System.Windows.Forms.Form
    {
        public static SplashForm Instance { get; private set; } = new SplashForm();
        public SplashForm() => InitializeComponent();
        public void UpdateProgressBar(int value) => LoadingBar.Value = value;
        public void SetStatus(string msg) => LblStatus.Text = msg;
        void SplashForm_Load(object sender, System.EventArgs e) => LblVersion.Text = "Ver " + System.Windows.Forms.Application.ProductVersion;
    }
}