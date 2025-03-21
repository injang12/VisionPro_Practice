namespace VisionSystem
{
    class SplashManager
    {
        public static SplashManager Instance { get; private set; } = new SplashManager();
        readonly SplashForm Splash = SplashForm.Instance;

        delegate void CloseCallback();
        delegate void UpdateProgressCallback(int value);
        delegate void SetStatusCallback(string msg);

        readonly System.Threading.Thread SplashThread;
        readonly System.Threading.EventWaitHandle loaded = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset);

        public SplashManager() => SplashThread = new System.Threading.Thread(new System.Threading.ThreadStart(RunSplash));
        public void UpdateLoadingBar(int value) => Splash.Invoke(new UpdateProgressCallback(Splash.UpdateProgressBar), value);
        public void SetStatus(string msg) => Splash.Invoke(new SetStatusCallback(Splash.SetStatus), msg);
        public void Close() => Splash.Invoke(new CloseCallback(Splash.Close));
        public void Join() => SplashThread.Join();

        public void Start()
        {
            SplashThread.Start();
            loaded.WaitOne();
        }

        void RunSplash()
        {
            Splash.Load += new System.EventHandler(OnLoad);
            Splash.ShowDialog();
        }

        void OnLoad(object sender, System.EventArgs e) => loaded.Set();
    }
}