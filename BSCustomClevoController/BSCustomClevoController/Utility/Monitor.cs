using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace BSCustomClevoController.Utility
{
    class MonitorControl
    {
        private const int SC_MONITORPOWER = 61808;
        private const int WM_SYSCOMMAND = 274;

        [DllImport("user32.dll")]
        private static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern int PostMessage(int hWnd, int Msg, int wParam, int lParam);

        public static void ChangeMonitorState(MonitorMode mode)
        {
            //Thread.Sleep(200);
            PostMessage(-1, 274, 61808, (int)mode);
        }

        public void MonitorOff()
        {
            ChangeMonitorState(MonitorMode.MONITOR_OFF);
        }

        public void MonitorOn()
        {
            ChangeMonitorState(MonitorMode.MONITOR_ON);
        }

        public void MonitorStandBy()
        {
            ChangeMonitorState(MonitorMode.MONITOR_STANBY);
        }

        public enum MonitorMode
        {
            MONITOR_ON = -1, // 0xFFFFFFFF
            MONITOR_STANBY = 1,
            MONITOR_OFF = 2,
        }
    }
}