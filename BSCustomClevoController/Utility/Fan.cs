using System;
using System.Runtime.InteropServices;
using static BSCustomClevoController.Utility.Structs;

namespace BSCustomClevoController.Utility
{
    class Fan
    {
        [DllImport("InsydeDCHU.dll")]
        static extern int SetDCHU_Data(int command, byte[] buffer, int length);

        [DllImport("InsydeDCHU.dll")]
        static extern int GetDCHU_Data_Buffer(int command, ref byte buffer);

        [DllImport("InsydeDCHU.dll")]
        static extern int WriteAppSettings(int page, int offset, int length, ref byte buffer);

        [DllImport("InsydeDCHU.dll")]
        public static extern int ReadAppSettings(int page, int offset, int length, ref byte buffer);

        public static FanStatus GetStatus()
        {
            byte[] offset = new byte[4];
            ReadAppSettings(4, 7, 1, ref offset[0]);
            byte[] mode = new byte[4];
            ReadAppSettings(4, 5, 1, ref mode[0]);

            FanStatus status = new FanStatus { Mode = LookupMode(mode[0]), Offset = offset[0] };
            Read_FanInfo(ref status.Fan_CPU, ref status.Fan_GPU1);
            Read_FanSpeed(ref status.Fan_CPU.rpm, ref status.Fan_CPU.percent, ref status.Fan_CPU.remote,
                          ref status.Fan_GPU1.rpm, ref status.Fan_GPU1.percent, ref status.Fan_GPU1.remote);
            return status;
        }

        public static void Read_FanSpeed(ref int rpm_CPU, ref int percent_CPU, ref int remote_CPU,
                                         ref int rpm_GPU, ref int percent_GPU, ref int remote_GPU)
        {
            byte[] numArray = new byte[256];
            GetDCHU_Data_Buffer(12, ref numArray[0]);

            rpm_CPU = numArray[3] + (numArray[2] << 8);
            rpm_CPU = (int)Math.Round(60.0 / (5.56521739130435E-05 * rpm_CPU) * 2.0, 0);
            percent_CPU = (int)Math.Round((double)numArray[16] / byte.MaxValue * 100.0, 0);
            remote_CPU = numArray[18];

            rpm_GPU = numArray[5] + (numArray[4] << 8);
            rpm_GPU = (int)Math.Round(60.0 / (5.56521739130435E-05 * rpm_GPU) * 2.0, 0);
            percent_GPU = (int)Math.Round((double)numArray[19] / byte.MaxValue * 100.0, 0);
            remote_GPU = numArray[21];
        }

        public static void AntiDust()
        {
            SetDCHU_Data(121, new byte[4] { 1, 0, 0, 41 }, 4);
        }

        // As a percentage in range [0-255]
        public static void SetOffset(byte offset)
        {
            SetDCHU_Data(121, new byte[4] { offset, 0, 0, 14 }, 4);
            WriteAppSettings(4, 7, 1, ref new byte[4] { offset, 0, 0, 14 }[0]);
        }

        public static void SetMode(Mode mode)
        {
            SetDCHU_Data(121, new byte[4] { (byte)mode, 0, 0, 1 }, 4);
            WriteAppSettings(4, 5, 1, ref new byte[4] { (byte)mode, 0, 0, 1 }[0]);
        }

        public enum Mode
        {
            Auto = 0,
            Max = 1,
            MaxQ = 5,
            Custom = 6,
            Unknown = 9 // Sonradan Eklendi
        }

        static Mode LookupMode(byte mode)
        {
            switch (mode)
            {
                case 0: return Mode.Auto;
                case 1: return Mode.Max;
                case 5: return Mode.MaxQ;
                case 6: return Mode.Custom;
                default: return Mode.Unknown;
            };
        }

