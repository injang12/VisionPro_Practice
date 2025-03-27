namespace VisionSystem
{
    /// <summary>
    /// 시스템 전체에서 공유되는 전역 데이터 저장소
    /// </summary>
    internal static class DataStore
    {
        /// <summary>
        /// 자동 검사 딜레이 시간 (초 단위)
        /// </summary>
        public static double InspDelay { get; set; } = 0.5;

        /// <summary>
        /// 패턴 검사 관련 전역 상태
        /// </summary>
        public static class Pattern
        {
            public static Cognex.VisionPro.ICogImage InputImage { get; set; } = null;    // 패턴 InputImage (공용)
            public static Cognex.VisionPro.ICogImage TrainImage { get; set; } = null;    // 트레인 InputImage (공용)
            public static Cognex.VisionPro.PMAlign.CogPMAlignTool PPattern { get; set; } = new Cognex.VisionPro.PMAlign.CogPMAlignTool();   // Point 패턴 툴
            public static Cognex.VisionPro.PMAlign.CogPMAlignTool APattern { get; set; } = new Cognex.VisionPro.PMAlign.CogPMAlignTool();   // Angle 패턴 툴
            public static double PThreshold { get; set; } = 0.5;   // Point 임계값
            public static double AThreshold { get; set; } = 0.5;   // Angle 임계값
            public static double PAngle { get; set; } = 180;   // Point 각도
            public static double AAngle { get; set; } = 180;   // Angle 각도
            public static bool PTrain { get; set; } = false;   // Point 트레인 유무
            public static bool ATrain { get; set; } = false;   // Angle 트레인 유무
        }

        /// <summary>
        /// 서치 및 트레인 영역 설정 정보
        /// </summary>
        public static class RectangleRegion
        {
            public static Cognex.VisionPro.CogRectangleAffine PTrainRegion { get; set; } = new Cognex.VisionPro.CogRectangleAffine();    // Point 트레인 영역
            public static Cognex.VisionPro.CogRectangleAffine PRegion { get; set; } = new Cognex.VisionPro.CogRectangleAffine();    // Point 서치 영역
            public static Cognex.VisionPro.CogRectangleAffine ATrainRegion { get; set; } = new Cognex.VisionPro.CogRectangleAffine();    // Angle 트레인 영역
            public static Cognex.VisionPro.CogRectangleAffine ARegion { get; set; } = new Cognex.VisionPro.CogRectangleAffine();    // Angle 서치 영역
        }
    }
}