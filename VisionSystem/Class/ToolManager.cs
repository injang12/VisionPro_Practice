using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;

using System.Drawing;
using System.Windows.Forms;

namespace VisionSystem
{
    internal class ToolManager
    {
        public static ToolManager Instance { get; private set; } = new ToolManager();
        readonly static PMAlign Pattern = PMAlign.Instance;
        readonly FileManager FManager = FileManager.Instance;

        bool isStart = false;       // 자동 검사 시작 체크
        bool isRunning = false;     // 자동 검사 스레드 동작 중 인지 체크 (자동 검사 Stop을 해도 스레드가 동작 중이면 에러 발생)
        bool isStop = false;        // 자동 검사 종료
        int count = 0;              // 자동 검사 이미지 카운팅

        /// <summary>
        /// 매뉴얼 패턴 실행 및 결과 그래픽 표시
        /// </summary>
        /// <param name="display">결과를 출력할 디스플레이</param>
        /// <param name="logList">로그 메시지를 출력할 ListBox</param>
        public static void RunManual(CogRecordDisplay display, ListBox logList)
        {
            Util.DisplayClear(display);
            if (!Pattern.Run(display, "BtnPRun", "", logList) || !Pattern.Run(display, "BtnARun", "ManualRun", logList)) return;

            CogTransform2DLinear p = DataStore.Pattern.PPattern.Results[0].GetPose();
            CogTransform2DLinear a = DataStore.Pattern.APattern.Results[0].GetPose();
            double x1 = p.TranslationX, y1 = p.TranslationY, x2 = a.TranslationX, y2 = a.TranslationY;

            double angle = Util.AngleCalculation(x1, y1, x2, y2);
            GraphicManager.CreateLabel(display, CogColorConstants.White, new Font("Segoe UI", 15f), $"Angle: {angle:0.00}", x2 - 200, y2 + 100, CogColorConstants.Black);
            GraphicManager.CreateCenterLineGraphics(display, x2, y2);
            GraphicManager.CreateSegmentGraphics(display, x1, y1, x2, y2);
        }

        /// <summary>
        /// 자동 검사 시작 및 중지 핸들링
        /// </summary>
        /// <param name="display">검사 이미지 디스플레이</param>
        /// <param name="logList">로그 출력 대상</param>
        /// <param name="btnSetup">Setup 버튼 (토글 대상)</param>
        /// <param name="btnFolder">폴더 선택 버튼 (토글 대상)</param>
        /// <param name="btnExit">종료 버튼 (토글 대상)</param>
        /// <param name="currentImageName">현재 이미지 파일명을 표시할 Label</param>
        public void AutoInsp(CogRecordDisplay display, ListBox logList, Button btnSetup, Button btnFolder, Button btnExit, Label currentImageName)
        {
            if (FManager.MainImageFileName.Count == 0)
            {
                Util.PrintLog(logList, "Image folder is not registered...");
                return;
            }

            if (isStart)
            {
                isStart = false;
                foreach (Button btn in new[] { btnFolder, btnSetup, btnExit })
                {
                    btn.Enabled = true;
                    btn.BackColor = Color.FromArgb(40, 40, 40);
                }
                Util.PrintLog(logList, "Stop automatic inspection...");
                return;
            }

            if (isRunning) return;

            isStart = true;
            isStop = false;
            foreach (Button btn in new[] { btnFolder, btnSetup, btnExit })
            {
                btn.Enabled = false;
                btn.BackColor = Color.Red;
            }
            Util.PrintLog(logList, "Start automatic inspection...");

            new System.Threading.Thread(() =>
            {
                while (isStart)
                {
                    StartAutoInsp(display, currentImageName);
                    if (isStop)
                    {
                        foreach (Button btn in new[] { btnFolder, btnSetup, btnExit })
                        {
                            btn.Invoke(new System.Action(() => {
                                btn.Enabled = true;
                                btn.BackColor = Color.FromArgb(40, 40, 40);
                            }));
                        }
                        logList.Invoke(new System.Action(() => Util.PrintLog(logList, "Automatic inspection completed!!")));
                    }
                }
                isRunning = false;
            }).Start();
        }

