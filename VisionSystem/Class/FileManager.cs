using Cognex.VisionPro;
using Cognex.VisionPro.ImageFile;

using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using static VisionSystem.DataStore;

namespace VisionSystem
{
    internal class FileManager
    {
        public static FileManager Instance { get; private set; } = new FileManager();

        List<string> SetupImageFileName { get; set; } = new List<string>();
        public List<string> MainImageFileName { get; set; } = new List<string>();
        int ImageIndex { get; set; } = -1;

        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        string INIPath { get; set; }

        /// <summary>
        /// INI파일 경로 지정
        /// </summary>
        /// <param name="path">불러올 INI파일 경로 입력</param>
        void Set_INI_Path(string path) => INIPath = path;

        /// <summary>
        /// INI파일 내용 읽어온 후 해당 값 String으로 반환
        /// </summary>
        /// <param name="Section">INI파일의 섹션 이름</param>
        /// <param name="Key">해당 섹션의 내용 이름</param>
        /// <returns>string</returns>
        string ReadValue(string Section, string Key)
        {
            StringBuilder strValue = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", strValue, 255, INIPath);
            return strValue.ToString();
        }

        /// <summary>
        /// INI파일 내용 입력한 값으로 수정
        /// </summary>
        /// <param name="Section">INI파일의 섹션 이름</param>
        /// <param name="Key">해당 섹션의 내용 이름</param>
        /// <param name="Value">해당 섹션의 내용 값</param>
        /// <returns>bool</returns>
        bool WriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, INIPath);
            return true;
        }

        /// <summary>
        /// 자동 검사에 사용 할 이미지 폴더 지정
        /// </summary>
        /// <param name="LogList">로그 창 지정</param>
        /// <param name="isSetup">셋업 창, 메인 창 구분</param>
        public void SpecifyFolder(ListBox LogList, bool isSetup)
        {
            List<string> FileName = new List<string>();

            FileName = isSetup ? SetupImageFileName : MainImageFileName;

            using (FolderBrowserDialog openFolder = new FolderBrowserDialog())
            {
                if (openFolder.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = openFolder.SelectedPath;

                    if (string.IsNullOrEmpty(folderPath))
                    {
                        Utilities.PrintLog(LogList, "Failed to specify folder...");
                        return;
                    }

                    string[] files = Directory.GetFiles(folderPath);
                    FileName.Clear();
                    FileName.AddRange(files);

                    // 확장자 필터링
                    FileName = FileName.Where(file =>
                        file.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                        file.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                        file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                    ).ToList();

                    if (FileName.Count > 0)
                    {
                        ImageIndex = 0;
                        Utilities.PrintLog(LogList, $"Directory specified successfully! {FileName.Count} images found.");
                    }
                    else Utilities.PrintLog(LogList, "No valid image files found in the selected folder.");
                }
                else Utilities.PrintLog(LogList, "Failed to specify folder...");
            }
        }

        /// <summary>
        /// 지정한 이미지 폴더 다음 이미지 넘기기
        /// </summary>
        /// <returns>string</returns>
        string NextImage(ListBox LogList)
        {
            if (SetupImageFileName.Count == 0)
            {
                Utilities.PrintLog(LogList, "The specified folder does not exist...");
                return null;
            }

            ImageIndex++;
            if (ImageIndex >= SetupImageFileName.Count) ImageIndex = 0; // 처음으로 돌아감

            return SetupImageFileName[ImageIndex];
        }

        /// <summary>
        /// 지정한 이미지 폴더 이전 이미지 넘기기
        /// </summary>
        /// <returns>string</returns>
        string PreImage(ListBox LogList)
        {
            if (SetupImageFileName.Count == 0)
            {
                Utilities.PrintLog(LogList, "The specified folder does not exist...");
                return null;
            }

            ImageIndex--;
            if (ImageIndex < 0) ImageIndex = SetupImageFileName.Count - 1; // 마지막 이미지로 돌아감

            return SetupImageFileName[ImageIndex];
        }

        /// <summary>
        /// 지정된 폴더 이미지 넘기기
        /// </summary>
        /// <param name="Display">사용 할 디스플레이</param>
        /// <param name="LogList">사용 할 로그 창</param>
        /// <param name="CurrentImage">현재 이미지 이름</param>
        /// <param name="specify">폴더 지정(true) or 이미지 넘기기(false)</param>
        /// <param name="BtnPath">이미지 넘기기 버튼(이전, 다음 이미지)</param>
        public void TurnImageOver(CogRecordDisplay Display, ListBox LogList, Label CurrentImage, bool specify, Button BtnPath = null)
        {
            string path = null;

            if (specify)
            {
                SpecifyFolder(LogList, true);

                if (SetupImageFileName.Count == 0) return;

                path = SetupImageFileName[0];
            }
            else
            {
                switch (BtnPath.Name)
                {
                    case "BtnPreImage":
                        path = PreImage(LogList);
                        break;
                    case "BtnNextImage":
                        path = NextImage(LogList);
                        break;
                    default:
                        break;
                }
            }

            ICogImage image = Load_ImageFile(path);

            if (image == null) return;

            if (image is CogImage24PlanarColor)
                image = CogImageConvert.GetIntensityImage(image, 0, 0, image.Width, image.Height);

            Display.Image = image;
            Pattern.InputImage = image;

            ToolManager.RunManual(Display, LogList);
            CurrentImage.Text = Path.GetFileName(SetupImageFileName[ImageIndex]);
        }

        /// <summary>
        /// 이미지 저장
        /// </summary>
        /// <param name="path">경로 지정</param>
        /// <param name="Image">저장 할 이미지</param>
        public void Save_ImageFile(string path, ICogImage Image)
        {
            using (CogImageFileTool imgFileTool = new CogImageFileTool())
            {
                imgFileTool.InputImage = Image;
                imgFileTool.Operator.Open(path, CogImageFileModeConstants.Write);
                imgFileTool.Run();
                imgFileTool.Operator.Close();
            }
        }

        /// <summary>
        /// 이미지 불러오기(경로)
        /// </summary>
        /// <param name="imagePath">경로 지정</param>
        /// <returns>ICogImage</returns>
        public ICogImage Load_ImageFile(string imagePath)
        {
            if (imagePath == null) return null;

            try
            {
                using (CogImageFileTool imgFileTool = new CogImageFileTool())
                {
                    imgFileTool.Operator.Open(imagePath, CogImageFileModeConstants.Read);
                    imgFileTool.Run();

                    ICogImage lo_Image8bit = imgFileTool.OutputImage;

                    imgFileTool.Operator.Close();

                    return lo_Image8bit;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 이미지 로드 (선택)
        /// </summary>
        /// <param name="Display">출력할 디스플레이</param>
        /// /// <param name="LogList">로그 창</param>
        public void Load_Image(CogRecordDisplay Display, ListBox LogList)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.DefaultExt = "jpg";
                dlg.Filter = "JPEG Image (*.jpg)|*.jpg|PNG Image (*.png)|*.png|BMP Image (*.bmp)|*.bmp";

                if (dlg.ShowDialog() != DialogResult.OK || !File.Exists(dlg.FileName)) return;

                using (CogImageFileTool ImageFileTool = new CogImageFileTool())
                {
                    ImageFileTool.Operator.Open(@dlg.FileName, CogImageFileModeConstants.Read);
                    ImageFileTool.Run();

                    Utilities.DisplayClear(Display);

                    ICogImage Image = null;

                    if (ImageFileTool.OutputImage.ToString() == "Cognex.VisionPro.CogImage24PlanarColor")
                        Image = CogImageConvert.GetIntensityImage(ImageFileTool.OutputImage, 0, 0, ImageFileTool.OutputImage.Width, ImageFileTool.OutputImage.Height);

                    Display.Image = Image;
                    Pattern.InputImage = Display.Image;

                    ImageFileTool.Operator.Close();
                }
                Utilities.PrintLog(LogList, "Image Load Successful!");
            }
        }
        
        /// <summary>
        /// 이미지 로드 (이미지 이름 지정 - 경로: Debug\Image\xxx.bmp)
        /// </summary>
        /// <param name="Display">출력 할 디스플레이</param>
        /// <param name="name">이미지 이름</param>
        void Load_Image(CogRecordDisplay Display, string name)
        {
            using (CogImageFileTool ImageFileTool = new CogImageFileTool())
            {
                ImageFileTool.Operator.Open(@Path.Combine(Application.StartupPath, "Image", $"{name}.bmp"), CogImageFileModeConstants.Read);
                ImageFileTool.Run();

                Utilities.DisplayClear(Display);
                Display.Image = ImageFileTool.OutputImage;

                ImageFileTool.Operator.Close();
            }
        }

        /// <summary>
        /// 영역 ini 저장 메서드
        /// </summary>
        /// <param name="section">ini_Section</param>
        /// <param name="Region">ini_Key</param>
        void WriteRegion(string section, CogRectangleAffine Region)
        {
            WriteValue(section, "CenterX", Region.CenterX.ToString());
            WriteValue(section, "CenterY", Region.CenterY.ToString());
            WriteValue(section, "SideXLength", Region.SideXLength.ToString());
            WriteValue(section, "SideYLength", Region.SideYLength.ToString());
            WriteValue(section, "Rotation", Region.Rotation.ToString());
        }

        /// <summary>
        /// 영역 ini 로드 메서드
        /// </summary>
        /// <param name="section">ini_Section</param>
        /// <param name="Region">ini_Key</param>
        void ReadRegion(string section, CogRectangleAffine Region)
        {
            Region.CenterX = Convert.ToDouble(ReadValue(section, "CenterX"));
            Region.CenterY = Convert.ToDouble(ReadValue(section, "CenterY"));
            Region.SideXLength = Convert.ToDouble(ReadValue(section, "SideXLength"));
            Region.SideYLength = Convert.ToDouble(ReadValue(section, "SideYLength"));
            Region.Rotation = Convert.ToDouble(ReadValue(section, "Rotation"));
        }

        /// <summary>
        /// 저장된 패턴 트레인
        /// </summary>
        /// <param name="name">Point 패턴 or Angle 패턴</param>
        /// <param name="Display">사용 할 디스플레이</param>
        /// <param name="Region">트레인 서치영역</param>
        /// <param name="Pattern">저장 할 패턴 툴</param>
        void TrainPattern(string name, CogRecordDisplay Display, CogRectangleAffine Region, Cognex.VisionPro.PMAlign.CogPMAlignTool Pattern)
        {
            Load_Image(Display, name);
            string imagePath = Path.Combine(Application.StartupPath, "Image", "MasterImage.bmp");
            ICogImage image = Load_ImageFile(imagePath);

            if (image.ToString() == "Cognex.VisionPro.CogImage24PlanarColor")
                image = CogImageConvert.GetIntensityImage(image, 0, 0, image.Width, image.Height);

            Pattern.InputImage = image;
            ToolManager.PMAlign.Instance.TrainRun(Region, Pattern);
        }

        /// <summary>
        /// 파라미터 세이브
        /// </summary>
        /// <param name="LogList">로그 창</param>
        public void ParamSave(ListBox LogList)
        {
            if (MessageBox.Show("Do you want to save it?", "Caution", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

            string path = Path.Combine(Application.StartupPath, "Param");

            Directory.CreateDirectory(path);

            string iniPath = Path.Combine(path, "Param.ini");

            if (!File.Exists(iniPath)) File.Create(iniPath).Dispose();

            Set_INI_Path(iniPath);

            WriteValue("PPattern", "Threshold", Pattern.PThreshold.ToString());
            WriteValue("PPattern", "Angle", Pattern.PAngle.ToString());
            WriteValue("APattern", "Threshold", Pattern.AThreshold.ToString());
            WriteValue("APattern", "Angle", Pattern.AAngle.ToString());

            WriteRegion("PTrain", RectangleRegion.PTrainRegion);
            WriteRegion("PSearch", RectangleRegion.PRegion);
            WriteRegion("ATrain", RectangleRegion.ATrainRegion);
            WriteRegion("ASearch", RectangleRegion.ARegion);

            WriteValue("Train", "Point", Pattern.PTrain.ToString());
            WriteValue("Train", "Angle", Pattern.ATrain.ToString());

            Utilities.PrintLog(LogList, "Successfully saved parameters.");
        }

        /// <summary>
        /// 파라미터 로드
        /// </summary>
        public void ParamLoad()
        {
            string iniPath = Path.Combine(Application.StartupPath, "Param", "Param.ini");

            if (!File.Exists(iniPath)) return;

            Set_INI_Path(iniPath);

            Pattern.PThreshold = Convert.ToDouble(ReadValue("PPattern", "Threshold"));
            Pattern.PAngle = Convert.ToDouble(ReadValue("PPattern", "Angle"));
            Pattern.AThreshold = Convert.ToDouble(ReadValue("APattern", "Threshold"));
            Pattern.AAngle = Convert.ToDouble(ReadValue("APattern", "Angle"));

            ReadRegion("PTrain", RectangleRegion.PTrainRegion);
            ReadRegion("PSearch", RectangleRegion.PRegion);
            ReadRegion("ATrain", RectangleRegion.ATrainRegion);
            ReadRegion("ASearch", RectangleRegion.ARegion);

            Pattern.PTrain = Convert.ToBoolean(ReadValue("Train", "Point"));
            Pattern.ATrain = Convert.ToBoolean(ReadValue("Train", "Angle"));

            if (Pattern.PTrain)
                TrainPattern("Point", SetupForm.Instance.PDisplay, RectangleRegion.PTrainRegion, Pattern.PPattern);
            if (Pattern.ATrain)
                TrainPattern("Angle", SetupForm.Instance.ADisplay, RectangleRegion.ATrainRegion, Pattern.APattern);
        }
    }
}