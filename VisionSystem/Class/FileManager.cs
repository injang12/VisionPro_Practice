using Cognex.VisionPro;
using Cognex.VisionPro.ImageFile;

using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace VisionSystem
{
    internal class FileManager
    {
        public static FileManager Instance { get; private set; } = new FileManager();
        public List<string> SetupImageFileName { get; set; } = new List<string>();
        public List<string> MainImageFileName { get; set; } = new List<string>();
        public int imageIndex = -1;

        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        readonly DataStore.Pattern Pattern = DataStore.Pattern.Instance;
        readonly DataStore.Region Region = DataStore.Region.Instance;
        string str_INIPath;

        /// <summary>
        /// INI파일 경로 지정
        /// </summary>
        /// <param name="INIPath">불러올 INI파일 경로 입력</param>
        public void Set_INI_Path(string INIPath) => str_INIPath = INIPath;

        /// <summary>
        /// INI파일 내용 읽어온 후 해당 값 String으로 반환
        /// </summary>
        /// <param name="Section">INI파일의 섹션 이름</param>
        /// <param name="Key">해당 섹션의 내용 이름</param>
        /// <returns></returns>
        public string ReadValue(string Section, string Key)
        {
            StringBuilder strValue = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", strValue, 255, str_INIPath);
            return strValue.ToString();
        }

        /// <summary>
        /// INI파일 내용 입력한 값으로 수정
        /// </summary>
        /// <param name="Section">INI파일의 섹션 이름</param>
        /// <param name="Key">해당 섹션의 내용 이름</param>
        /// <param name="Value">해당 섹션의 내용 값</param>
        /// <returns></returns>
        public bool WriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, str_INIPath);
            return true;
        }

        /// <summary>
        /// 자동 검사에 사용 할 이미지 폴더 지정
        /// </summary>
        /// <param name="LogBox">로그 창 지정</param>
        /// <param name="isSetup">셋업 창, 메인 창 구분</param>
        public void SpecifyFolder(ListBox LogBox, bool isSetup)
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
                        LogManager.PrintLog(LogBox, "Failed to specify folder...");
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
                        imageIndex = 0;
                        LogManager.PrintLog(LogBox, $"Directory specified successfully! {FileName.Count} images found.");
                    }
                    else LogManager.PrintLog(LogBox, "No valid image files found in the selected folder.");
                }
                else LogManager.PrintLog(LogBox, "Failed to specify folder...");
            }
        }

        /// <summary>
        /// 지정한 이미지 폴더 다음 이미지 넘기기
        /// </summary>
        /// <returns>string</returns>
        public string NextImage(ListBox LogBox)
        {
            if (SetupImageFileName.Count == 0)
            {
                LogManager.PrintLog(LogBox, "The specified folder does not exist...");
                return null;
            }

            imageIndex++;
            if (imageIndex >= SetupImageFileName.Count) imageIndex = 0; // 처음으로 돌아감

            return SetupImageFileName[imageIndex];
        }

        /// <summary>
        /// 지정한 이미지 폴더 이전 이미지 넘기기
        /// </summary>
        /// <returns>string</returns>
        public string PreImage(ListBox LogBox)
        {
            if (SetupImageFileName.Count == 0)
            {
                LogManager.PrintLog(LogBox, "The specified folder does not exist...");
                return null;
            }

            imageIndex--;
            if (imageIndex < 0) imageIndex = SetupImageFileName.Count - 1; // 마지막 이미지로 돌아감

            return SetupImageFileName[imageIndex];
        }


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

            ToolManager.Instance.RunManual(Display, LogList);
            CurrentImage.Text = Path.GetFileName(SetupImageFileName[imageIndex]);
        }

        /// <summary>
        /// 이미지 저장
        /// </summary>
        /// <param name="path">경로 지정</param>
        /// <param name="image">저장 할 이미지</param>
        public void Save_ImageFile(string path, ICogImage image)
        {
            using (CogImageFileTool imgFileTool = new CogImageFileTool())
            {
                imgFileTool.InputImage = image;
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
        /// 이미지 로드
        /// </summary>
        /// <param name="Display">출력할 디스플레이</param>
        /// /// <param name="LogBox">로그 창</param>
        public void Load_Image(CogRecordDisplay Display, ListBox LogBox)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.DefaultExt = "bmp";
                dlg.Filter = "BMP Image (*.bmp)|*.bmp|PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg";

                if (dlg.ShowDialog() != DialogResult.OK || !File.Exists(dlg.FileName)) return;

                using (CogImageFileTool ImageFileTool = new CogImageFileTool())
                {
                    ImageFileTool.Operator.Open(@dlg.FileName, CogImageFileModeConstants.Read);
                    ImageFileTool.Run();

                    ToolManager.Instance.DisplayClear(Display);

                    ICogImage Image = null;

                    if (ImageFileTool.OutputImage.ToString() == "Cognex.VisionPro.CogImage24PlanarColor")
                        Image = CogImageConvert.GetIntensityImage(ImageFileTool.OutputImage, 0, 0, ImageFileTool.OutputImage.Width, ImageFileTool.OutputImage.Height);

                    Display.Image = Image;
                    Pattern.InputImage = Display.Image;

                    ImageFileTool.Operator.Close();
                }
                LogManager.PrintLog(LogBox, "Image Load Successful!");
            }
        }

        void Load_Image(CogRecordDisplay Display, string name)
        {
            using (CogImageFileTool ImageFileTool = new CogImageFileTool())
            {
                ImageFileTool.Operator.Open(@Path.Combine(Application.StartupPath, "Image", $"{name}.bmp"), CogImageFileModeConstants.Read);
                ImageFileTool.Run();

                ToolManager.Instance.DisplayClear(Display);
                Display.Image = ImageFileTool.OutputImage;

                ImageFileTool.Operator.Close();
            }
        }

        /// <summary>
        /// 파라미터 저장
        /// </summary>
        public void ParamSave(ListBox LogBox)
        {
            if (MessageBox.Show("Do you want to save it?", "Caution", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

            string path = Path.Combine(Application.StartupPath, "Param");
            Directory.CreateDirectory(path);

            path = Path.Combine(path, "Param.ini");

            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path))
                {
                }
            }
            Set_INI_Path(path);

            WriteValue("PPattern", "Threshold", Pattern.PThreshold.ToString());
            WriteValue("PPattern", "Angle", Pattern.PAngle.ToString());

            WriteValue("APattern", "Threshold", Pattern.AThreshold.ToString());
            WriteValue("APattern", "Angle", Pattern.AAngle.ToString());

            WriteValue("PTrain", "CenterX", Region.PTrainRegion.CenterX.ToString());
            WriteValue("PTrain", "CenterY", Region.PTrainRegion.CenterY.ToString());
            WriteValue("PTrain", "SideXLength", Region.PTrainRegion.SideXLength.ToString());
            WriteValue("PTrain", "SideYLength", Region.PTrainRegion.SideYLength.ToString());
            WriteValue("PTrain", "Rotation", Region.PTrainRegion.Rotation.ToString());

            WriteValue("PSearch", "CenterX", Region.PRegion.CenterX.ToString());
            WriteValue("PSearch", "CenterY", Region.PRegion.CenterY.ToString());
            WriteValue("PSearch", "SideXLength", Region.PRegion.SideXLength.ToString());
            WriteValue("PSearch", "SideYLength", Region.PRegion.SideYLength.ToString());
            WriteValue("PSearch", "Rotation", Region.PRegion.Rotation.ToString());

            WriteValue("ATrain", "CenterX", Region.ATrainRegion.CenterX.ToString());
            WriteValue("ATrain", "CenterY", Region.ATrainRegion.CenterY.ToString());
            WriteValue("ATrain", "SideXLength", Region.ATrainRegion.SideXLength.ToString());
            WriteValue("ATrain", "SideYLength", Region.ATrainRegion.SideYLength.ToString());
            WriteValue("ATrain", "Rotation", Region.ATrainRegion.Rotation.ToString());

            WriteValue("ASearch", "CenterX", Region.ARegion.CenterX.ToString());
            WriteValue("ASearch", "CenterY", Region.ARegion.CenterY.ToString());
            WriteValue("ASearch", "SideXLength", Region.ARegion.SideXLength.ToString());
            WriteValue("ASearch", "SideYLength", Region.ARegion.SideYLength.ToString());
            WriteValue("ASearch", "Rotation", Region.ARegion.Rotation.ToString());

            WriteValue("Train", "Point", Pattern.PTrain.ToString());
            WriteValue("Train", "Angle", Pattern.ATrain.ToString());

            LogManager.PrintLog(LogBox, "Successfully saved parameters.");
        }

        /// <summary>
        /// 파라미터 불러오기
        /// </summary>
        public void ParamLoad()
        {
            string path = Path.Combine(Application.StartupPath, "Param", "Param.ini");

            if (!File.Exists(path)) return;

            Set_INI_Path(path);

            Pattern.PThreshold = Convert.ToDouble(ReadValue("PPattern", "Threshold"));
            Pattern.PAngle = Convert.ToDouble(ReadValue("PPattern", "Angle"));

            Pattern.AThreshold = Convert.ToDouble(ReadValue("APattern", "Threshold"));
            Pattern.AAngle = Convert.ToDouble(ReadValue("APattern", "Angle"));

            Region.PTrainRegion.CenterX = Convert.ToDouble(ReadValue("PTrain", "CenterX"));
            Region.PTrainRegion.CenterY = Convert.ToDouble(ReadValue("PTrain", "CenterY"));
            Region.PTrainRegion.SideXLength = Convert.ToDouble(ReadValue("PTrain", "SideXLength"));
            Region.PTrainRegion.SideYLength = Convert.ToDouble(ReadValue("PTrain", "SideYLength"));
            Region.PTrainRegion.Rotation = Convert.ToDouble(ReadValue("PTrain", "Rotation"));

            Region.PRegion.CenterX = Convert.ToDouble(ReadValue("PSearch", "CenterX"));
            Region.PRegion.CenterY = Convert.ToDouble(ReadValue("PSearch", "CenterY"));
            Region.PRegion.SideXLength = Convert.ToDouble(ReadValue("PSearch", "SideXLength"));
            Region.PRegion.SideYLength = Convert.ToDouble(ReadValue("PSearch", "SideYLength"));
            Region.PRegion.Rotation = Convert.ToDouble(ReadValue("PSearch", "Rotation"));

            Region.ATrainRegion.CenterX = Convert.ToDouble(ReadValue("ATrain", "CenterX"));
            Region.ATrainRegion.CenterY = Convert.ToDouble(ReadValue("ATrain", "CenterY"));
            Region.ATrainRegion.SideXLength = Convert.ToDouble(ReadValue("ATrain", "SideXLength"));
            Region.ATrainRegion.SideYLength = Convert.ToDouble(ReadValue("ATrain", "SideYLength"));
            Region.ATrainRegion.Rotation = Convert.ToDouble(ReadValue("ATrain", "Rotation"));

            Region.ARegion.CenterX = Convert.ToDouble(ReadValue("ASearch", "CenterX"));
            Region.ARegion.CenterY = Convert.ToDouble(ReadValue("ASearch", "CenterY"));
            Region.ARegion.SideXLength = Convert.ToDouble(ReadValue("ASearch", "SideXLength"));
            Region.ARegion.SideYLength = Convert.ToDouble(ReadValue("ASearch", "SideYLength"));
            Region.ARegion.Rotation = Convert.ToDouble(ReadValue("ASearch", "Rotation"));

            Pattern.PTrain = Convert.ToBoolean(ReadValue("Train", "Point"));
            Pattern.ATrain = Convert.ToBoolean(ReadValue("Train", "Angle"));

            if (Pattern.PTrain)
            {
                Load_Image(SetupForm.Instance.PDisplay, "Point");
                ICogImage Image = Load_ImageFile(Path.Combine(Application.StartupPath, "Image", "MasterImage.bmp"));

                if (Image.ToString() == "Cognex.VisionPro.CogImage24PlanarColor")
                    Image = CogImageConvert.GetIntensityImage(Image, 0, 0, Image.Width, Image.Height);

                Pattern.InputImage = Image;
                ToolManager.PMAlign.Instance.TrainPattern(DataStore.Region.Instance.PTrainRegion, DataStore.Pattern.Instance.PPattern);
            }
            if (Pattern.ATrain)
            {
                Load_Image(SetupForm.Instance.ADisplay, "Angle");
                ICogImage Image = Load_ImageFile(Path.Combine(Application.StartupPath, "Image", "MasterImage.bmp"));

                if (Image.ToString() == "Cognex.VisionPro.CogImage24PlanarColor")
                    Image = CogImageConvert.GetIntensityImage(Image, 0, 0, Image.Width, Image.Height);

                Pattern.InputImage = Image;
                ToolManager.PMAlign.Instance.TrainPattern(DataStore.Region.Instance.ATrainRegion, DataStore.Pattern.Instance.APattern);
            }
        }
    }
}