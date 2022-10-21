using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace FanSpeedSetting
{
	// Token: 0x0200000C RID: 12
	public class RWReg
	{
		// Token: 0x060000C7 RID: 199
		[DllImport("kernel32.dll")]
		private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpszReturnBuffer, int nSize, string lpFileName);

		// Token: 0x060000C9 RID: 201 RVA: 0x00006221 File Offset: 0x00004421
		public void Init()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00009800 File Offset: 0x00007A00
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00009846 File Offset: 0x00007A46
		public int window_FullSrceen
		{
			get
			{
				string text = Convert.ToString(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter3.0\\Fan\\", "window_FullSrceen", "null"));
				bool flag = text == "null";
				int result;
				if (flag)
				{
					result = 1;
				}
				else
				{
					result = (int)Convert.ToInt16(text);
				}
				return result;
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter3.0\\Fan\\", "window_FullSrceen", value.ToString());
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00009860 File Offset: 0x00007A60
		// (set) Token: 0x060000CD RID: 205 RVA: 0x000098AA File Offset: 0x00007AAA
		public int window_Color
		{
			get
			{
				string text = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\UI\\", "window_Color", "null").ToString();
				bool flag = text == "null";
				int result;
				if (flag)
				{
					result = 10617087;
				}
				else
				{
					result = Convert.ToInt32(text);
				}
				return result;
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\UI\\", "window_Color", value.ToString());
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000098C4 File Offset: 0x00007AC4
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000098F2 File Offset: 0x00007AF2
		public int First_run
		{
			get
			{
				return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter3.0\\Fan\\", "First_run", 0));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter3.0\\Fan\\", "First_run", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000990C File Offset: 0x00007B0C
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00009939 File Offset: 0x00007B39
		public string ECVersion
		{
			get
			{
				return Convert.ToString(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter3.0\\", "ECVersion", "1."));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter3.0\\", "ECVersion", value, RegistryValueKind.String);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00009950 File Offset: 0x00007B50
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x0000997E File Offset: 0x00007B7E
		public int window_Width
		{
			get
			{
				return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter3.0\\Fan\\", "window_Width", 0));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter3.0\\Fan\\", "window_Width", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00009998 File Offset: 0x00007B98
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x000099C6 File Offset: 0x00007BC6
		public int window_Height
		{
			get
			{
				return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter3.0\\Fan\\", "window_Height", 0));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter3.0\\Fan\\", "window_Height", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000099E0 File Offset: 0x00007BE0
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00009A26 File Offset: 0x00007C26
		public int UAC_Behavior
		{
			get
			{
				string text = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", "ConsentPromptBehaviorAdmin", "5").ToString();
				bool flag = text == "null";
				int result;
				if (flag)
				{
					result = 5;
				}
				else
				{
					result = Convert.ToInt32(text);
				}
				return result;
			}
			set
			{
				Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", "ConsentPromptBehaviorAdmin", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00009A40 File Offset: 0x00007C40
		public int Support_Page_SystemMonitor()
		{
			return Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\UI\\", "Support_Page_SystemMonitor", "0"));
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00009A6C File Offset: 0x00007C6C
		public int Support_Page_LEDDevice()
		{
			return Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\UI\\", "Support_Page_LEDDevice", "0"));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00009A98 File Offset: 0x00007C98
		public string Check_language()
		{
			string text = Convert.ToString(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter3.0\\Fan\\", "language", ""));
			bool flag = text == "";
			if (flag)
			{
				text = CultureInfo.CurrentCulture.Name.ToLower();
				this.WriteLanguage(text);
			}
			bool flag2 = text.Contains("en");
			if (flag2)
			{
				bool flag3 = text == "en-us";
				if (flag3)
				{
					Global.Flexikey_LED_Language = 0;
				}
				else
				{
					Global.Flexikey_LED_Language = 1;
				}
				text = "en-us";
			}
			else
			{
				bool flag4 = text.Contains("es");
				if (flag4)
				{
					text = "es";
					Global.Flexikey_LED_Language = 1;
				}
				else
				{
					bool flag5 = text.Contains("de");
					if (flag5)
					{
						text = "de";
						Global.Flexikey_LED_Language = 1;
					}
					else
					{
						bool flag6 = text.Contains("it");
						if (flag6)
						{
							text = "it";
							Global.Flexikey_LED_Language = 1;
						}
						else
						{
							bool flag7 = text.Contains("ja");
							if (flag7)
							{
								text = "ja";
								Global.Flexikey_LED_Language = 2;
							}
							else
							{
								bool flag8 = text.Contains("ko");
								if (flag8)
								{
									text = "ko";
									Global.Flexikey_LED_Language = 0;
								}
								else
								{
									bool flag9 = text.Contains("zh-cn");
									if (flag9)
									{
										text = "zh-cn";
										Global.Flexikey_LED_Language = 0;
									}
									else
									{
										bool flag10 = text.Contains("zh-tw");
										if (flag10)
										{
											text = "zh-tw";
											Global.Flexikey_LED_Language = 0;
										}
										else
										{
											bool flag11 = text.Contains("fr-fr") || text.Contains("zh-fr");
											if (flag11)
											{
												text = "fr-fr";
												Global.Flexikey_LED_Language = 1;
											}
											else
											{
												bool flag12 = text.Contains("fr-ca") || text.Contains("zh-ca");
												if (flag12)
												{
													text = "fr-ca";
													Global.Flexikey_LED_Language = 1;
												}
												else
												{
													bool flag13 = text.Contains("pt-br") || text.Contains("zh-br");
													if (flag13)
													{
														text = "pt-br";
														Global.Flexikey_LED_Language = 3;
													}
													else
													{
														bool flag14 = text.Contains("pt-pt") || text.Contains("zh-pt");
														if (flag14)
														{
															text = "pt-pt";
															Global.Flexikey_LED_Language = 1;
														}
														else
														{
															bool flag15 = text.Contains("tr-tr") || text.Contains("zh-tr");
															if (flag15)
															{
																text = "tr-tr";
																Global.Flexikey_LED_Language = 1;
															}
															else
															{
																bool flag16 = text.Contains("th-th") || text.Contains("zh-th");
																if (flag16)
																{
																	text = "th-th";
																	Global.Flexikey_LED_Language = 0;
																}
																else
																{
																	bool flag17 = text.Contains("vi-vn") || text.Contains("zh-vn");
																	if (flag17)
																	{
																		text = "vi-vn";
																		Global.Flexikey_LED_Language = 0;
																	}
																	else
																	{
																		bool flag18 = text.Contains("ru");
																		if (flag18)
																		{
																			text = "ru";
																			Global.Flexikey_LED_Language = 1;
																		}
																		else
																		{
																			text = "en-us";
																			Global.Flexikey_LED_Language = 0;
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00009DC0 File Offset: 0x00007FC0
		public int KB_language()
		{
			return Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\UI\\", "KB_language", "0"));
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00009DEB File Offset: 0x00007FEB
		private void WriteLanguage(string strLang)
		{
			Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter3.0\\Fan\\", "language", strLang);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00009E00 File Offset: 0x00008000
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00009E2D File Offset: 0x0000802D
		public int GPUOC_Freq
		{
			get
			{
				return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\GPU", "GPUOC_Freq", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\GPU", "GPUOC_Freq", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00009E48 File Offset: 0x00008048
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00009E75 File Offset: 0x00008075
		public int GPUOC2_Freq
		{
			get
			{
				return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\GPU", "GPUOC2_Freq", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\GPU", "GPUOC2_Freq", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00009E90 File Offset: 0x00008090
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00009EBD File Offset: 0x000080BD
		public int GPUMOC_Freq
		{
			get
			{
				return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\GPU", "GPUMOC_Freq", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\GPU", "GPUMOC_Freq", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00009ED8 File Offset: 0x000080D8
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00009F05 File Offset: 0x00008105
		public int GPUMOC2_Freq
		{
			get
			{
				return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\GPU", "GPUMOC2_Freq", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\GPU", "GPUMOC2_Freq", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00009F20 File Offset: 0x00008120
		public int GPU_CoreINC_MAX()
		{
			return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\GPU", "GPUOCMax", "0"));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00009F50 File Offset: 0x00008150
		public int GPU_CoreINC_MIN()
		{
			return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\GPU", "GPUOCMin", "0"));
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00009F80 File Offset: 0x00008180
		public int GPU_VRAMINC_MAX()
		{
			return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\GPU", "MEMOCMax", "0"));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00009FB0 File Offset: 0x000081B0
		public int GPU_VRAMINC_MIN()
		{
			return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\GPU", "MEMOCMin", "0"));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00009FE0 File Offset: 0x000081E0
		public int WMI_6()
		{
			string value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\WMI\\", "WMI_6", "0").ToString();
			return Convert.ToInt32(value);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000A014 File Offset: 0x00008214
		public int WMI_8()
		{
			string value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\WMI\\", "WMI_8", "0").ToString();
			return Convert.ToInt32(value);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000A048 File Offset: 0x00008248
		public int WMI_69()
		{
			string value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\WMI\\", "WMI_69", "0").ToString();
			return Convert.ToInt32(value);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000A07C File Offset: 0x0000827C
		public int WMI_70()
		{
			string value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\WMI\\", "WMI_70", "0").ToString();
			return Convert.ToInt32(value);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000A0B0 File Offset: 0x000082B0
		public int WMI_122()
		{
			string value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\WMI\\", "WMI_122", "0").ToString();
			return Convert.ToInt32(value);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000A0E4 File Offset: 0x000082E4
		public int Get_GPUSwitchStatus()
		{
			string value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\WMI\\", "WMI_84", "0").ToString();
			return (int)Convert.ToInt16(value);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000A118 File Offset: 0x00008318
		public int Get_TP()
		{
			string value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\WMI\\", "WMI_42", "0").ToString();
			return (int)Convert.ToInt16(value);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000A14C File Offset: 0x0000834C
		public int Get_CCD()
		{
			string value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\WMI\\", "WMI_34", "0").ToString();
			return (int)Convert.ToInt16(value);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000A180 File Offset: 0x00008380
		public int Get_LeftWinKey()
		{
			string value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0", "LeftWinKey", "1").ToString();
			return Convert.ToInt32(value);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000A1B4 File Offset: 0x000083B4
		public bool Get_DisableAirplane()
		{
			return Convert.ToBoolean(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0", "Disable_Airplane", null));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000A1DC File Offset: 0x000083DC
		public bool Get_SupportDocking()
		{
			return Convert.ToBoolean(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0", "SupportDocking", null));
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000A204 File Offset: 0x00008404
		public int Get_DockingStatus()
		{
			string value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0", "DockingStatus", "0").ToString();
			return Convert.ToInt32(value);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000A238 File Offset: 0x00008438
		public int Get_Fan_Table_duty(int FanIndex, int DutyIndex)
		{
			string text = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC\\Fan" + FanIndex.ToString(), "D" + DutyIndex.ToString(), "null").ToString();
			bool flag = text == "null";
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(text);
			}
			return result;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000A296 File Offset: 0x00008496
		public void Set_Fan_Table_duty(int FanIndex, int DutyIndex, int value)
		{
			Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC\\Fan" + FanIndex.ToString(), "D" + DutyIndex.ToString(), value, RegistryValueKind.DWord);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000A2C8 File Offset: 0x000084C8
		public int Get_Fan_Table_temp(int FanIndex, int TempIndex)
		{
			string text = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC\\Fan" + FanIndex.ToString(), "T" + TempIndex.ToString(), "null").ToString();
			bool flag = text == "null";
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(text);
			}
			return result;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000A326 File Offset: 0x00008526
		public void Set_Fan_Table_temp(int FanIndex, int TempIndex, int value)
		{
			Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC\\Fan" + FanIndex.ToString(), "T" + TempIndex.ToString(), value, RegistryValueKind.DWord);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000A358 File Offset: 0x00008558
		public int Get_Fan_Table_duty_default(int FanIndex, int DutyIndex)
		{
			string text = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC\\Fan" + FanIndex.ToString(), "Default_D" + DutyIndex.ToString(), "null").ToString();
			bool flag = text == "null";
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(text);
			}
			return result;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000A3B8 File Offset: 0x000085B8
		public int Get_Fan_Table_temp_default(int FanIndex, int TempIndex)
		{
			string text = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC\\Fan" + FanIndex.ToString(), "Default_T" + TempIndex.ToString(), "null").ToString();
			bool flag = text == "null";
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(text);
			}
			return result;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000A418 File Offset: 0x00008618
		public int CPUFan_duty()
		{
			string text = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC\\Fan1", "duty", "0").ToString();
			bool flag = text == "null";
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(text);
			}
			return result;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000A460 File Offset: 0x00008660
		public int GPUFan_duty()
		{
			string text = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC\\Fan2", "duty", "0").ToString();
			bool flag = text == "null";
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(text);
			}
			return result;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000A4A8 File Offset: 0x000086A8
		public int GPUFan2_duty()
		{
			string text = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC\\Fan3", "duty", "0").ToString();
			bool flag = text == "null";
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(text);
			}
			return result;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000A4F0 File Offset: 0x000086F0
		public int CPUFan_rpm()
		{
			string text = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC\\Fan1", "rpm", "0").ToString();
			bool flag = text == "null";
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(text);
			}
			return result;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000A538 File Offset: 0x00008738
		public int GPUFan_rpm()
		{
			string text = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC\\Fan2", "rpm", "0").ToString();
			bool flag = text == "null";
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(text);
			}
			return result;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000A580 File Offset: 0x00008780
		public int GPUFan2_rpm()
		{
			string text = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC\\Fan3", "rpm", "0").ToString();
			bool flag = text == "null";
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(text);
			}
			return result;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000A5C8 File Offset: 0x000087C8
		public int FanMode()
		{
			string text = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC", "FanMode", "0").ToString();
			bool flag = text == "null";
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(text);
			}
			return result;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000A610 File Offset: 0x00008810
		public int FanOffset()
		{
			string text = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC", "FanOffset", "0").ToString();
			bool flag = text == "null";
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(text);
			}
			return result;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000A658 File Offset: 0x00008858
		public int FanCount()
		{
			string text = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC", "FanCount", "0").ToString();
			bool flag = text == "null";
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(text);
			}
			return result;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000A6A0 File Offset: 0x000088A0
		// (set) Token: 0x06000105 RID: 261 RVA: 0x0000A6CD File Offset: 0x000088CD
		public byte FanOCMode
		{
			get
			{
				return Convert.ToByte(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC", "FanOCMode", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC", "FanOCMode", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000A6E8 File Offset: 0x000088E8
		// (set) Token: 0x06000107 RID: 263 RVA: 0x0000A70F File Offset: 0x0000890F
		public bool SupportFanLess
		{
			get
			{
				return Convert.ToBoolean(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0", "SupportFanLess", null));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0", "SupportFanLess", value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000A728 File Offset: 0x00008928
		public bool SupportMaxqFan
		{
			get
			{
				return Convert.ToBoolean(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\EC\\", "SupportMaxQ_FAN", null));
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000A750 File Offset: 0x00008950
		public void LoadCurrentMode()
		{
			int num = (int)Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\wmi", "WMI_96", null);
			bool flag = ((num >> 2) & 1) == 1;
			if (flag)
			{
				this.SupportNewMode = true;
			}
			else
			{
				this.SupportNewMode = false;
			}
			bool supportNewMode = this.SupportNewMode;
			if (supportNewMode)
			{
				this.CurrentMode2 = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\", "CurrentMode2", "entertainment").ToString();
			}
			this.CurrentMode = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\", "CurrentMode", "entertainment").ToString();
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\ControlCenter2.0\\" + this.CurrentMode, false);
			this.CCD = (int)registryKey.GetValue("CCD");
			this.TouchPad = (int)Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0", "TouchPad", null);
			this.AirplaneMode = (int)registryKey.GetValue("AirplaneMode");
			this.HeadPhone = (int)registryKey.GetValue("HeadPhone");
			this.KBSleepTimer = (int)registryKey.GetValue("KBSleepTimer");
			this.KBSleepTimerOn = (((long)this.KBSleepTimer & (long)(int.MinValue)).Equals(0L) ? 1 : 0);
			this.KBSleepTimer &= 268435455;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000A8A0 File Offset: 0x00008AA0
		public int KBType()
		{
			return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED", "KBType", "0"));
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		// (set) Token: 0x0600010C RID: 268 RVA: 0x0000A8FD File Offset: 0x00008AFD
		public int PerkeySpeed
		{
			get
			{
				return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED", "KBSpeed", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED", "KBSpeed", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000A918 File Offset: 0x00008B18
		// (set) Token: 0x0600010E RID: 270 RVA: 0x0000A945 File Offset: 0x00008B45
		public int KBStatus
		{
			get
			{
				return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED", "KBStatus", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED", "KBStatus", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000A960 File Offset: 0x00008B60
		// (set) Token: 0x06000110 RID: 272 RVA: 0x0000A98D File Offset: 0x00008B8D
		public int KBbrightness
		{
			get
			{
				return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED", "KBbrightness", "3"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED", "KBbrightness", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000A9A8 File Offset: 0x00008BA8
		public bool SupportSelectBootEffect()
		{
			int num = Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\", "SupportKBSelectBootEffect", "0"));
			bool flag = num == 0;
			return !flag;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		public int SelectBootEffect()
		{
			return Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\", "KBSelectBootEffect", "0"));
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000AA10 File Offset: 0x00008C10
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000AA3D File Offset: 0x00008C3D
		public int KB_RGB_Mode
		{
			get
			{
				return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED", "KB_RGB_Mode", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED", "KB_RGB_Mode", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000AA58 File Offset: 0x00008C58
		public void Set_KB_RGB(string part, int R, int G, int B)
		{
			Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\" + part, "R", R, RegistryValueKind.DWord);
			Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\" + part, "G", G, RegistryValueKind.DWord);
			Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\" + part, "B", B, RegistryValueKind.DWord);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		public int Get_KB_R(string part)
		{
			string value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED" + part, "R", "0").ToString();
			return Convert.ToInt32(value);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		public int Get_KB_G(string part)
		{
			string value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED" + part, "G", "0").ToString();
			return Convert.ToInt32(value);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000AB30 File Offset: 0x00008D30
		public int Get_KB_B(string part)
		{
			string value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED" + part, "B", "0").ToString();
			return Convert.ToInt32(value);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000AB68 File Offset: 0x00008D68
		public void Set_Perkey_Static_RGB(string id, byte R, byte G, byte B)
		{
			int num = ((int)R << 16) + ((int)G << 8) + (int)B;
			Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\Static", id, num, RegistryValueKind.DWord);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000AB98 File Offset: 0x00008D98
		public int Get_Perkey_Static_RGB(string id)
		{
			string value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\Static", id, "0").ToString();
			return Convert.ToInt32(value);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		// (set) Token: 0x0600011C RID: 284 RVA: 0x0000ABF5 File Offset: 0x00008DF5
		public int Perkey_EffectMode
		{
			get
			{
				return Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "EffectMode", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "EffectMode", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000AC10 File Offset: 0x00008E10
		// (set) Token: 0x0600011E RID: 286 RVA: 0x0000AC3D File Offset: 0x00008E3D
		public int Perkey_BlinkStatus
		{
			get
			{
				return Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "BlinkStatus", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "BlinkStatus", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000AC58 File Offset: 0x00008E58
		// (set) Token: 0x06000120 RID: 288 RVA: 0x0000AC85 File Offset: 0x00008E85
		public byte Blink_R
		{
			get
			{
				return Convert.ToByte(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Blink_R", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Blink_R", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000ACA0 File Offset: 0x00008EA0
		// (set) Token: 0x06000122 RID: 290 RVA: 0x0000ACCD File Offset: 0x00008ECD
		public byte Blink_G
		{
			get
			{
				return Convert.ToByte(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Blink_G", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Blink_G", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000ACE8 File Offset: 0x00008EE8
		// (set) Token: 0x06000124 RID: 292 RVA: 0x0000AD15 File Offset: 0x00008F15
		public byte Blink_B
		{
			get
			{
				return Convert.ToByte(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Blink_B", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Blink_B", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000AD30 File Offset: 0x00008F30
		// (set) Token: 0x06000126 RID: 294 RVA: 0x0000AD5D File Offset: 0x00008F5D
		public int Perkey_BreathStatus
		{
			get
			{
				return Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "BreathStatus", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "BreathStatus", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000AD78 File Offset: 0x00008F78
		// (set) Token: 0x06000128 RID: 296 RVA: 0x0000ADA5 File Offset: 0x00008FA5
		public byte Breath_R
		{
			get
			{
				return Convert.ToByte(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Breath_R", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Breath_R", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000ADC0 File Offset: 0x00008FC0
		// (set) Token: 0x0600012A RID: 298 RVA: 0x0000ADED File Offset: 0x00008FED
		public byte Breath_G
		{
			get
			{
				return Convert.ToByte(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Breath_G", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Breath_G", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000AE08 File Offset: 0x00009008
		// (set) Token: 0x0600012C RID: 300 RVA: 0x0000AE35 File Offset: 0x00009035
		public byte Breath_B
		{
			get
			{
				return Convert.ToByte(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Breath_B", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Breath_B", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000AE50 File Offset: 0x00009050
		// (set) Token: 0x0600012E RID: 302 RVA: 0x0000AE7D File Offset: 0x0000907D
		public int Perkey_RippleStatus
		{
			get
			{
				return Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "RippleStatus", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "RippleStatus", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000AE98 File Offset: 0x00009098
		// (set) Token: 0x06000130 RID: 304 RVA: 0x0000AEC5 File Offset: 0x000090C5
		public byte Ripple_R
		{
			get
			{
				return Convert.ToByte(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Ripple_R", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Ripple_R", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000AEE0 File Offset: 0x000090E0
		// (set) Token: 0x06000132 RID: 306 RVA: 0x0000AF0D File Offset: 0x0000910D
		public byte Ripple_G
		{
			get
			{
				return Convert.ToByte(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Ripple_G", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Ripple_G", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000AF28 File Offset: 0x00009128
		// (set) Token: 0x06000134 RID: 308 RVA: 0x0000AF55 File Offset: 0x00009155
		public byte Ripple_B
		{
			get
			{
				return Convert.ToByte(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Ripple_B", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "Ripple_B", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000AF70 File Offset: 0x00009170
		// (set) Token: 0x06000136 RID: 310 RVA: 0x0000AF9D File Offset: 0x0000919D
		public int Perkey_StaticStatus
		{
			get
			{
				return Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "StaticStatus", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED\\PKB\\", "StaticStatus", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000AFB8 File Offset: 0x000091B8
		public int PerKey_Type()
		{
			return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\LED", "PerKey_Type", "0"));
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000AFE8 File Offset: 0x000091E8
		public string GetTDP()
		{
			return "0";
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000B000 File Offset: 0x00009200
		public int CalCPUTemp(string CPUTDP, int set_remote)
		{
			bool flag = CPUTDP == "84W" || CPUTDP == "88W";
			double value;
			if (flag)
			{
				bool flag2 = set_remote > 60;
				if (flag2)
				{
					value = (double)(set_remote - 12) * 0.33 + 44.0;
				}
				else
				{
					value = (double)(set_remote - 1);
				}
			}
			else
			{
				bool flag3 = CPUTDP == "65W";
				if (flag3)
				{
					bool flag4 = set_remote > 50;
					if (flag4)
					{
						value = (double)(set_remote - 35) * 0.41 + 43.7;
					}
					else
					{
						value = (double)(set_remote - 1);
					}
				}
				else
				{
					bool flag5 = CPUTDP == "91W";
					if (flag5)
					{
						bool flag6 = set_remote > 50;
						if (flag6)
						{
							value = (double)(set_remote - 9) * 0.22 + 43.7;
						}
						else
						{
							value = (double)(set_remote - 5);
						}
					}
					else
					{
						bool flag7 = CPUTDP == "35W";
						if (flag7)
						{
							bool flag8 = set_remote > 32;
							if (flag8)
							{
								value = (double)set_remote * 0.5 + 16.0;
							}
							else
							{
								value = (double)set_remote;
							}
						}
						else
						{
							bool flag9 = CPUTDP == "47W";
							if (flag9)
							{
								bool flag10 = set_remote > 26;
								if (flag10)
								{
									value = (double)set_remote * 0.5 + 13.0;
								}
								else
								{
									value = (double)set_remote;
								}
							}
							else
							{
								value = (double)set_remote;
							}
						}
					}
				}
			}
			return (int)Math.Round(value, 0);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000B194 File Offset: 0x00009394
		public void UpdateWMI12()
		{
			object value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\wmi", "WMI_12", "");
			byte[] array = (byte[])value;
			this.cpu_temp = this.CalCPUTemp(this.GetTDP(), (int)array[18]);
			this.gpu1_temp = (int)array[21];
			this.gpu2_temp = (int)array[24];
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000B1E8 File Offset: 0x000093E8
		public int CpuTemp()
		{
			return this.cpu_temp;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000B200 File Offset: 0x00009400
		public int GpuTemp(int num)
		{
			bool flag = num == 0;
			int result;
			if (flag)
			{
				result = this.gpu1_temp;
			}
			else
			{
				bool flag2 = num == 1;
				if (flag2)
				{
					result = this.gpu2_temp;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000B238 File Offset: 0x00009438
		// (set) Token: 0x0600013E RID: 318 RVA: 0x0000B265 File Offset: 0x00009465
		public int FlexiKey_KB_Enable
		{
			get
			{
				return Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ComboKey\\", "KBEnable", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ComboKey", "KBEnable", value.ToString());
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000B280 File Offset: 0x00009480
		// (set) Token: 0x06000140 RID: 320 RVA: 0x0000B2AD File Offset: 0x000094AD
		public int FlexiKey_Mouse_Enable
		{
			get
			{
				return Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ComboKey\\", "MouseEnable", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ComboKey", "MouseEnable", value.ToString());
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000B2C8 File Offset: 0x000094C8
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000B2F5 File Offset: 0x000094F5
		public int FlexiAccessStatus
		{
			get
			{
				return Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\FA", "FlexiAccessStatus", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\FA", "FlexiAccessStatus", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000B310 File Offset: 0x00009510
		// (set) Token: 0x06000144 RID: 324 RVA: 0x0000B33D File Offset: 0x0000953D
		public string FA_ClientName
		{
			get
			{
				return Convert.ToString(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\FA", "ClientName", ""));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\FA", "ClientName", value, RegistryValueKind.String);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000B354 File Offset: 0x00009554
		// (set) Token: 0x06000146 RID: 326 RVA: 0x0000B381 File Offset: 0x00009581
		public string FA_Port
		{
			get
			{
				return Convert.ToString(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\FA", "Port", ""));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\FA", "Port", value, RegistryValueKind.String);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000B398 File Offset: 0x00009598
		// (set) Token: 0x06000148 RID: 328 RVA: 0x0000B3C5 File Offset: 0x000095C5
		public string FA_IP
		{
			get
			{
				return Convert.ToString(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\FA", "IP", ""));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\FA", "IP", value, RegistryValueKind.String);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000B3DC File Offset: 0x000095DC
		// (set) Token: 0x0600014A RID: 330 RVA: 0x0000B409 File Offset: 0x00009609
		public string last_RefreshDate
		{
			get
			{
				return Convert.ToString(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\BatteryUtility", "RefreshDate", "null"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\BatteryUtility", "RefreshDate", value, RegistryValueKind.String);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000B420 File Offset: 0x00009620
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000B44D File Offset: 0x0000964D
		public int OngoingStatus
		{
			get
			{
				return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\BatteryUtility", "OngoingStatus", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\BatteryUtility", "OngoingStatus", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000B468 File Offset: 0x00009668
		// (set) Token: 0x0600014E RID: 334 RVA: 0x0000B495 File Offset: 0x00009695
		public int Refresh_ElapsedTime_ss
		{
			get
			{
				return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\BatteryUtility", "Refresh_ElapsedTime_ss", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\BatteryUtility", "Refresh_ElapsedTime_ss", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000B4B0 File Offset: 0x000096B0
		// (set) Token: 0x06000150 RID: 336 RVA: 0x0000B4DD File Offset: 0x000096DD
		public int Refresh_ElapsedTime_mm
		{
			get
			{
				return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\BatteryUtility", "Refresh_ElapsedTime_mm", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\BatteryUtility", "Refresh_ElapsedTime_mm", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000B4F8 File Offset: 0x000096F8
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000B525 File Offset: 0x00009725
		public int Refresh_ElapsedTime_hh
		{
			get
			{
				return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\BatteryUtility", "Refresh_ElapsedTime_hh", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\BatteryUtility", "Refresh_ElapsedTime_hh", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000B540 File Offset: 0x00009740
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000B56D File Offset: 0x0000976D
		public int BAT_log_count
		{
			get
			{
				return (int)Convert.ToInt16(Registry.GetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\BatteryUtility", "BAT_log_count", "0"));
			}
			set
			{
				Registry.SetValue("HKEY_CURRENT_USER\\Software\\ControlCenter2.0\\BatteryUtility", "BAT_log_count", value, RegistryValueKind.DWord);
			}
		}

		// Token: 0x040000BF RID: 191
		public string UI_language = "en-US";

		// Token: 0x040000C0 RID: 192
		private const int CPU_REMOTE = 18;

		// Token: 0x040000C1 RID: 193
		private const int GPU1_REMOTE = 21;

		// Token: 0x040000C2 RID: 194
		private const int GPU2_REMOTE = 24;

		// Token: 0x040000C3 RID: 195
		private int cpu_temp;

		// Token: 0x040000C4 RID: 196
		private int gpu1_temp;

		// Token: 0x040000C5 RID: 197
		private int gpu2_temp;

		// Token: 0x040000C6 RID: 198
		public double barkup_VB_CPUFan_H;

		// Token: 0x040000C7 RID: 199
		public double barkup_VB_CPUFan_W;

		// Token: 0x040000C8 RID: 200
		public double barkup_VB_CPU_Temp_W;

		// Token: 0x040000C9 RID: 201
		public double barkup_VB_CPU_Temp_H;

		// Token: 0x040000CA RID: 202
		public string CurrentMode;

		// Token: 0x040000CB RID: 203
		public string CurrentMode2;

		// Token: 0x040000CC RID: 204
		public int AirplaneMode;

		// Token: 0x040000CD RID: 205
		public int TouchPad;

		// Token: 0x040000CE RID: 206
		public int CCD;

		// Token: 0x040000CF RID: 207
		public int WhiteLedKB_Brightness;

		// Token: 0x040000D0 RID: 208
		public int HeadPhone;

		// Token: 0x040000D1 RID: 209
		public int KBSleepTimer;

		// Token: 0x040000D2 RID: 210
		public int KBSleepTimerOn;

		// Token: 0x040000D3 RID: 211
		public bool SupportNewMode = false;
	}
}
