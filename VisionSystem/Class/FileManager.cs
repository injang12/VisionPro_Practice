using Cognex.VisionPro;
using Cognex.VisionPro.ImageFile;

using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace VisionSystem
{
    internal class FileManager
    {
        public static FileManager Instance { get; private set; } = new FileManager();

        List<string> SetupImageFileName { get; set; } = new List<string>();
        public List<string> MainImageFileName { get; set; } = new List<string>();
        int ImageIndex { get; set; } = -1;

        [System.Runtime.InteropServices.DllImport("kernel32")]
        static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [System.Runtime.InteropServices.DllImport("kernel32")]
        static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);

        string INIPath { get; set; }

        /// <summary>
        /// INI 파일 경로를 설정합니다.
        /// </summary>
        /// <param name="path">INI 파일 전체 경로</param>
        void Set_INI_Path(string path) => INIPath = path;

        /// <summary>
        /// INI 파일에서 값을 읽어 문자열로 반환합니다.
        /// </summary>
        /// <param name="section">INI 파일의 섹션 이름</param>
        /// <param name="key">섹션 내 키 이름</param>
        /// <returns>해당 키의 문자열 값</returns>
        string ReadValue(string section, string key)
        {
            System.Text.StringBuilder strValue = new System.Text.StringBuilder(255);
            GetPrivateProfileString(section, key, "", strValue, 255, INIPath);
            return strValue.ToString();
        }

        /// <summary>
        /// INI 파일에 문자열 값을 기록합니다.
        /// </summary>
        /// <param name="section">INI 파일의 섹션 이름</param>
        /// <param name="key">섹션 내 키 이름</param>
        /// <param name="value">기록할 값</param>
        void WriteValue(string section, string key, string value) => WritePrivateProfileString(section, key, value, INIPath);

        /// <summary>
        /// 검사에 사용할 이미지 폴더를 지정하고, 이미지 파일 목록을 갱신합니다.
        /// </summary>
        /// <param name="logList">로그 출력용 ListBox</param>
        /// <param name="isSetup">Setup폼 인지 여부</param>
        public void SpecifyFolder(ListBox logList, bool isSetup)
        {
            List<string> FileList = isSetup ? SetupImageFileName : MainImageFileName;

            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    Util.PrintLog(logList, "Failed to specify folder...");
                    return;
                }

                List<string> files = Directory.GetFiles(dialog.SelectedPath).Where(f =>
                    f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                    f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                    f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)).ToList();

                FileList.Clear();
                FileList.AddRange(files);

                if (FileList.Count > 0)
                {
                    ImageIndex = 0;
                    Util.PrintLog(logList, $"Directory specified successfully! {FileList.Count} images found.");
                }
                else Util.PrintLog(logList, "No valid image files found in the selected folder.");
            }
        }

        /// <summary>
        /// 다음 이미지 경로를 반환합니다. 마지막 이미지 이후에는 처음으로 순환됩니다.
        /// </summary>
        /// <param name="logList">로그 출력용 ListBox</param>
        /// <returns>이미지 경로 문자열</returns>
        string NextImage(ListBox logList)
        {
            if (SetupImageFileName.Count == 0) return Util.PrintLog(logList, "The specified folder does not exist...");

            if (++ImageIndex >= SetupImageFileName.Count) ImageIndex = 0;
            return SetupImageFileName[ImageIndex];
        }

        /// <summary>
        /// 이전 이미지 경로를 반환합니다. 처음 이미지 이전에는 마지막으로 순환됩니다.
        /// </summary>
        /// <param name="logList">로그 출력용 ListBox</param>
        /// <returns>이미지 경로 문자열</returns>
        string PreImage(ListBox logList)
        {
            if (SetupImageFileName.Count == 0) return Util.PrintLog(logList, "The specified folder does not exist...");

            if (--ImageIndex < 0) ImageIndex = SetupImageFileName.Count - 1;
            return SetupImageFileName[ImageIndex];
        }

        /// <summary>
        /// 이미지 파일을 불러와 디스플레이에 출력하며, 초기화 여부에 따라 폴더 지정 또는 이미지 순서를 결정합니다.
        /// </summary>
        /// <param name="display">이미지를 출력할 CogRecordDisplay</param>
        /// <param name="logList">로그 출력용 ListBox</param>
        /// <param name="currentImage">현재 이미지 파일명을 표시할 Label</param>
        /// <param name="specify">true: 폴더 지정, false: 이미지 순서 이동</param>
        /// <param name="btnPath">버튼 컨트롤 (BtnPreImage 또는 BtnNextImage)</param>
        public void ImageChange(CogRecordDisplay display, ListBox logList, Label currentImage, bool specify, Button btnPath = null)
        {
            string path = null;

            if (specify)
            {
                SpecifyFolder(logList, true);
                if (SetupImageFileName.Count == 0) return;
                path = SetupImageFileName[0];
            }
            else if (btnPath != null)
            {
                switch (btnPath.Name)
                {
                    case "BtnPreImage":
                        path = PreImage(logList);
                        break;
                    case "BtnNextImage":
                        path = NextImage(logList);
                        break;
                }
            }

            ICogImage image = Load_ImageFile(path);
            if (image == null) return;

            if (image is CogImage24PlanarColor) image = CogImageConvert.GetIntensityImage(image, 0, 0, image.Width, image.Height);

            display.Image = image;
            DataStore.Pattern.InputImage = image;

            ToolManager.RunManual(display, logList);
            currentImage.Text = Path.GetFileName(SetupImageFileName[ImageIndex]);

            GC.Collect();
        }

        /// <summary>
        /// 지정한 경로에 이미지를 저장합니다.
        /// </summary>
        /// <param name="path">저장할 경로</param>
        /// <param name="image">저장할 이미지 객체</param>
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
        /// 지정된 경로에서 이미지를 로드합니다.
        /// </summary>
        /// <param name="imagePath">불러올 이미지 경로</param>
        /// <returns>ICogImage 객체. 실패 시 null 반환</returns>
        public ICogImage Load_ImageFile(string imagePath)
        {
            if (imagePath == null) return null;

            using (CogImageFileTool tool = new CogImageFileTool())
            {
                tool.Operator.Open(imagePath, CogImageFileModeConstants.Read);
                tool.Run();
                return tool.OutputImage;
            }
        }

        /// <summary>
        /// 파일 다이얼로그를 통해 사용자가 선택한 이미지를 로드하고 디스플레이에 출력합니다.
        /// </summary>
        /// <param name="display">이미지를 출력할 CogRecordDisplay</param>
        /// <param name="logList">로그 출력용 ListBox</param>
        public void Load_Image(CogRecordDisplay display, ListBox logList)
        {
            using (OpenFileDialog dlg = new OpenFileDialog
            {
                DefaultExt = "jpg",
                Filter = "JPEG Image (*.jpg)|*.jpg|PNG Image (*.png)|*.png|BMP Image (*.bmp)|*.bmp"
            })
            {
                if (dlg.ShowDialog() != DialogResult.OK || !File.Exists(dlg.FileName)) return;

                using (CogImageFileTool tool = new CogImageFileTool())
                {
                    tool.Operator.Open(dlg.FileName, CogImageFileModeConstants.Read);
                    tool.Run();
                    Util.DisplayClear(display);

                    ICogImage output = tool.OutputImage;
                    ICogImage image = output.ToString() == "Cognex.VisionPro.CogImage24PlanarColor"
                        ? CogImageConvert.GetIntensityImage(output, 0, 0, output.Width, output.Height) : output;

                    display.Image = image;
                    DataStore.Pattern.InputImage = image;
                }
                Util.PrintLog(logList, "Image Load Successful!");
            }
        }

        /// <summary>
        /// 지정된 이름의 이미지를 로드하여 디스플레이에 출력합니다. (경로: Debug\\Image\\{name}.bmp)
        /// </summary>
        /// <param name="display">이미지를 출력할 CogRecordDisplay</param>
        /// <param name="name">이미지 파일명 (확장자 제외)</param>
        void Load_Image(CogRecordDisplay display, string name)
        {
            using (CogImageFileTool ImageFileTool = new CogImageFileTool())
            {
                ImageFileTool.Operator.Open(@Path.Combine(Application.StartupPath, "Image", $"{name}.bmp"), CogImageFileModeConstants.Read);
                ImageFileTool.Run();

                Util.DisplayClear(display);
                display.Image = ImageFileTool.OutputImage;
            }
        }

        /// <summary>
        /// 지정된 영역 정보를 INI 파일에 저장합니다.
        /// </summary>
        /// <param name="section">INI 섹션 이름</param>
        /// <param name="region">저장할 CogRectangleAffine 객체</param>
        void WriteRegion(string section, CogRectangleAffine region)
        {
            WriteValue(section, "CenterX", region.CenterX.ToString());
            WriteValue(section, "CenterY", region.CenterY.ToString());
            WriteValue(section, "SideXLength", region.SideXLength.ToString());
            WriteValue(section, "SideYLength", region.SideYLength.ToString());
            WriteValue(section, "Rotation", region.Rotation.ToString());
        }

        /// <summary>
        /// INI 파일에서 영역 정보를 불러와 지정된 객체에 적용합니다.
        /// </summary>
        /// <param name="section">INI 섹션 이름</param>
        /// <param name="region">적용 대상 CogRectangleAffine 객체</param>
        void ReadRegion(string section, CogRectangleAffine region)
        {
            region.CenterX = Convert.ToDouble(ReadValue(section, "CenterX"));
            region.CenterY = Convert.ToDouble(ReadValue(section, "CenterY"));
            region.SideXLength = Convert.ToDouble(ReadValue(section, "SideXLength"));
            region.SideYLength = Convert.ToDouble(ReadValue(section, "SideYLength"));
            region.Rotation = Convert.ToDouble(ReadValue(section, "Rotation"));
        }

        /// <summary>
        /// 지정된 영역과 이미지를 기반으로 패턴 학습을 수행합니다.
        /// </summary>
        /// <param name="name">패턴 이름 (Point 또는 Angle)</param>
        /// <param name="display">디스플레이 객체</param>
        /// <param name="region">학습에 사용할 검색 영역</param>
        /// <param name="pattern">CogPMAlignTool 인스턴스</param>
        void TrainPattern(string name, CogRecordDisplay display, CogRectangleAffine region, Cognex.VisionPro.PMAlign.CogPMAlignTool pattern)
        {
            Load_Image(display, name);
            string imagePath = Path.Combine(Application.StartupPath, "Image", "MasterImage.bmp");
            ICogImage image = Load_ImageFile(imagePath);

            if (image == null) return;

            if (image.ToString() == "Cognex.VisionPro.CogImage24PlanarColor")
                image = CogImageConvert.GetIntensityImage(image, 0, 0, image.Width, image.Height);

            DataStore.Pattern.InputImage = image;
            ToolManager.PMAlign.Instance.TrainRun(region, pattern);
        }

        /// <summary>
        /// 파라미터를 INI 파일로 저장합니다.
        /// </summary>
        /// <param name="logList">로그 출력용 ListBox</param>
        public void ParamSave(ListBox logList)
        {
            if (MessageBox.Show("Do you want to save it?", "Caution", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

            string path = Path.Combine(Application.StartupPath, "Param");
            string iniPath = Path.Combine(path, "Param.ini");

            Directory.CreateDirectory(path);

            if (!File.Exists(iniPath)) File.Create(iniPath).Dispose();

            Set_INI_Path(iniPath);

            WriteValue("PPattern", "Threshold", DataStore.Pattern.PThreshold.ToString());
            WriteValue("PPattern", "Angle", DataStore.Pattern.PAngle.ToString());
            WriteValue("APattern", "Threshold", DataStore.Pattern.AThreshold.ToString());
            WriteValue("APattern", "Angle", DataStore.Pattern.AAngle.ToString());

            WriteRegion("PTrain", DataStore.RectangleRegion.PTrainRegion);
            WriteRegion("PSearch", DataStore.RectangleRegion.PRegion);
            WriteRegion("ATrain", DataStore.RectangleRegion.ATrainRegion);
            WriteRegion("ASearch", DataStore.RectangleRegion.ARegion);

            WriteValue("Train", "Point", DataStore.Pattern.PTrain.ToString());
            WriteValue("Train", "Angle", DataStore.Pattern.ATrain.ToString());

            Util.PrintLog(logList, "Successfully saved parameters.");
        }

        /// <summary>
        /// INI 파일에서 파라미터를 불러와 설정에 반영하고, 필요 시 패턴 학습을 수행합니다.
        /// </summary>
        /// <param name="pointDisplay">Point 디스플레이</param>
        /// <param name="angleDisplay">Angle 디스플레이</param>
        public void ParamLoad(CogRecordDisplay pointDisplay, CogRecordDisplay angleDisplay)
        {
            string iniPath = Path.Combine(Application.StartupPath, "Param", "Param.ini");

            if (!File.Exists(iniPath)) return;

            Set_INI_Path(iniPath);

            DataStore.Pattern.PThreshold = Convert.ToDouble(ReadValue("PPattern", "Threshold"));
            DataStore.Pattern.PAngle = Convert.ToDouble(ReadValue("PPattern", "Angle"));
            DataStore.Pattern.AThreshold = Convert.ToDouble(ReadValue("APattern", "Threshold"));
            DataStore.Pattern.AAngle = Convert.ToDouble(ReadValue("APattern", "Angle"));

            ReadRegion("PTrain", DataStore.RectangleRegion.PTrainRegion);
            ReadRegion("PSearch", DataStore.RectangleRegion.PRegion);
            ReadRegion("ATrain", DataStore.RectangleRegion.ATrainRegion);
            ReadRegion("ASearch", DataStore.RectangleRegion.ARegion);

            DataStore.Pattern.PTrain = Convert.ToBoolean(ReadValue("Train", "Point"));
            DataStore.Pattern.ATrain = Convert.ToBoolean(ReadValue("Train", "Angle"));

            if (DataStore.Pattern.PTrain) TrainPattern("Point", pointDisplay, DataStore.RectangleRegion.PTrainRegion, DataStore.Pattern.PPattern);
            if (DataStore.Pattern.ATrain) TrainPattern("Angle", angleDisplay, DataStore.RectangleRegion.ATrainRegion, DataStore.Pattern.APattern);
        }
    }
}