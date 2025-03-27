using System.Windows.Forms;

namespace VisionSystem
{
    internal class Util
    {
        /// <summary>
        /// 도 단위와 라디안 단위 간의 각도를 변환합니다.
        /// </summary>
        /// <param name="convertName">변환 방향: "R"은 Degree → Radian, "D"는 Radian → Degree</param>
        /// <param name="value">변환할 각도 값</param>
        /// <returns>변환된 각도 값 (double)</returns>
        public static double RadianDegreeConvert(string convertName, double value)
        {
            convertName = convertName.ToLower();
            if (convertName == "r") return value * (System.Math.PI / 180);
            else if (convertName == "d") return value * (180 / System.Math.PI);
            return 0;
        }

        /// <summary>
        /// 두 점 사이의 각도를 계산하여 반환합니다. 기준은 화면 상단을 0도로 하며 시계 방향 +, 반시계 방향 - 로 계산됩니다.
        /// </summary>
        /// <param name="x1">첫 번째 점의 X 좌표</param>
        /// <param name="y1">첫 번째 점의 Y 좌표</param>
        /// <param name="x2">두 번째 점의 X 좌표</param>
        /// <param name="y2">두 번째 점의 Y 좌표</param>
        /// <returns>-180 ~ 180 도 사이의 각도 (double)</returns>
        public static double AngleCalculation(double x1, double y1, double x2, double y2)
        {
            double angle = RadianDegreeConvert("D", System.Math.Atan2(y2 - y1, x2 - x1)) - 90;
            if (angle < -180) angle += 360;
            else if (angle >= 360) angle -= 360;    
            return angle;
        }

        /// <summary>
        /// NumericUpDown 컨트롤의 값을 내부 설정 값에 반영합니다.
        /// </summary>
        /// <param name="numeric">값이 변경된 NumericUpDown 컨트롤</param>
        public static void ValueChange(NumericUpDown numeric)
        {
            switch (numeric.Name)
            {
                case "NumAAngle":
                    DataStore.Pattern.AAngle = (double)numeric.Value;
                    break;
                case "NumAThreshold":
                    DataStore.Pattern.AThreshold = (double)numeric.Value;
                    break;
                case "NumPAngle":
                    DataStore.Pattern.PAngle = (double)numeric.Value;
                    break;
                case "NumPThreshold":
                    DataStore.Pattern.PThreshold = (double)numeric.Value;
                    break;
            }
        }

        /// <summary>
        /// 로그 메시지를 ListBox 컨트롤에 출력합니다. 출력 시 타임스탬프가 포함됩니다.
        /// </summary>
        /// <param name="logList">로그를 출력할 ListBox 컨트롤</param>
        /// <param name="msg">출력할 메시지 문자열</param>
        public static string PrintLog(ListBox logList, string msg)
        {
            logList.Items.Add($"[{System.DateTime.Now:yy/MM/dd} {System.DateTime.Now:HH:mm:ss}] {msg}");
            logList.TopIndex = logList.Items.Count - 1;
            return null;
        }

        /// <summary>
        /// 체크박스 상태에 따라 그룹박스를 표시하거나 숨기고, 관련된 항목의 활성화를 설정합니다.
        /// </summary>
        /// <param name="display">화면을 초기화할 대상 CogRecordDisplay</param>
        /// <param name="chkBox">체크 이벤트가 발생한 체크박스</param>
        /// <param name="chkAngle">Angle 관련 체크박스</param>
        /// <param name="chkPoint">Point 관련 체크박스</param>
        /// <param name="pGroup">Point 설정 그룹박스</param>
        /// <param name="aGroup">Angle 설정 그룹박스</param>
        public static void ShowGroupBox(Cognex.VisionPro.CogRecordDisplay display, CheckBox chkBox, CheckBox chkAngle, CheckBox chkPoint, GroupBox pGroup, GroupBox aGroup)
        {
            DisplayClear(display);
            if (chkBox.Checked)
            {
                if (chkBox.Text == "PointPattern")
                {
                    chkAngle.Enabled = false;
                    pGroup.Visible = true;
                    pGroup.Location = new System.Drawing.Point(10, 10);
                }
                else
                {
                    chkPoint.Enabled = false;
                    aGroup.Visible = true;
                    aGroup.Location = new System.Drawing.Point(10, 10);
                }
            }
            else
            {
                if (chkBox.Text == "PointPattern") chkAngle.Enabled = true;
                else chkPoint.Enabled = true;
                if (chkBox.Text == "PointPattern") pGroup.Visible = false;
                else aGroup.Visible = false;
            }
        }

        /// <summary>
        /// 디스플레이에서 모든 고정 및 상호작용 그래픽을 제거합니다.
        /// </summary>
        /// <param name="display">그래픽을 제거할 CogRecordDisplay</param>
        public static void DisplayClear(Cognex.VisionPro.CogRecordDisplay display)
        {
            display.StaticGraphics.Clear();
            display.InteractiveGraphics.Clear();
        }
    }
}