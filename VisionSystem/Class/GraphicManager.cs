using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;

using System.Drawing;

namespace VisionSystem
{
    class GraphicManager
    {
        /// <summary>
        /// 디스플레이에 선택된 이름에 해당하는 초기 검색 영역을 표시합니다.
        /// </summary>
        /// <param name="display">검색 영역을 표시할 CogRecordDisplay 객체</param>
        /// <param name="selectedBtn">선택된 버튼 (이름에 따라 대응 영역 선택)</param>
        public static void InitRegion(CogRecordDisplay display, System.Windows.Forms.Button selectedBtn)
        {
            Util.DisplayClear(display);

            System.Collections.Generic.Dictionary<string, CogRectangleAffine> regions = new System.Collections.Generic.Dictionary<string, CogRectangleAffine>
            {
                { "BtnPTrainRegion", DataStore.RectangleRegion.PTrainRegion },
                { "BtnPRegion", DataStore.RectangleRegion.PRegion },
                { "BtnATrainRegion", DataStore.RectangleRegion.ATrainRegion },
                { "BtnARegion", DataStore.RectangleRegion.ARegion }
            };

            if (!regions.TryGetValue(selectedBtn.Name, out CogRectangleAffine searchRegion)) return;

            searchRegion.GraphicDOFEnable = CogRectangleAffineDOFConstants.All;
            searchRegion.Interactive = true;
            searchRegion.Color = CogColorConstants.Cyan;
            searchRegion.SelectedColor = CogColorConstants.Blue;
            searchRegion.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;

            display.InteractiveGraphics.Add(searchRegion, null, true);
        }

        /// <summary>
        /// 디스플레이에 텍스트 라벨을 생성하여 고정 그래픽으로 추가합니다.
        /// </summary>
        /// <param name="display">라벨을 표시할 CogRecordDisplay 객체</param>
        /// <param name="color">텍스트 색상 (전경색)</param>
        /// <param name="font">표시할 텍스트의 폰트 (System.Drawing.Font)</param>
        /// <param name="text">표시할 문자열</param>
        /// <param name="x">텍스트의 X 좌표 (픽셀 단위)</param>
        /// <param name="y">텍스트의 Y 좌표 (픽셀 단위)</param>
        /// <param name="backColor">텍스트 배경색. <see cref="CogColorConstants.None"/>이면 투명 처리됩니다.</param>
        public static void CreateLabel(CogRecordDisplay display, CogColorConstants color, Font font, string text, double x, double y, CogColorConstants backColor)
        {
            CogGraphicLabel label = new CogGraphicLabel
            {
                Text = text,
                X = x,
                Y = y,
                Color = color,
                Font = font,
                BackgroundColor = backColor != CogColorConstants.None ? backColor : CogColorConstants.None
            };

            display.StaticGraphics.Add(label, null);
        }

        /// <summary>
        /// 디스플레이 중앙에 수직 기준선(90도 회전 직선)을 추가합니다.
        /// </summary>
        /// <param name="Display">기준선을 표시할 CogRecordDisplay 객체</param>
        /// <param name="x2">기준선 중심점의 X 좌표</param>
        /// <param name="y2">기준선 중심점의 Y 좌표</param>
        public static void CreateCenterLineGraphics(CogRecordDisplay Display, double x2, double y2)
        {
            Cognex.VisionPro.Dimensioning.CogCreateLineTool CreateLineTool = new Cognex.VisionPro.Dimensioning.CogCreateLineTool()
            {
                InputImage = DataStore.Pattern.InputImage,
                OutputColor = CogColorConstants.Yellow,
                OutputLineWidthInScreenPixels = 3,
                Line = { X = x2, Y = y2, Rotation = Util.RadianDegreeConvert("R", 90) }
            };

            CreateLineTool.Run();
            Display.StaticGraphics.Add(CreateLineTool.GetOutputLine(), null);
        }

        /// <summary>
        /// 디스플레이에 두 점을 잇는 직선을 추가합니다.
        /// </summary>
        /// <param name="Display">직선을 표시할 CogRecordDisplay 객체</param>
        /// <param name="x1">종단점의 X 좌표</param>
        /// <param name="y1">종단점의 Y 좌표</param>
        /// <param name="x2">시작점의 X 좌표</param>
        /// <param name="y2">시작점의 Y 좌표</param>
        public static void CreateSegmentGraphics(CogRecordDisplay Display, double x1, double y1, double x2, double y2)
        {
            Cognex.VisionPro.Dimensioning.CogCreateSegmentTool CreateSegmentTool = new Cognex.VisionPro.Dimensioning.CogCreateSegmentTool
            {
                InputImage = DataStore.Pattern.InputImage,
                OutputColor = CogColorConstants.Magenta,
                OutputLineWidthInScreenPixels = 3,
                Segment = { StartX = x2, StartY = y2, EndX = x1, EndY = y1 }
            };

            CreateSegmentTool.Run();
            Display.StaticGraphics.Add(CreateSegmentTool.GetOutputSegment(), null);
        }

        /// <summary>
        /// 패턴 매칭 결과를 디스플레이에 시각화하고, 정합 여부에 따라 결과 라벨을 표시합니다.
        /// </summary>
        /// <param name="Display">결과를 표시할 CogRecordDisplay 객체</param>
        /// <param name="PMAlignTool">실행된 CogPMAlignTool 객체</param>
        /// <param name="SearchRegion">매칭에 사용된 검색 영역. 실패 시 표시 색이 변경됩니다.</param>
        /// <returns>매칭 결과가 유효하고 허용 임계값을 통과하면 true, 그렇지 않으면 false 반환</returns>
        public static bool PatternResultGraphics(CogRecordDisplay Display, CogPMAlignTool PMAlignTool, CogRectangleAffine SearchRegion)
        {
            CogPMAlignResults Result = PMAlignTool.Results;

            if (Result == null || Result.Count == 0)
            {
                SearchRegion.Color = CogColorConstants.Red;
                SearchRegion.XDirectionAdornment = CogRectangleAffineDirectionAdornmentConstants.None;
                SearchRegion.YDirectionAdornment = CogRectangleAffineDirectionAdornmentConstants.None;

                Display.StaticGraphics.Add(SearchRegion, "");
                CreateLabel(Display, CogColorConstants.Red, new Font("Segoe UI", 50f), "NG", 300, 200, CogColorConstants.None);
                return false;
            }

            Display.StaticGraphics.Add(Result[0].CreateResultGraphics(CogPMAlignResultGraphicConstants.MatchRegion | CogPMAlignResultGraphicConstants.Origin), null);

            double x = Result[0].GetPose().TranslationX;
            double y = Result[0].GetPose().TranslationY;

            CreateLabel(Display, CogColorConstants.White, new Font("Segoe UI", 15f), $"Score: {Result[0].Score * 100:0}", x, y + 150, CogColorConstants.Black);
            CreateLabel(Display, CogColorConstants.White, new Font("Segoe UI", 15f), $"X: {x:0.00}, Y: {y:0.00}", x, y + 200, CogColorConstants.Black);

            bool isOk = Result[0].Score > PMAlignTool.RunParams.AcceptThreshold;
            CreateLabel(Display, isOk ? CogColorConstants.Green : CogColorConstants.Red, new Font("Segoe UI", 50f), isOk ? "OK" : "NG", 300, 200, CogColorConstants.None);
            return true;
        }
    }
}