        public static void SetCustomFanTable(FanInfo CPU, FanInfo GPU)
        {
            byte[] buffer = new byte[256];
            buffer[2] = CPU.T2;
            buffer[3] = (byte)Math.Round(CPU.D2 / 100.0 * byte.MaxValue, 0);
            buffer[4] = CPU.T3;
            buffer[5] = (byte)Math.Round(CPU.D3 / 100.0 * byte.MaxValue, 0);
            buffer[6] = GPU.T2;
            buffer[7] = (byte)Math.Round(GPU.D2 / 100.0 * 255.0, 0);
            buffer[8] = GPU.T3;
            buffer[9] = (byte)Math.Round(GPU.D3 / 100.0 * 255.0, 0);
            int CPU_R12 = (int)Math.Round((CPU.D2 - CPU.D1) / (CPU.T2 - CPU.T1) * 2.55 * 16.0, 0);
            int CPU_R23 = (int)Math.Round((CPU.D3 - CPU.D2) / (CPU.T3 - CPU.T2) * 2.55 * 16.0, 0);
            int CPU_R34 = (int)Math.Round((100 - CPU.D3) / (100 - CPU.T3) * 2.55 * 16.0, 0);
            int GPU_R12 = (int)Math.Round((GPU.D2 - GPU.D1) / (double)(GPU.T2 - GPU.T1) * 2.55 * 16.0, 0);
            int GPU_R23 = (int)Math.Round((GPU.D3 - GPU.D2) / (double)(GPU.T3 - GPU.T2) * 2.55 * 16.0, 0);
            int GPU_R34 = (int)Math.Round((100 - GPU.D2) / (double)(100 - GPU.T3) * 2.55 * 16.0, 0);
            buffer[14] = (byte)(CPU_R12 >> 8);
            buffer[15] = (byte)(CPU_R12 & byte.MaxValue);
            buffer[16] = (byte)(CPU_R23 >> 8);
            buffer[17] = (byte)(GPU_R23 & byte.MaxValue);
            buffer[18] = (byte)(GPU_R34 >> 8);
            buffer[19] = (byte)(GPU_R34 & byte.MaxValue);
            SetDCHU_Data(14, buffer, buffer.Length);
            SetMode(Mode.Custom);
            SetFanInfo(CPU, GPU);
        }

        static void SetFanInfo(FanInfo CPU, FanInfo GPU)
        {
            byte[] offset = new byte[4];
            ReadAppSettings(4, 7, 1, ref offset[0]);
            byte[] mode = new byte[4];
            ReadAppSettings(4, 5, 1, ref mode[0]);

            byte[] buffer = new byte[256];
            buffer[0] = 3; //major
            buffer[1] = 0; //minor
            buffer[2] = 0; //build
            buffer[3] = 0; //revision
            buffer[4] = 8;
            buffer[5] = mode[0];
            buffer[6] = 1;
            buffer[7] = offset[0];
            buffer[16] = CPU.D1;
            buffer[17] = CPU.D2;
            buffer[18] = CPU.D3;
            buffer[19] = 100;
            buffer[22] = CPU.T1;
            buffer[23] = CPU.T2;
            buffer[24] = CPU.T3;
            buffer[25] = 100;
            int CPU_R12 = (int)Math.Round((CPU.D2 - CPU.D1) / (CPU.T2 - CPU.T1) * 2.55 * 16.0, 0);
            int CPU_R23 = (int)Math.Round((CPU.D3 - CPU.D2) / (CPU.T3 - CPU.T2) * 2.55 * 16.0, 0);
            int CPU_R34 = (int)Math.Round((100 - CPU.D3) / (100 - CPU.T3) * 2.55 * 16.0, 0);
            buffer[28] = (byte)(CPU_R12 & byte.MaxValue);
            buffer[29] = (byte)(CPU_R12 >> 8);
            buffer[30] = (byte)(CPU_R23 & byte.MaxValue);
            buffer[31] = (byte)(CPU_R23 >> 8);
            buffer[32] = (byte)(CPU_R34 & byte.MaxValue);
            buffer[33] = (byte)(CPU_R34 >> 8);
            buffer[34] = GPU.D1;
            buffer[35] = GPU.D2;
            buffer[36] = GPU.D3;
            buffer[37] = 100;
            buffer[38] = GPU.T1;
            buffer[39] = GPU.T2;
            buffer[40] = GPU.T3;
            buffer[41] = 100;
            int GPU_R12 = (int)Math.Round((GPU.D2 - GPU.D1) / (double)(GPU.T2 - GPU.T1) * 2.55 * 16.0, 0);
            int GPU_R23 = (int)Math.Round((GPU.D3 - GPU.D2) / (double)(GPU.T3 - GPU.T2) * 2.55 * 16.0, 0);
            int GPU_R34 = (int)Math.Round((100 - GPU.D3) / (double)(100 - GPU.T3) * 2.55 * 16.0, 0);
            buffer[42] = (byte)(GPU_R12 & byte.MaxValue);
            buffer[43] = (byte)(GPU_R12 >> 8);
            buffer[44] = (byte)(GPU_R23 & byte.MaxValue);
            buffer[45] = (byte)(GPU_R23 >> 8);
            buffer[46] = (byte)(GPU_R34 & byte.MaxValue);
            buffer[47] = (byte)(GPU_R34 >> 8);

            WriteAppSettings(4, 0, 256, ref buffer[0]);
        }

