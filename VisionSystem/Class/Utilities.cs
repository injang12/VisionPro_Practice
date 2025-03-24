using System;
using System.Drawing;
using System.Windows.Forms;

using Cognex.VisionPro;

namespace VisionSystem
{
    internal class Utilities
    {
        /// <summary>
        /// 로그 메세지 출력
        /// </summary>
        /// <param name="LogBox">로그 창</param>
        /// <param name="msg">출력 할 로그 메세지</param>
        public static void PrintLog(ListBox LogBox, string msg)
        {
            LogBox.Items.Add($"[{DateTime.Now:yy/MM/dd} {DateTime.Now:HH:mm:ss}] {msg}");
            LogBox.TopIndex = LogBox.Items.Count - 1;
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
                ToolManager.Instance.DisplayClear(Display);
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
    }
}