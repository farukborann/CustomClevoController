using System.Runtime.InteropServices;

namespace BSCustomClevoController.Utility
{
    class Touchpad
    {
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        public static void ToggleTouchpad()
        {
            keybd_event((byte)17, (byte)0, 0U, 0);
            keybd_event((byte)91, (byte)0, 0U, 0);
            keybd_event((byte)135, (byte)0, 0U, 0);
            keybd_event((byte)17, (byte)0, 2U, 0);
            keybd_event((byte)91, (byte)0, 2U, 0);
            keybd_event((byte)135, (byte)0, 2U, 0);
        }
    }
}