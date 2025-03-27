using System.Windows.Forms;

namespace VisionSystem
{
    internal class Util
    {
        /// <summary>
        /// 라디안 or 디그리 변환
        /// </summary>
        /// <param name="convertName">변환 하고 싶은 이름 R or D</param>
        /// <param name="currentValue">변환 전 각도 값</param>
        /// <returns>double</returns>
        public static double RadianDegreeConvert(string convertName, double currentValue)
        {
            convertName = convertName.ToLower();

            switch (convertName)
            {
                case "r":
                    return currentValue * (System.Math.PI / 180);
                case "d":
                    return currentValue * (180.0 / System.Math.PI);
                default:
                    return 0;
            }
        }

        /// <summary>
        /// 두 점의 각도 구하기 및 그래픽 그리기
        /// </summary>
        /// <param name="x1">점1의 x값</param>
        /// <param name="y1">점1의 y값</param>
        /// <param name="x2">점2의 x값</param>
        /// <param name="y2">점2의 y값</param>
        /// <returns>double</returns>
        public static double PointToPointAngleAndGraphics(double x1, double y1, double x2, double y2)
        {
            // 벡터 방향 (dx, dy)
            double dx = x2 - x1;
            double dy = y2 - y1;

            // 각도 계산
            double thetaRad = System.Math.Atan2(dy, dx);

            // 0°를 위쪽(↑)으로 변환 90° 빼기
            double resultAngle = RadianDegreeConvert("D", thetaRad) - 90;

            // -180~180° 범위로 변환
            if (resultAngle < -180) resultAngle += 360;
            else if (resultAngle >= 360) resultAngle -= 360;

            return resultAngle;
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
                    DataStore.Pattern.AAngle = (double)Numeric.Value;
                    break;
                case "NumAThreshold":
                    DataStore.Pattern.AThreshold = (double)Numeric.Value;
                    break;
                case "NumPAngle":
                    DataStore.Pattern.PAngle = (double)Numeric.Value;
                    break;
                case "NumPThreshold":
                    DataStore.Pattern.PThreshold = (double)Numeric.Value;
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
            LogList.Items.Add($"[{System.DateTime.Now:yy/MM/dd} {System.DateTime.Now:HH:mm:ss}] {msg}");
            LogList.TopIndex = LogList.Items.Count - 1;
        }

        /// <summary>
        /// 그룹박스 활성화
        /// </summary>
        /// <param name="Display">사용 할 디스플레이</param>
        /// <param name="ChkBox">이벤트 sender 체크박스</param>
        /// <param name="ChkAngle">angle 체크박스</param>
        /// <param name="ChkPoint">point 체크박스</param>
        /// <param name="PGroup">point 그룹박스</param>
        /// <param name="AGroup">angle 그룹박스</param>
        public static void ShowGroupBox(Cognex.VisionPro.CogRecordDisplay Display, CheckBox ChkBox, CheckBox ChkAngle, CheckBox ChkPoint, GroupBox PGroup, GroupBox AGroup)
        {
            if (ChkBox.Checked)
            {
                switch (ChkBox.Text)
                {
                    case "PointPattern":
                        ChkAngle.Enabled = false;
                        PGroup.Visible = true;
                        PGroup.Location = new System.Drawing.Point(10, 10);
                        break;
                    case "AnglePattern":
                        ChkPoint.Enabled = false;
                        AGroup.Visible = true;
                        AGroup.Location = new System.Drawing.Point(10, 10);
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
        public static void DisplayClear(Cognex.VisionPro.CogRecordDisplay Display)
        {
            Display.StaticGraphics.Clear();
            Display.InteractiveGraphics.Clear();
        }
    }
}