        static void Read_FanInfo(ref FanInfo Fan_CPU, ref FanInfo Fan_GPU1)
        {
            byte[] array = new byte[256];
            ReadAppSettings(4, 0, 256, ref array[0]);

            Fan_CPU.D1 = array[16];
            Fan_CPU.D2 = array[17];
            Fan_CPU.D3 = array[18];
            Fan_CPU.D4 = 100;
            Fan_CPU.D2_Default = array[20];
            Fan_CPU.D3_Default = array[21];
            Fan_CPU.T1 = array[22];
            Fan_CPU.T2 = array[23];
            Fan_CPU.T3 = array[24];
            Fan_CPU.T4 = 100;
            Fan_CPU.T2_Default = array[26];
            Fan_CPU.T3_Default = array[27];
            Fan_CPU.R12 = (array[29] << 8) + array[28];
            Fan_CPU.R23 = (array[31] << 8) + array[30];
            Fan_CPU.R34 = (array[33] << 8) + array[32];
            Fan_GPU1.D1 = array[34];
            Fan_GPU1.D2 = array[35];
            Fan_GPU1.D3 = array[36];
            Fan_GPU1.D4 = 100;
            Fan_GPU1.D2_Default = array[38];
            Fan_GPU1.D3_Default = array[39];
            Fan_GPU1.T1 = array[40];
            Fan_GPU1.T2 = array[41];
            Fan_GPU1.T3 = array[42];
            Fan_GPU1.T4 = 100;
            Fan_GPU1.T2_Default = array[44];
            Fan_GPU1.T3_Default = array[45];
            Fan_GPU1.R12 = (array[47] << 8) + array[46];
            Fan_GPU1.R23 = (array[49] << 8) + array[48];
            Fan_GPU1.R34 = (array[51] << 8) + array[50];

            /*fanCurveInfos[0] = new FanCurveInfo { Temp = numArray2[22], Speed = numArray2[16] };
            fanCurveInfos[1] = new FanCurveInfo { Temp = numArray2[23], Speed = numArray2[17] };
            fanCurveInfos[2] = new FanCurveInfo { Temp = numArray2[24], Speed = numArray2[18] };
            fanCurveInfos[3] = new FanCurveInfo { Temp = numArray2[40], Speed = numArray2[34] };
            fanCurveInfos[4] = new FanCurveInfo { Temp = numArray2[41], Speed = numArray2[35] };
            fanCurveInfos[5] = new FanCurveInfo { Temp = numArray2[42], Speed = numArray2[36] };

            return fanCurveInfos;*/


        }
    }
}