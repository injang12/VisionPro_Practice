using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;

using System;
using System.IO;
using System.Drawing;
using System.Threading;
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
        /// 매뉴얼 런
        /// </summary>
        /// <param name="Display">지정 할 디스플레이</param>
        /// <param name="LogList">로그 창 지정</param>
        public static void RunManual(CogRecordDisplay Display, ListBox LogList)
        {
            Utilities.DisplayClear(Display);

            if (!Pattern.PatternRun(Display, "BtnPRun", "", LogList)) return;
            if (!Pattern.PatternRun(Display, "BtnARun", "ManualRun", LogList)) return;

            double x1 = DataStore.Pattern.PPattern.Results[0].GetPose().TranslationX;
            double y1 = DataStore.Pattern.PPattern.Results[0].GetPose().TranslationY;
            double x2 = DataStore.Pattern.APattern.Results[0].GetPose().TranslationX;
            double y2 = DataStore.Pattern.APattern.Results[0].GetPose().TranslationY;

            double resultAngle = Utilities.PointToPointAngleAndGraphics(x1, y1, x2, y2);

            GraphicManager.CreateLabel(Display, CogColorConstants.White, new Font("Segoe UI", 15f), $"Angle: {resultAngle:0.00}", x2 - 200, y2 + 100, CogColorConstants.Black);

            GraphicManager.CreateCenterLineGraphics(Display, x2, y2);

            GraphicManager.CreateSegmentGraphics(Display, x1, y1, x2, y2);
        }

        /// <summary>
        /// 자동 검사 시작 메서드
        /// </summary>
        /// <param name="Display">검사 화면 지정</param>
        /// <param name="LogList">로그 창 지정</param>
        /// <param name="BtnSetup">셋업 버튼 비활성화</param>
        /// <param name="BtnFolder">폴더 지정 버튼 비활성화</param>
        /// <param name="BtnExit">종료 버튼 비활성화</param>
        /// <param name="CurrentImageName">현재 이미지 이름</param>
        public void AutoInspection(CogRecordDisplay Display, ListBox LogList, Button BtnSetup, Button BtnFolder, Button BtnExit, Label CurrentImageName)
        {
            if (FManager.MainImageFileName.Count == 0)
            {
                Utilities.PrintLog(LogList, "Image folder is not registered...");
                return;
            }

            if (isStart)
            {
                isStart = false;
                BtnFolder.Enabled = true;
                BtnFolder.BackColor = Color.FromArgb(40, 40, 40);
                BtnSetup.Enabled = true;
                BtnSetup.BackColor = Color.FromArgb(40, 40, 40);
                BtnExit.Enabled = true;
                BtnExit.BackColor = Color.FromArgb(40, 40, 40);
                Utilities.PrintLog(LogList, "Stop automatic inspection...");
                return;
            }
            else
            {
                if (isRunning) return;

                isStart = true;
                isStop = false;
                BtnFolder.Enabled = false;
                BtnFolder.BackColor = Color.Red;
                BtnSetup.Enabled = false;
                BtnSetup.BackColor = Color.Red;
                BtnExit.Enabled = false;
                BtnExit.BackColor = Color.Red;
                Utilities.PrintLog(LogList, "Start automatic inspection...");

                Thread InspThread = new Thread(() =>
                {
                    while (isStart)
                    {
                        StartAutoInsp(Display, CurrentImageName);

                        if (isStop)
                        {
                            BtnFolder.Invoke(new Action(() => BtnFolder.Enabled = true));
                            BtnFolder.Invoke(new Action(() => BtnFolder.BackColor = Color.FromArgb(40, 40, 40)));
                            BtnSetup.Invoke(new Action(() => BtnSetup.Enabled = true));
                            BtnSetup.Invoke(new Action(() => BtnSetup.BackColor = Color.FromArgb(40, 40, 40)));
                            BtnExit.Invoke(new Action(() => BtnExit.Enabled = true));
                            BtnExit.Invoke(new Action(() => BtnExit.BackColor = Color.FromArgb(40, 40, 40)));

                            LogList.Invoke(new Action(() => Utilities.PrintLog(LogList, "Automatic inspection completed!!")));
                        }
                    }
                    
                    isRunning = false;
                });
                InspThread.Start();
            }
        }

        /// <summary>
        /// 자동 검사 시퀀스
        /// </summary>
        /// <param name="Display">검사 화면 지정</param>
        /// <param name="CurrentImageName">현재 이미지 이름</param>
        void StartAutoInsp(CogRecordDisplay Display, Label CurrentImageName)
        {
            isRunning = true;

            if (FManager.MainImageFileName.Count <= count)
            {
                isStart = false;
                isStop = true;
                count = 0;
                return;
            }

            ICogImage image = FManager.Load_ImageFile(FManager.MainImageFileName[count]);

            CurrentImageName.Invoke(new Action(() => CurrentImageName.Text = Path.GetFileName(FManager.MainImageFileName[count])));

            if (image is CogImage24PlanarColor)
                image = CogImageConvert.GetIntensityImage(image, 0, 0, image.Width, image.Height);

            Display.Image = image;
            DataStore.Pattern.InputImage = image;

            count++;

            Utilities.DisplayClear(Display);

            if (!Pattern.PatternRun(Display, "BtnPRun", null)) return;
            if (!Pattern.PatternRun(Display, "BtnARun", null)) return;

            double x1 = DataStore.Pattern.PPattern.Results[0].GetPose().TranslationX;
            double y1 = DataStore.Pattern.PPattern.Results[0].GetPose().TranslationY;
            double x2 = DataStore.Pattern.APattern.Results[0].GetPose().TranslationX;
            double y2 = DataStore.Pattern.APattern.Results[0].GetPose().TranslationY;

            double resultAngle = Utilities.PointToPointAngleAndGraphics(x1, y1, x2, y2);

            GraphicManager.CreateLabel(Display, CogColorConstants.White, new Font("Segoe UI", 15f), $"Angle: {resultAngle:0.00}", x2 - 200, y2 + 100, CogColorConstants.Black);

            GraphicManager.CreateCenterLineGraphics(Display, x2, y2);

            GraphicManager.CreateSegmentGraphics(Display, x1, y1, x2, y2);

            double time = DataStore.InspDelay * 1000;

            Thread.Sleep((int)time);      /* 검사 속도 조절 */
        }

        public class PMAlign
        {
            public static PMAlign Instance { get; private set; } = new PMAlign();
            
            public void PatternTrain(CogRecordDisplay SDisplay, CogRecordDisplay PDisplay, CogRecordDisplay ADisplay, ListBox LogList, Button BtnTrain)
            {
                if (SDisplay.Image == null)
                {
                    Utilities.PrintLog(LogList, "There is no image.");
                    return;
                }

                CogRectangleAffine TrainRegion = null;
                CogRecordDisplay Display = null;
                bool overwriteCheck = false;
                string toolName = null;

                switch (BtnTrain.Name)
                {
                    case "BtnPTrain":
                        Display = PDisplay;
                        TrainRegion = DataStore.RectangleRegion.PTrainRegion;
                        overwriteCheck = DataStore.Pattern.PTrain;
                        toolName = "Point";
                        break;
                    case "BtnATrain":
                        Display = ADisplay;
                        TrainRegion = DataStore.RectangleRegion.ATrainRegion;
                        overwriteCheck = DataStore.Pattern.ATrain;
                        toolName = "Angle";
                        break;
                    default:
                        break;
                }

                if (overwriteCheck)
                    if (MessageBox.Show("Do you want to overwrite the train image?", "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        return;

                switch (toolName)
                {
                    case "Point":
                        DataStore.Pattern.PTrain = TrainRun(TrainRegion, DataStore.Pattern.PPattern);
                        break;
                    case "Angle":
                        DataStore.Pattern.ATrain = TrainRun(TrainRegion, DataStore.Pattern.APattern);
                        break;
                }

                Utilities.DisplayClear(SDisplay);

                string imagePath = Path.Combine(Application.StartupPath, "Image");
                Directory.CreateDirectory(imagePath);

                Display.Image = DataStore.Pattern.TrainImage;

                FileManager FManager = FileManager.Instance;
                FManager.Save_ImageFile(Path.Combine(imagePath, $"{toolName}.bmp"), Display.Image);
                FManager.Save_ImageFile(Path.Combine(imagePath, "MasterImage.bmp"), SDisplay.Image);

                Utilities.PrintLog(LogList, "Image Train and Save Complete!");
            }

            /// <summary>
            /// 패턴 학습
            /// </summary>
            /// <param name="TrainRegion">트레인 할 형상의 영역</param>
            public bool TrainRun(CogRectangleAffine TrainRegion, CogPMAlignTool PMAlignTool)
            {
                if (DataStore.Pattern.InputImage == null) return false;

                try
                {
                    PMAlignTool.Pattern.TrainImage = DataStore.Pattern.InputImage;
                    PMAlignTool.Pattern.TrainRegion = TrainRegion;
                    PMAlignTool.Pattern.Origin.TranslationX = TrainRegion.CenterX;
                    PMAlignTool.Pattern.Origin.TranslationY = TrainRegion.CenterY;

                    PMAlignTool.Pattern.Train();

                    DataStore.Pattern.TrainImage = PMAlignTool.Pattern.GetTrainedPatternImage();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            /// <summary>
            /// 패턴 서치
            /// </summary>
            /// <param name="Display">입출력 디스플레이</param>
            /// <param name="name">Point, Angle 구분</param>
            /// <param name="mode">Run, Manual 구분</param>
            /// <param name="LogList">출력 할 로그 창</param>
            public bool PatternRun(CogRecordDisplay Display, string name, string mode, ListBox LogList = null)
            {
                if (mode == "Run") Utilities.DisplayClear(Display);

                if (Display.Image == null)
                {
                    if (LogList != null) Utilities.PrintLog(LogList, "There is no image.");
                    return false;
                }

                CogPMAlignTool PMAlignTool = new CogPMAlignTool();
                CogRectangleAffine SearchRegion = new CogRectangleAffine();
                double angle = 0;
                double threshold = 0;

                switch (name)
                {
                    case "BtnPRun":
                        PMAlignTool = DataStore.Pattern.PPattern;
                        SearchRegion = DataStore.RectangleRegion.PRegion;
                        angle = DataStore.Pattern.PAngle;
                        threshold = DataStore.Pattern.PThreshold;
                        break;
                    case "BtnARun":
                        PMAlignTool = DataStore.Pattern.APattern;
                        SearchRegion = DataStore.RectangleRegion.ARegion;
                        angle = DataStore.Pattern.AAngle;
                        threshold = DataStore.Pattern.AThreshold;
                        break;
                }

                if (!PMAlignTool.Pattern.Trained)
                {
                    if (LogList != null) Utilities.PrintLog(LogList, "There are no registered patterns.");
                    return false;
                } 

                PMAlignTool.InputImage = DataStore.Pattern.InputImage;

                PMAlignTool.RunParams.ZoneAngle.Configuration = CogPMAlignZoneConstants.LowHigh;
                PMAlignTool.RunParams.ZoneAngle.Low = CogMisc.DegToRad(-angle);
                PMAlignTool.RunParams.ZoneAngle.High = CogMisc.DegToRad(angle);
                PMAlignTool.RunParams.AcceptThreshold = threshold;
                PMAlignTool.SearchRegion = SearchRegion;

                PMAlignTool.Run();

                if (!GraphicManager.PatternResultGraphics(Display, PMAlignTool, SearchRegion)) return false;

                switch (mode)
                {
                    case "Run":
                        Utilities.PrintLog(LogList, "Pattern run complete!");
                        break;
                    case "ManualRun":
                        Utilities.PrintLog(LogList, "Manual run complete!");
                        break;
                    default:
                        break;
                }

                return true;
            }
        }
    }
}