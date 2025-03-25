using Cognex.VisionPro;
using Cognex.VisionPro.Dimensioning;

using System.Drawing;
using System.Windows.Forms;

namespace VisionSystem
{
    class GraphicManager
    {
        /// <summary>
        /// 영역 생성
        /// </summary>
        /// <param name="Display">출력 할 디스플레이</param>
        /// <param name="SelectedBtn">선택한 버튼</param>
        public static void InitRegion(CogRecordDisplay Display, Button SelectedBtn)
        {
            Utilities.DisplayClear(Display);

            CogRectangleAffine SearchRegion;

            switch (SelectedBtn.Name)
            {
                case "BtnPTrainRegion":
                    SearchRegion = DataStore.Region.PTrainRegion;
                    break;
                case "BtnPRegion":
                    SearchRegion = DataStore.Region.PRegion;
                    break;
                case "BtnATrainRegion":
                    SearchRegion = DataStore.Region.ATrainRegion;
                    break;
                case "BtnARegion":
                    SearchRegion = DataStore.Region.ARegion;
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
        public static void CreateLabel(CogRecordDisplay Display, CogColorConstants color, Font font, string text, double x, double y, CogColorConstants backColor = CogColorConstants.None)
        {
            CogGraphicLabel Label = new CogGraphicLabel
            {
                Text = text,
                X = x,
                Y = y,
                Color = color,
                Font = font
            };

            if (backColor != CogColorConstants.None)
                Label.BackgroundColor = backColor;

            Display.StaticGraphics.Add(Label, null);
            Label.Dispose();
        }

        /// <summary>
        /// 중앙 직선 그리기
        /// </summary>
        /// <param name="Display">출력 할 디스플레이</param>
        /// <param name="x2">점2의 x값</param>
        /// <param name="y2">점2의 y값</param>
        public static void CreateCenterLineGraphics(CogRecordDisplay Display, double x2, double y2)
        {
            CogCreateLineTool CreateLineTool = new CogCreateLineTool()
            {
                InputImage = DataStore.Pattern.InputImage,
                OutputColor = CogColorConstants.Yellow,
                OutputLineWidthInScreenPixels = 3
            };

            CreateLineTool.Line.X = x2;
            CreateLineTool.Line.Y = y2;
            CreateLineTool.Line.Rotation = Utilities.RadianDegreeConvert("R", 90);

            CreateLineTool.Run();

            Display.StaticGraphics.Add(CreateLineTool.GetOutputLine(), null);
        }

        /// <summary>
        /// 두 점의 직선 그리기
        /// </summary>
        /// <param name="Display">출력 할 디스플레이</param>
        /// <param name="x1">점1의 x값</param>
        /// <param name="y1">점1의 y값</param>
        /// <param name="x2">점2의 x값</param>
        /// <param name="y2">점2의 y값</param>
        public static void CreateSegmentGraphics(CogRecordDisplay Display, double x1, double y1, double x2, double y2)
        {
            CogCreateSegmentTool CreateSegmentTool = new CogCreateSegmentTool
            {
                InputImage = DataStore.Pattern.InputImage,
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
    }
}