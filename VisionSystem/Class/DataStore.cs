namespace VisionSystem
{
    internal static class DataStore   // 각종 파라미터 저장소
    {
        public static double InspDelay { get; set; } = 0.5;   // 자동 검사 딜레이 속도

        public class Pattern  // 패턴 저장소
        {
            public static Cognex.VisionPro.ICogImage InputImage { get; set; } = null;    // 패턴 InputImage (공용)
            public static Cognex.VisionPro.ICogImage TrainImage { get; set; } = null;    // 트레인 InputImage (공용)
            public static Cognex.VisionPro.PMAlign.CogPMAlignTool PPattern { get; set; } = new Cognex.VisionPro.PMAlign.CogPMAlignTool();   // point 패턴 툴
            public static Cognex.VisionPro.PMAlign.CogPMAlignTool APattern { get; set; } = new Cognex.VisionPro.PMAlign.CogPMAlignTool();   // angle 패턴 툴
            public static double PThreshold { get; set; } = 0.5;   // point 임계 값
            public static double AThreshold { get; set; } = 0.5;   // angle 임계 값
            public static double PAngle { get; set; } = 180;   // point 각도
            public static double AAngle { get; set; } = 180;   // angle 각도
            public static bool PTrain { get; set; } = false;   // point 트레인 유무
            public static bool ATrain { get; set; } = false;   // angle 트레인 유무
        }
        
        public class RectangleRegion    // 영역 저장소
        {
            public static Cognex.VisionPro.CogRectangleAffine PTrainRegion { get; set; } = new Cognex.VisionPro.CogRectangleAffine();    // point 트레인 영역
            public static Cognex.VisionPro.CogRectangleAffine PRegion { get; set; } = new Cognex.VisionPro.CogRectangleAffine();    // point 서치영역
            public static Cognex.VisionPro.CogRectangleAffine ATrainRegion { get; set; } = new Cognex.VisionPro.CogRectangleAffine();    // angle 트레인 영역
            public static Cognex.VisionPro.CogRectangleAffine ARegion { get; set; } = new Cognex.VisionPro.CogRectangleAffine();    // angle 서치영역
        }
    }
}