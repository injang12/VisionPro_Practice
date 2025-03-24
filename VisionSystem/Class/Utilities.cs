using System;
using System.Drawing;
using System.Windows.Forms;

using Cognex.VisionPro;

namespace VisionSystem
{
    internal class Utilities
    {
        /// <summary>
        /// 라디안 or 디그리 변환
        /// </summary>
        /// <param name="convertName">변환 하고 싶은 이름 R or D</param>
        /// <param name="currentValue">변환 전 각도 값</param>
        /// <returns>double</returns>
        /// <exception cref="ArgumentException"></exception>
        public static double RadianDegreeConvert(string convertName, double currentValue)
        {
            convertName = convertName.ToLower();

            if (convertName == "r")
                return currentValue * (Math.PI / 180);
            else if (convertName == "d")
                return currentValue * (180.0 / Math.PI);

            throw new ArgumentException("Invalid conversion type. Please enter 'R' or 'D'.");
        }

        /// <summary>
        /// 값 체인지
        /// </summary>
        /// <param name="Numeric">변경 할 컨트롤(Numeric)</param>
        public static void ValueChange(NumericUpDown Numeric)
        {
            switch (Numeric.Name)
            {
                case "NumAAngle":
                    DataStore.Pattern.Instance.AAngle = (double)Numeric.Value;
                    break;
                case "NumAThreshold":
                    DataStore.Pattern.Instance.AThreshold = (double)Numeric.Value;
                    break;
                case "NumPAngle":
                    DataStore.Pattern.Instance.PAngle = (double)Numeric.Value;
                    break;
                case "NumPThreshold":
                    DataStore.Pattern.Instance.PThreshold = (double)Numeric.Value;
                    break;
            }
        }

        /// <summary>
        /// 로그 메세지 출력
        /// </summary>
        /// <param name="LogList">로그 창</param>
        /// <param name="msg">출력 할 로그 메세지</param>
        public static void PrintLog(ListBox LogList, string msg)
        {
            LogList.Items.Add($"[{DateTime.Now:yy/MM/dd} {DateTime.Now:HH:mm:ss}] {msg}");
            LogList.TopIndex = LogList.Items.Count - 1;
        }

        /// <summary>
        /// 체크 박스 선택
        /// </summary>
        public static void SelectedCheckBox(CogRecordDisplay Display, CheckBox ChkBox, CheckBox ChkAngle, CheckBox ChkPoint, GroupBox PGroup, GroupBox AGroup)
        {
            if (ChkBox.Checked)
            {
                switch (ChkBox.Text)
                {
                    case "PointPattern":
                        ChkAngle.Enabled = false;
                        PGroup.Visible = true;
                        PGroup.Location = new Point(10, 10);
                        break;
                    case "AnglePattern":
                        ChkPoint.Enabled = false;
                        AGroup.Visible = true;
                        AGroup.Location = new Point(10, 10);
                        break;
                }
            }
            else
            {
                DisplayClear(Display);
                switch (ChkBox.Text)
                {
                    case "PointPattern":
                        ChkAngle.Enabled = true;
                        PGroup.Visible = false;
                        break;
                    case "AnglePattern":
                        ChkPoint.Enabled = true;
                        AGroup.Visible = false;
                        break;
                }
            }
        }

        /// <summary>
        /// 디스플레이 그래픽 제거
        /// </summary>
        /// <param name="Display">제거 할 디스플레이</param>
        public static void DisplayClear(CogRecordDisplay Display)
        {
            Display.StaticGraphics.Clear();
            Display.InteractiveGraphics.Clear();
        }
    }
}