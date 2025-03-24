using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.Dimensioning;

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
        readonly DataStore.Pattern Pattern = DataStore.Pattern.Instance;
        readonly DataStore.Region Region = DataStore.Region.Instance;
        readonly FileManager FManager = FileManager.Instance;

        bool isStart = false;       // 자동 검사 시작 체크
        bool isRunning = false;     // 자동 검사 스레드 동작 중 인지 체크 (자동 검사 Stop을 해도 스레드가 동작 중이면 에러 발생)
        bool isStop = false;        // 자동 검사 종료
        int count = 0;              // 자동 검사 이미지 카운팅

        /// <summary>
        /// 영역 생성
        /// </summary>
        /// <param name="Display">출력 할 디스플레이</param>
        /// <param name="SelectedBtn">선택한 버튼</param>
        public void InitRegion(CogRecordDisplay Display, Button SelectedBtn)
        {
            Utilities.DisplayClear(Display);

            CogRectangleAffine SearchRegion;

            switch (SelectedBtn.Name)
            {
                case "BtnPTrainRegion":
                    SearchRegion = Region.PTrainRegion;
                    break;
                case "BtnPRegion":
                    SearchRegion = Region.PRegion;
                    break;
                case "BtnATrainRegion":
                    SearchRegion = Region.ATrainRegion;
                    break;
                case "BtnARegion":
                    SearchRegion = Region.ARegion;
                    break;
                default:
                    return;
            }

            SearchRegion.GraphicDOFEnable = CogRectangleAffineDOFConstants.All;
            SearchRegion.Interactive = true;
            SearchRegion.Color = CogColorConstants.Cyan;
            SearchRegion.SelectedColor = CogColorConstants.Blue;
            SearchRegion.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;

            Display.InteractiveGraphics.Add(SearchRegion, null, true);
        }

        /// <summary>
        /// 그래픽 라벨 생성
        /// </summary>
        /// <param name="Display">표시 할 디스플레이</param>
        /// <param name="color">폰트 색상</param>
        /// <param name="font">폰트 두께</param>
        /// <param name="text">텍스트</param>
        /// <param name="x">좌표 X</param>
        /// <param name="y">좌표 Y</param>
        /// <param name="backColor">배경 색(투명 생략 가능)</param>
        void CreateLabel(CogRecordDisplay Display, CogColorConstants color, Font font, string text, double x, double y, CogColorConstants backColor = CogColorConstants.None)
        {
            CogGraphicLabel Label = new CogGraphicLabel
            {
                Text = text,
                X = x,
                Y = y,
                Color = color,
                Font = font
            };

            if (backColor != CogColorConstants.None) Label.BackgroundColor = backColor;

            Display.StaticGraphics.Add(Label, null);
            Label.Dispose();
        }

        /// <summary>
        /// 두 점의 각도 구하기 및 그래픽 그리기
        /// </summary>
        /// <param name="Display">출력 할 디스플레이</param>
        /// <param name="x1">점1의 x값</param>
        /// <param name="y1">점1의 y값</param>
        /// <param name="x2">점2의 x값</param>
        /// <param name="y2">점2의 y값</param>
        void PointToPointAngleAndGraphics(CogRecordDisplay Display, double x1, double y1, double x2, double y2)
        {
            // 벡터 방향 (dx, dy)
            double dx = x2 - x1;
            double dy = y2 - y1;

            // 각도 계산
            double thetaRad = Math.Atan2(dy, dx);

            // 0°를 위쪽(↑)으로 변환 90° 빼기
            double resultAngle = Utilities.RadianDegreeConvert("D", thetaRad) - 90;

            // -180~180° 범위로 변환
            if (resultAngle < -180) resultAngle += 360;
            else if (resultAngle >= 360) resultAngle -= 360;

            CreateLabel(Display, CogColorConstants.White, new Font("Segoe UI", 15f), $"Angle: {resultAngle:0.00}", x2 - 200, y2 + 100, CogColorConstants.Black);

            ////////////////// 중앙 직선(CreateLine) 그리기 /////////////////////
            CogCreateLineTool CreateLineTool = new CogCreateLineTool()
            {
                InputImage = Pattern.InputImage,
                OutputColor = CogColorConstants.Yellow,
                OutputLineWidthInScreenPixels = 3
            };

            CreateLineTool.Line.X = x2;
            CreateLineTool.Line.Y = y2;
            CreateLineTool.Line.Rotation = Utilities.RadianDegreeConvert("R", 90);

            CreateLineTool.Run();

            Display.StaticGraphics.Add(CreateLineTool.GetOutputLine(), null);

            //////////////// 두 점의 직선(CreateSegment) 그리기 ///////////////////
            CogCreateSegmentTool CreateSegmentTool = new CogCreateSegmentTool
            {
                InputImage = Pattern.InputImage,
                OutputColor = CogColorConstants.Magenta,
                OutputLineWidthInScreenPixels = 3
            };

            CreateSegmentTool.Segment.StartX = x2;
            CreateSegmentTool.Segment.StartY = y2;
            CreateSegmentTool.Segment.EndX = x1;
            CreateSegmentTool.Segment.EndY = y1;

            CreateSegmentTool.Run();

            Display.StaticGraphics.Add(CreateSegmentTool.GetOutputSegment(), null);
        }

        /// <summary>
        /// 매뉴얼 런
        /// </summary>
        /// <param name="Display">지정 할 디스플레이</param>
        /// <param name="LogList">로그 창 지정</param>
        public void RunManual(CogRecordDisplay Display, ListBox LogList)
        {
            Utilities.DisplayClear(Display);

            if (!PMAlign.Instance.PatternRun(Display, Pattern.PPattern, Region.PRegion, Pattern.PAngle, Pattern.PThreshold, "", LogList)) return;
            if (!PMAlign.Instance.PatternRun(Display, Pattern.APattern, Region.ARegion, Pattern.AAngle, Pattern.AThreshold, "ManualRun", LogList)) return;

            double x1 = Pattern.PPattern.Results[0].GetPose().TranslationX;
            double y1 = Pattern.PPattern.Results[0].GetPose().TranslationY;
            double x2 = Pattern.APattern.Results[0].GetPose().TranslationX;
            double y2 = Pattern.APattern.Results[0].GetPose().TranslationY;

            PointToPointAngleAndGraphics(Display, x1, y1, x2, y2);
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
            Pattern.InputImage = image;

            count++;

            Utilities.DisplayClear(Display);

            if (!PMAlign.Instance.PatternRun(Display, Pattern.PPattern, Region.PRegion, Pattern.PAngle, Pattern.PThreshold, null)) return;
            if (!PMAlign.Instance.PatternRun(Display, Pattern.APattern, Region.ARegion, Pattern.AAngle, Pattern.AThreshold, null)) return;

            double x1 = Pattern.PPattern.Results[0].GetPose().TranslationX;
            double y1 = Pattern.PPattern.Results[0].GetPose().TranslationY;
            double x2 = Pattern.APattern.Results[0].GetPose().TranslationX;
            double y2 = Pattern.APattern.Results[0].GetPose().TranslationY;

            PointToPointAngleAndGraphics(Display, x1, y1, x2, y2);

            double time = DataStore.Instance.InspDelay * 1000;

            Thread.Sleep((int)time);      /* 검사 속도 조절 */
        }

        public class PMAlign
        {
            public static PMAlign Instance { get; private set; } = new PMAlign();
            readonly DataStore.Pattern Pattern = DataStore.Pattern.Instance;
            readonly DataStore.Region Region = DataStore.Region.Instance;
            readonly ToolManager TManager = ToolManager.Instance;
            readonly FileManager FManager = FileManager.Instance;
            
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
                        TrainRegion = Region.PTrainRegion;
                        overwriteCheck = Pattern.PTrain;
                        toolName = "Point";
                        break;
                    case "BtnATrain":
                        Display = ADisplay;
                        TrainRegion = Region.ATrainRegion;
                        overwriteCheck = Pattern.ATrain;
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
                        Pattern.PTrain = TrainRun(TrainRegion, Pattern.PPattern);
                        break;
                    case "Angle":
                        Pattern.ATrain = TrainRun(TrainRegion, Pattern.APattern);
                        break;
                }

                Utilities.DisplayClear(SDisplay);

                string imagePath = Path.Combine(Application.StartupPath, "Image");
                Directory.CreateDirectory(imagePath);

                Display.Image = Pattern.TrainImage;

                FManager.Save_ImageFile(Path.Combine(imagePath, $"{toolName}.bmp"), Display.Image);
                FManager.Save_ImageFile(Path.Combine(imagePath, $"{toolName}Mask.bmp"), Pattern.MaskImage);
                FManager.Save_ImageFile(Path.Combine(imagePath, "MasterImage.bmp"), SDisplay.Image);

                Utilities.PrintLog(LogList, "Image Train and Save Complete!");
            }

            /// <summary>
            /// 패턴 학습
            /// </summary>
            /// <param name="TrainRegion">트레인 할 형상의 영역</param>
            public bool TrainRun(CogRectangleAffine TrainRegion, CogPMAlignTool PMAlignTool)
            {
                if (Pattern.InputImage == null) return false;

                try
                {
                    PMAlignTool.Pattern.TrainImage = Pattern.InputImage;
                    PMAlignTool.Pattern.TrainRegion = TrainRegion;
                    PMAlignTool.Pattern.Origin.TranslationX = TrainRegion.CenterX;
                    PMAlignTool.Pattern.Origin.TranslationY = TrainRegion.CenterY;

                    PMAlignTool.Pattern.Train();

                    Pattern.MaskImage = PMAlignTool.Pattern.GetTrainedPatternImageMask();

                    Pattern.TrainImage = PMAlignTool.Pattern.GetTrainedPatternImage();

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
            /// <param name="PMAlignTool">사용 할 패턴 툴</param>
            /// <param name="SearchRegion">서치 영역</param>
            /// <param name="angle">각도</param>
            /// <param name="threshold">임계치</param>
            /// <param name="LogList">출력 할 로그 창</param>
            public bool PatternRun(CogRecordDisplay Display, CogPMAlignTool PMAlignTool, CogRectangleAffine SearchRegion, double angle, double threshold, string mode, ListBox LogList = null)
            {
                if (mode == "Run") Utilities.DisplayClear(Display);

                if (Display.Image == null)
                {
                    if (LogList != null) Utilities.PrintLog(LogList, "There is no image.");
                    return false;
                }

                if (!PMAlignTool.Pattern.Trained)
                {
                    if (LogList != null) Utilities.PrintLog(LogList, "There are no registered patterns.");
                    return false;
                } 

                PMAlignTool.InputImage = Pattern.InputImage;

                PMAlignTool.RunParams.ZoneAngle.Configuration = CogPMAlignZoneConstants.LowHigh;
                PMAlignTool.RunParams.ZoneAngle.Low = CogMisc.DegToRad(-angle);
                PMAlignTool.RunParams.ZoneAngle.High = CogMisc.DegToRad(angle);
                PMAlignTool.RunParams.AcceptThreshold = threshold;
                PMAlignTool.SearchRegion = SearchRegion;

                PMAlignTool.Run();

                if (PMAlignTool.Results == null || PMAlignTool.Results.Count <= 0)
                {
                    SearchRegion.Color = CogColorConstants.Red;
                    SearchRegion.XDirectionAdornment = CogRectangleAffineDirectionAdornmentConstants.None;
                    SearchRegion.YDirectionAdornment = CogRectangleAffineDirectionAdornmentConstants.None;

                    Display.StaticGraphics.Add(SearchRegion, "");

                    TManager.CreateLabel(Display, CogColorConstants.Red, new Font("Segoe UI", 50f), "NG", 300, 200);

                    return false;
                }

                CogCompositeShape ResultGraphics = PMAlignTool.Results[0].CreateResultGraphics(CogPMAlignResultGraphicConstants.MatchRegion | CogPMAlignResultGraphicConstants.Origin);

                Display.StaticGraphics.Add(ResultGraphics, null);

                double PointX = PMAlignTool.Results[0].GetPose().TranslationX;
                double PointY = PMAlignTool.Results[0].GetPose().TranslationY;

                string strScore = $"Score: {PMAlignTool.Results[0].Score * 100:0}";
                string strPos = $"X: {PointX:0.00}, Y: {PointY:0.00}";

                TManager.CreateLabel(Display, CogColorConstants.White, new Font("Segoe UI", 15f), strScore, PointX, PointY + 150, CogColorConstants.Black);
                TManager.CreateLabel(Display, CogColorConstants.White, new Font("Segoe UI", 15f), strPos, PointX, PointY + 200, CogColorConstants.Black);

                if (PMAlignTool.Results[0].Score > PMAlignTool.RunParams.AcceptThreshold)
                    TManager.CreateLabel(Display, CogColorConstants.Green, new Font("Segoe UI", 50f), "OK", 300, 200);
                else
                    TManager.CreateLabel(Display, CogColorConstants.Red, new Font("Segoe UI", 50f), "NG", 300, 200);

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