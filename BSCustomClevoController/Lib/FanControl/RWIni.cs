using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace FanSpeedSetting
{
	// Token: 0x0200000B RID: 11
	public class RWIni
	{
		// Token: 0x060000BE RID: 190
		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retStr, int bufferSize, string filePath);

		// Token: 0x060000BF RID: 191
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		// Token: 0x060000C0 RID: 192
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpszReturnBuffer, int bufferSize, string filePath);

		// Token: 0x060000C1 RID: 193 RVA: 0x00009580 File Offset: 0x00007780
		public RWIni()
		{
			this.path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			this.lang = Global.RW_REG.Check_language();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000095F0 File Offset: 0x000077F0
		public string Translation(string Text)
		{
			RWIni.GetPrivateProfileString(this.lang, Text, this.Default, this.outStr, this.Size, this.path + "\\mtlanguage.ini");
			return this.outStr.ToString();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000963C File Offset: 0x0000783C
		public string Read_OEM(string section, string key)
		{
			RWIni.GetPrivateProfileString(section, key, this.Default, this.outStr, this.Size, this.path + "\\oem.ini");
			return this.outStr.ToString();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00009684 File Offset: 0x00007884
		public string Read_Custom(string section, string key)
		{
			RWIni.GetPrivateProfileString(section, key, this.Default, this.outStr, this.Size, this.path + "\\Custom.ini");
			return this.outStr.ToString();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000096CC File Offset: 0x000078CC
		public string Read_Batery(string section, string key)
		{
			RWIni.GetPrivateProfileString(section, key, this.Default, this.outStr, this.Size, this.path + "\\Battery.ini");
			return this.outStr.ToString();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00009714 File Offset: 0x00007914
		public List<string> Read_OEM_list(string section)
		{
			byte[] array = new byte[128];
			RWIni.GetPrivateProfileSection(section, array, 128, this.path + "\\oem.ini");
			string[] array2 = Encoding.Unicode.GetString(array).Trim(new char[1]).Split(new char[1]);
			bool flag = array2.Length == 0;
			List<string> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = array2.Length == 1 && array2[0] == "";
				if (flag2)
				{
					result = null;
				}
				else
				{
					List<string> list = new List<string>();
					foreach (string text in array2)
					{
						list.Add(text.Substring(0, text.IndexOf("=")));
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x040000BA RID: 186
		private string path;

		// Token: 0x040000BB RID: 187
		private StringBuilder outStr = new StringBuilder(255);

		// Token: 0x040000BC RID: 188
		private string Default = "null";

		// Token: 0x040000BD RID: 189
		private int Size = 255;

		// Token: 0x040000BE RID: 190
		private string lang = "en-US";
	}
}
