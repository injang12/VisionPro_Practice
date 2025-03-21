namespace VisionSystem
{
    internal class DataStore
    {
        public static DataStore Instance { get; private set; } = new DataStore();
        public double InspDelay { get; set; } = 0.5;

        public class Pattern
        {
            public static Pattern Instance { get; private set; } = new Pattern();
            public Cognex.VisionPro.ICogImage InputImage { get; set; } = null;
            public Cognex.VisionPro.ICogImage MaskImage { get; set; } = null;
            public Cognex.VisionPro.ICogImage TrainImage { get; set; } = null;
            public Cognex.VisionPro.PMAlign.CogPMAlignTool PPattern { get; set; } = new Cognex.VisionPro.PMAlign.CogPMAlignTool();
            public Cognex.VisionPro.PMAlign.CogPMAlignTool APattern { get; set; } = new Cognex.VisionPro.PMAlign.CogPMAlignTool();
            public double PThreshold { get; set; } = 0.5;
            public double AThreshold { get; set; } = 0.5;
            public double PAngle { get; set; } = 180;
            public double AAngle { get; set; } = 180;
            public bool PTrain { get; set; } = false;
            public bool ATrain { get; set; } = false;
        }

        public class Region
        {
            public static Region Instance { get; private set; } = new Region();
            public Cognex.VisionPro.CogRectangleAffine PTrainRegion { get; set; } = new Cognex.VisionPro.CogRectangleAffine();
            public Cognex.VisionPro.CogRectangleAffine PRegion { get; set; } = new Cognex.VisionPro.CogRectangleAffine();
            public Cognex.VisionPro.CogRectangleAffine ATrainRegion { get; set; } = new Cognex.VisionPro.CogRectangleAffine();
            public Cognex.VisionPro.CogRectangleAffine ARegion { get; set; } = new Cognex.VisionPro.CogRectangleAffine();
        }
    }
}