        /// <summary>
        /// 자동 검사 루프 내 단일 이미지 처리
        /// </summary>
        /// <param name="display">이미지를 표시할 디스플레이</param>
        /// <param name="currentImageName">현재 이미지 파일명을 표시할 Label</param>
        void StartAutoInsp(CogRecordDisplay display, Label currentImageName)
        {
            isRunning = true;
            if (FManager.MainImageFileName.Count <= count)
            {
                isStart = false;
                isStop = true;
                count = 0;
                return;
            }

            string file = FManager.MainImageFileName[count];
            ICogImage image = FManager.Load_ImageFile(file);
            currentImageName.Invoke(new System.Action(() => currentImageName.Text = System.IO.Path.GetFileName(file)));

            if (image is CogImage24PlanarColor)
                image = CogImageConvert.GetIntensityImage(image, 0, 0, image.Width, image.Height);

            display.Image = image;
            DataStore.Pattern.InputImage = image;
            count++;

            Util.DisplayClear(display);

            if (!Pattern.Run(display, "BtnPRun", null) || !Pattern.Run(display, "BtnARun", null)) return;

            CogTransform2DLinear p = DataStore.Pattern.PPattern.Results[0].GetPose();
            CogTransform2DLinear a = DataStore.Pattern.APattern.Results[0].GetPose();
            double angle = Util.AngleCalculation(p.TranslationX, p.TranslationY, a.TranslationX, a.TranslationY);

            GraphicManager.CreateLabel(display, CogColorConstants.White, new Font("Segoe UI", 15f), $"Angle: {angle:0.00}", a.TranslationX - 200, a.TranslationY + 100, CogColorConstants.Black);
            GraphicManager.CreateCenterLineGraphics(display, a.TranslationX, a.TranslationY);
            GraphicManager.CreateSegmentGraphics(display, p.TranslationX, p.TranslationY, a.TranslationX, a.TranslationY);

            System.Threading.Thread.Sleep((int)(DataStore.InspDelay * 1000));
        }

        public class PMAlign
        {
            public static PMAlign Instance { get; private set; } = new PMAlign();

