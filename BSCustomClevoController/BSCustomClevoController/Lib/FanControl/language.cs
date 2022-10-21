using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace FanSpeedSetting
{
	// Token: 0x02000007 RID: 7
	public class language
	{
		// Token: 0x0600002F RID: 47
		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retStr, int bufferSize, string filePath);

		// Token: 0x06000030 RID: 48
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		// Token: 0x06000031 RID: 49 RVA: 0x000051D4 File Offset: 0x000033D4
		public language()
		{
			this.path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\mtlanguage.ini";
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00005240 File Offset: 0x00003440
		public string Translation(string Text)
		{
			language.GetPrivateProfileString(this.lang, Text, this.Default, this.outStr, this.Size, this.path);
			return this.outStr.ToString();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00005282 File Offset: 0x00003482
		public void Load_language()
		{
			this.lang = this.Check_language();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00005294 File Offset: 0x00003494
		public string Check_language()
		{
			byte[] appdata = Global.insyde.GetAPPData(1, 2, 1);
			bool flag = appdata[0] == 1;
			string result;
			if (flag)
			{
				result = "en-us";
			}
			else
			{
				bool flag2 = appdata[0] == 3;
				if (flag2)
				{
					result = "es";
				}
				else
				{
					bool flag3 = appdata[0] == 4;
					if (flag3)
					{
						result = "de";
					}
					else
					{
						bool flag4 = appdata[0] == 5;
						if (flag4)
						{
							result = "it";
						}
						else
						{
							bool flag5 = appdata[0] == 6;
							if (flag5)
							{
								bool flag6 = Global.support_bit.CustomerID == 5;
								if (flag6)
								{
									result = "ja-ep";
								}
								else
								{
									result = "ja";
								}
							}
							else
							{
								bool flag7 = appdata[0] == 7;
								if (flag7)
								{
									result = "ko";
								}
								else
								{
									bool flag8 = appdata[0] == 8;
									if (flag8)
									{
										result = "zh-cn";
									}
									else
									{
										bool flag9 = appdata[0] == 9;
										if (flag9)
										{
											result = "zh-tw";
										}
										else
										{
											bool flag10 = appdata[0] == 10;
											if (flag10)
											{
												result = "fr-fr";
											}
											else
											{
												bool flag11 = appdata[0] == 11;
												if (flag11)
												{
													result = "fr-ca";
												}
												else
												{
													bool flag12 = appdata[0] == 12;
													if (flag12)
													{
														result = "pt-br";
													}
													else
													{
														bool flag13 = appdata[0] == 13;
														if (flag13)
														{
															result = "pt-pt";
														}
														else
														{
															bool flag14 = appdata[0] == 14;
															if (flag14)
															{
																result = "tr-tr";
															}
															else
															{
																bool flag15 = appdata[0] == 15;
																if (flag15)
																{
																	result = "th-th";
																}
																else
																{
																	bool flag16 = appdata[0] == 16;
																	if (flag16)
																	{
																		result = "vi-vn";
																	}
																	else
																	{
																		bool flag17 = appdata[0] == 17;
																		if (flag17)
																		{
																			result = "ru";
																		}
																		else
																		{
																			bool flag18 = appdata[0] == 18;
																			if (flag18)
																			{
																				result = "ja-ep";
																			}
																			else
																			{
																				result = "en-us";
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
			}
			return result;
		}

		// Token: 0x04000017 RID: 23
		private string path;

		// Token: 0x04000018 RID: 24
		private StringBuilder outStr = new StringBuilder(255);

		// Token: 0x04000019 RID: 25
		private string Default = "null";

		// Token: 0x0400001A RID: 26
		private int Size = 255;

		// Token: 0x0400001B RID: 27
		private string lang = "en-US";
	}
}
