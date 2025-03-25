namespace VisionSystem
{
    internal static class DataStore
    {
        public static double InspDelay { get; set; } = 0.5;

        public class Pattern
        {
            public static Cognex.VisionPro.ICogImage InputImage { get; set; } = null;
            public static Cognex.VisionPro.ICogImage TrainImage { get; set; } = null;
            public static Cognex.VisionPro.PMAlign.CogPMAlignTool PPattern { get; set; } = new Cognex.VisionPro.PMAlign.CogPMAlignTool();
            public static Cognex.VisionPro.PMAlign.CogPMAlignTool APattern { get; set; } = new Cognex.VisionPro.PMAlign.CogPMAlignTool();
            public static double PThreshold { get; set; } = 0.5;
            public static double AThreshold { get; set; } = 0.5;
            public static double PAngle { get; set; } = 180;
            public static double AAngle { get; set; } = 180;
            public static bool PTrain { get; set; } = false;
            public static bool ATrain { get; set; } = false;
        }

        public class Region
        {
            public static Cognex.VisionPro.CogRectangleAffine PTrainRegion { get; set; } = new Cognex.VisionPro.CogRectangleAffine();
            public static Cognex.VisionPro.CogRectangleAffine PRegion { get; set; } = new Cognex.VisionPro.CogRectangleAffine();
            public static Cognex.VisionPro.CogRectangleAffine ATrainRegion { get; set; } = new Cognex.VisionPro.CogRectangleAffine();
            public static Cognex.VisionPro.CogRectangleAffine ARegion { get; set; } = new Cognex.VisionPro.CogRectangleAffine();
        }
    }
}