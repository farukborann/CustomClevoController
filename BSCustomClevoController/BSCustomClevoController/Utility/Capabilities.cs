using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace BSCustomClevoController.Utility
{
    class Capabilities
    {
        [DllImport("InsydeDCHU.dll")]
        public static extern int GetDCHU_Data_Integer(int command, ref int data);

        [DllImport("InsydeDCHU.dll")]
        static extern int WriteAppSettings(int page, int offset, int length, ref byte buffer);

        [DllImport("InsydeDCHU.dll")]
        static extern int ReadAppSettings(int page, int offset, int length, ref byte buffer);

        public static string[] Check()
        {
            Features features = new Features();
            DoWMIFlag(70, GetWMI(70), ref features);
            DoWMIFlag(82, GetWMI(82), ref features);
            DoWMIFlag(122, GetWMI(122), ref features);
            DoWMIFlag(96, GetWMI(96), ref features);
            DoWMIFlag(60, GetWMI(60), ref features);
            DoWMIFlag(16, GetWMI(16), ref features);

            return GetFeatures(features);
        }

        private static string[] GetFeatures(Features features)
        {
            List<string> props = new List<string>();
            var sb = new StringBuilder();

            foreach (var prop in features.GetType().GetProperties())
            {
                props.Add(prop.Name + ": " + prop.GetValue(features, null));
                //sb.AppendLine(prop.Name + ": " + prop.GetValue(features, null));
            }

            foreach (var field in features.GetType().GetFields())
            {
                props.Add(field.Name + ": " + field.GetValue(features));
                //sb.AppendLine(field.Name + ": " + field.GetValue(features));
            }
            return props.ToArray();
        }

        static int GetWMI(int command)
        {
            int data = 0;
            GetDCHU_Data_Integer(command, ref data);
            return data;
        }

        struct Features
        {
            public bool LoadDefault;
            public int PCbeep;
            public bool OCLoadDefault;
            public int EnergyStar15C;
            public int BatteryChargeControl;
            public bool FanSpeedSupport;
            public bool SupportGamingGroup;
            public bool Support3dHeadphone;
            public bool SupportTpColor;
            public bool CustomFanSupport;
            public bool WakeUpLanSupport;
            public bool SupportCheckNotAirplaneOsd;
            public bool SupportUsbHubReset;
            public bool SupportBootChangeCPUVoltage;
            public bool SupportLightbar;
            public bool SupportFanLess;
            public bool FanExists;
            public bool SupportTpSmbios;
            public bool SupportLogoColor;
            public bool SupportWmi3dHp;
            public bool SupportMSHybrid_dGPUSwicth;
            public bool SupportEthernetLED;
            public bool SupportEss;
            public bool SupportXMP;
            public int SupportGPUOC_WMI;
            public int SupportGPUcustom_WMI;
            public int SupportKBLEDsleep_WMI;
            public bool SupportHW_P_state;
            public bool NoSupportHW_KB_Type;
            public bool CheckUnigine;
            public bool SupportNewMode;
            public int WakeUpLanState;
            public bool SupportAllModeGPUOC;
            public bool SupportAntiDust_Fan;
            public bool SupportDisablePCIE_ASPM;
            public bool SupportPowerMode;
            public bool SupportFlexikey;
            public bool SupportFlexiAccess;
            public bool SupportGPUOC;
            public bool SupportCPUOC;
            public bool SupportFanSpeedSetting;
            public bool SupportEnergySave;
            public bool SupportBatteryUtility;
            public bool SupportFanOffset;
        }

        static void DoWMIFlag(int command, int WMI_Data, ref Features features)
        {
            if (command <= 61)
            {
                if (command <= 16)
                {
                    if (command == 6 || command == 9 || command != 16)
                        return;
                    features.SupportPowerMode = (WMI_Data & 1) == 1;
                    features.SupportFlexikey = (WMI_Data & 2) == 2;
                    features.SupportFlexiAccess = (WMI_Data & 4) == 4;
                    features.SupportGPUOC = (WMI_Data & 32) == 32;
                    features.SupportCPUOC = (WMI_Data & 64) == 64;
                    features.SupportFanSpeedSetting = (WMI_Data & 128) == 128;
                    features.SupportEnergySave = (WMI_Data & 256) == 256;
                    features.SupportBatteryUtility = (WMI_Data & 512) == 512;
                    byte[] bytes = BitConverter.GetBytes(WMI_Data);
                    WriteAppSettings(0, 252, 4, ref bytes[0]);
                }
                else
                {
                    if (command == 17)
                        return;
                    if (command == 60)
                    {
                        features.WakeUpLanState = (WMI_Data & 32) != 32 ? 0 : 1;
                        //features.SetWakeUpOnLan();
                    }
                }
            }
            else if (command <= 82)
            {
                if (command == 68)
                    return;
                if (command != 70)
                {
                    if (command != 82)
                        return;
                    //features.DefaultPowerConverationMode = WMI_Data >> 4;
                    //features.DefaultPowerConverationMode &= 3;
                    if ((WMI_Data & 2097152) == 2097152)
                        features.EnergyStar15C = 1;
                    if ((WMI_Data & 134217728) != 134217728)
                        return;
                    features.BatteryChargeControl = 1;
                }
                else
                {
                    if ((WMI_Data & 1) == 1)
                    {
                        //int num1 = (int)CallingVariations.SetUSB(0);
                        //int num2 = (int)CallingVariations.ReloadActivePowerPlan();
                    }
                    if ((WMI_Data & 4096) == 4096)
                        features.LoadDefault = true;
                    if ((WMI_Data & 8192) == 8192)
                    {
                        features.PCbeep = 1;
                        //CallingVariations.pc_beep_Mute(true);
                    }
                    if ((WMI_Data & 131072) != 131072)
                        return;
                    features.OCLoadDefault = true;
                }
            }
            else if (command != 96)
            {
                if (command == 119 || command != 122)
                    return;
                features.FanSpeedSupport = (WMI_Data & 1) == 1;
                features.SupportGamingGroup = (WMI_Data & 2) == 2;
                features.Support3dHeadphone = (WMI_Data & 4) == 4;
                features.SupportTpColor = (WMI_Data & 8) == 8;
                features.CustomFanSupport = (WMI_Data & 16) == 16;
                features.WakeUpLanSupport = (WMI_Data & 32) == 32;
                if ((WMI_Data & 64) == 64)
                    features.SupportCheckNotAirplaneOsd = true;
                features.SupportUsbHubReset = (WMI_Data & 256) == 256;
                if ((WMI_Data & 512) == 512)
                    features.SupportBootChangeCPUVoltage = true;
                features.SupportTpColor = (WMI_Data & 2048) == 2048;
                features.SupportLightbar = (WMI_Data & 4096) == 4096;
                features.SupportFanLess = (WMI_Data & 32768) == 32768;
                if ((WMI_Data & 65536) == 65536)
                    features.FanExists = false;
                if ((WMI_Data & 131072) == 131072)
                    features.SupportTpSmbios = true;
                if ((WMI_Data & 262144) == 262144)
                    features.SupportLogoColor = true;
                if ((WMI_Data & 524288) == 524288)
                    features.SupportWmi3dHp = true;
                if ((WMI_Data & 1048576) == 1048576)
                    features.SupportMSHybrid_dGPUSwicth = true;
                if ((WMI_Data & 2097152) == 2097152)
                    features.SupportEthernetLED = true;
                if ((WMI_Data & 4194304) == 4194304)
                    features.SupportEss = true;
                features.SupportXMP = (WMI_Data & 16777216) == 16777216;
                features.SupportGPUOC_WMI = (WMI_Data >> 26 & 1) != 1 ? 0 : 1;
                features.SupportGPUcustom_WMI = (WMI_Data >> 27 & 1) != 1 ? 0 : 1;
                features.SupportKBLEDsleep_WMI = (WMI_Data >> 28 & 1) != 1 ? 0 : 1;
                if ((WMI_Data & 1073741824) == 1073741824)
                    features.SupportHW_P_state = true;
                else
                    features.SupportHW_P_state = false;
            }
            else
            {
                features.NoSupportHW_KB_Type = (WMI_Data & 1) == 1;
                features.CheckUnigine = (WMI_Data & 2) == 2;
                features.SupportNewMode = (WMI_Data >> 2 & 1) == 1;
                if ((WMI_Data & 32) == 32)
                    features.SupportAllModeGPUOC = true;
                features.SupportAntiDust_Fan = (WMI_Data >> 7 & 1) == 1;
                features.SupportDisablePCIE_ASPM = (WMI_Data >> 9 & 1) == 1;
                if ((WMI_Data >> 10 & 1) == 1)
                    features.SupportFanOffset = false;
                else
                    features.SupportFanOffset = true;
            }
        }
    }
}