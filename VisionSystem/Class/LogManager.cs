namespace VisionSystem
{
    internal class LogManager
    {
        /// <summary>
        /// 로그 메세지 출력
        /// </summary>
        /// <param name="LogBox">로그 창</param>
        /// <param name="msg">출력 할 로그 메세지</param>
        public static void PrintLog(System.Windows.Forms.ListBox LogBox, string msg)
        {
            LogBox.Items.Add($"[{System.DateTime.Now:yy/MM/dd} {System.DateTime.Now:HH:mm:ss}] {msg}");
            LogBox.TopIndex = LogBox.Items.Count - 1;
        }
    }
}