            /// <summary>
            /// Point 또는 Angle 패턴 학습
            /// </summary>
            /// <param name="sDisplay">Setup 화면 디스플레이</param>
            /// <param name="pDisplay">Point 결과 디스플레이</param>
            /// <param name="aDisplay">Angle 결과 디스플레이</param>
            /// <param name="logList">로그 출력용 ListBox</param>
            /// <param name="btnTrain">트레인 버튼 (BtnPTrain or BtnATrain)</param>
            public void Train(CogRecordDisplay sDisplay, CogRecordDisplay pDisplay, CogRecordDisplay aDisplay, ListBox logList, Button btnTrain)
            {
                if (sDisplay.Image == null)
                {
                    Util.PrintLog(logList, "There is no image.");
                    return;
                }

                FileManager fManager = FileManager.Instance;
                string name = btnTrain.Name;
                bool isPoint = name == "BtnPTrain";
                CogRecordDisplay display = isPoint ? pDisplay : aDisplay;
                CogRectangleAffine region = isPoint ? DataStore.RectangleRegion.PTrainRegion : DataStore.RectangleRegion.ATrainRegion;
                bool alreadyTrained = isPoint ? DataStore.Pattern.PTrain : DataStore.Pattern.ATrain;
                string toolName = isPoint ? "Point" : name == "BtnATrain" ? "Angle" : null;

                if (toolName == null) return;

                if (alreadyTrained && MessageBox.Show("Do you want to overwrite the train image?", "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

                bool result = TrainRun(region, isPoint ? DataStore.Pattern.PPattern : DataStore.Pattern.APattern);

                if (isPoint) DataStore.Pattern.PTrain = result;
                else DataStore.Pattern.ATrain = result;

                Util.DisplayClear(sDisplay);

                string imagePath = System.IO.Path.Combine(Application.StartupPath, "Image");
                System.IO.Directory.CreateDirectory(imagePath);

                display.Image = DataStore.Pattern.TrainImage;
                fManager.Save_ImageFile(System.IO.Path.Combine(imagePath, $"{toolName}.bmp"), display.Image);
                fManager.Save_ImageFile(System.IO.Path.Combine(imagePath, "MasterImage.bmp"), sDisplay.Image);

                Util.PrintLog(logList, "Image Train and Save Complete!");
            }

            /// <summary>
            /// 지정된 툴 및 영역에 대해 패턴 학습 실행
            /// </summary>
            /// <param name="region">트레인에 사용할 영역</param>
            /// <param name="tool">대상 패턴 툴</param>
            /// <returns>트레인 성공 여부</returns>
            public bool TrainRun(CogRectangleAffine region, CogPMAlignTool tool)
            {
                if (DataStore.Pattern.InputImage == null) return false;

                try
                {
                    tool.Pattern.TrainImage = DataStore.Pattern.InputImage;
                    tool.Pattern.TrainRegion = region;
                    tool.Pattern.Origin.TranslationX = region.CenterX;
                    tool.Pattern.Origin.TranslationY = region.CenterY;
                    tool.Pattern.Train();
                    DataStore.Pattern.TrainImage = tool.Pattern.GetTrainedPatternImage();
                    return true;
                }
                catch { return false; }
            }

            /// <summary>
            /// Point 또는 Angle 패턴 실행 및 그래픽 표시
            /// </summary>
            /// <param name="display">디스플레이 객체</param>
            /// <param name="name">패턴 식별 버튼 이름 (BtnPRun / BtnARun)</param>
            /// <param name="mode">실행 모드 ("Run" / "ManualRun" / null)</param>
            /// <param name="logList">(옵션) 로그 출력용 ListBox</param>
            /// <returns>패턴 실행 성공 여부</returns>
            public bool Run(CogRecordDisplay display, string name, string mode, ListBox logList = null)
            {
                if (mode == "Run") Util.DisplayClear(display);
                if (display.Image == null)
                {
                    if (logList != null) Util.PrintLog(logList, "There is no image.");
                    return false;
                }

                CogPMAlignTool tool = null;
                CogRectangleAffine search = null;
                double angle = 0, threshold = 0;

                if (name == "BtnPRun")
                {
                    tool = DataStore.Pattern.PPattern;
                    search = DataStore.RectangleRegion.PRegion;
                    angle = DataStore.Pattern.PAngle;
                    threshold = DataStore.Pattern.PThreshold;
                }
                else if (name == "BtnARun")
                {
                    tool = DataStore.Pattern.APattern;
                    search = DataStore.RectangleRegion.ARegion;
                    angle = DataStore.Pattern.AAngle;
                    threshold = DataStore.Pattern.AThreshold;
                }

                if (tool == null || !tool.Pattern.Trained)
                {
                    if (logList != null) Util.PrintLog(logList, "There are no registered patterns.");
                    return false;
                }

                tool.InputImage = DataStore.Pattern.InputImage;
                tool.RunParams.ZoneAngle.Configuration = CogPMAlignZoneConstants.LowHigh;
                tool.RunParams.ZoneAngle.Low = CogMisc.DegToRad(-angle);
                tool.RunParams.ZoneAngle.High = CogMisc.DegToRad(angle);
                tool.RunParams.AcceptThreshold = threshold;
                tool.SearchRegion = search;
                tool.Run();

                if (!GraphicManager.PatternResultGraphics(display, tool, search)) return false;

                if (logList != null)
                {
                    if (mode == "Run") Util.PrintLog(logList, "Pattern run complete!");
                    else if (mode == "ManualRun") Util.PrintLog(logList, "Manual run complete!");
                }

                return true;
            }
        }
    }
}