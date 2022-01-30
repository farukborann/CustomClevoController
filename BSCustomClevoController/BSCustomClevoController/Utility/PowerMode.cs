using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace BSCustomClevoController.Utility
{
    class PowerMode
    {
        [DllImport("InsydeDCHU.dll")]
        static extern int SetDCHU_Data(int command, byte[] buffer, int length);

        [DllImport("InsydeDCHU.dll")]
        static extern int WriteAppSettings(int page, int offset, int length, ref byte buffer);

        [DllImport("InsydeDCHU.dll")]
        public static extern int ReadAppSettings(int page, int offset, int length, ref byte buffer);

        public static Mode GetStatus()
        {
            byte mode = 0;
            ReadAppSettings(1, 1, 1, ref mode);
            return LookupMode(mode);
        }

        public enum Mode
        {
            Quiet,
            Powersaving,
            Performance,
            Entertainment,
            Unknown,
        }

        static Mode LookupMode(byte mode) {
            switch (mode)
            {
                case 0 : return Mode.Quiet;
                case 1 : return Mode.Powersaving;
                case 2 : return Mode.Performance;
                case 3 : return Mode.Entertainment;
                default : return Mode.Entertainment;
            }
        }

        public static void SetPowerMode(Mode mode)
        {
            byte value = (byte)mode;
            SetDCHU_Data(121, new byte[4] { value, 0, 0, 25 }, 4);
            WriteAppSettings(1, 1, 1, ref value);
        }
    }
}