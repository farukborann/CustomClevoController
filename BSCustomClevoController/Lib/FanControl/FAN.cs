using System;

namespace FanSpeedSetting
{
	// Token: 0x02000005 RID: 5
	public class FAN
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021AC File Offset: 0x000003AC
		public void Read_FanSpeed()
		{
			byte[] array = new byte[256];
			array = Global.insyde.GetWMIPackage(12);
			this.Fan_CPU.rpm = (int)array[3] + ((int)array[2] << 8);
			this.Fan_CPU.duty = array[16];
			this.Fan_GPU1.rpm = (int)array[5] + ((int)array[4] << 8);
			this.Fan_GPU1.duty = array[19];
			this.Fan_GPU2.rpm = (int)array[7] + ((int)array[6] << 8);
			this.Fan_GPU2.duty = array[22];
			this.Fan_CPU.remote = Global.RW_REG.CalCPUTemp(Global.RW_REG.GetTDP(), (int)array[18]);
			this.Fan_GPU1.remote = (int)array[21];
			this.Fan_GPU2.remote = (int)array[24];
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000227C File Offset: 0x0000047C
		public void Read_WMI13()
		{
			byte[] array = new byte[256];
			array = Global.insyde.GetWMIPackage(13);
			this.FanCount = (int)array[12];
			this.InitFanMode = (int)array[14];
			bool flag = this.InitFanMode == 5;
			if (flag)
			{
				Global.support_bit.MaxQ = true;
			}
			byte b = array[43];
			bool flag2 = this.FanCount <= 1 || ((b >> 1) & 1) == 1;
			if (flag2)
			{
				this.SupportCustomFan = false;
			}
			bool supportCustomFan = this.SupportCustomFan;
			if (supportCustomFan)
			{
				this.Fan_CPU.T1 = array[16];
				this.Fan_CPU.D1 = (byte)Math.Round((double)array[17] / 255.0 * 100.0, 0);
				this.Fan_CPU.T2 = (this.Fan_CPU.T2_Default = array[18]);
				this.Fan_CPU.D2 = (this.Fan_CPU.D2_Default = (byte)Math.Round((double)array[19] / 255.0 * 100.0, 0));
				this.Fan_CPU.T3 = (this.Fan_CPU.T3_Default = array[20]);
				this.Fan_CPU.D3 = (this.Fan_CPU.D3_Default = (byte)Math.Round((double)array[21] / 255.0 * 100.0, 0));
				this.Fan_CPU.T4 = 100;
				this.Fan_CPU.D4 = 100;
				bool flag3 = this.Fan_CPU.D1 >= this.Fan_CPU.D2;
				if (flag3)
				{
					this.Fan_CPU.D2 = this.Fan_CPU.D1--;
					bool flag4 = this.Fan_CPU.D2 >= this.Fan_CPU.D3;
					if (flag4)
					{
						this.Fan_CPU.D3 = this.Fan_CPU.D2++;
					}
					bool flag5 = this.Fan_CPU.D3 >= this.Fan_CPU.D4;
					if (flag5)
					{
						this.Fan_CPU.D3 = this.Fan_CPU.D4--;
					}
				}
				bool flag6 = this.Fan_CPU.D2 >= this.Fan_CPU.D3;
				if (flag6)
				{
					this.Fan_CPU.D3 = this.Fan_CPU.D2++;
					bool flag7 = this.Fan_CPU.D3 >= this.Fan_CPU.D4;
					if (flag7)
					{
						this.Fan_CPU.D3 = this.Fan_CPU.D4--;
					}
					bool flag8 = this.Fan_CPU.D2 >= this.Fan_CPU.D3;
					if (flag8)
					{
						this.Fan_CPU.D2 = this.Fan_CPU.D3--;
					}
				}
				bool flag9 = this.Fan_CPU.D3 >= this.Fan_CPU.D4;
				if (flag9)
				{
					Global.FanArgs fan_CPU = this.Fan_CPU;
					fan_CPU.D3 -= 1;
					bool flag10 = this.Fan_CPU.D2 >= this.Fan_CPU.D3;
					if (flag10)
					{
						this.Fan_CPU.D2 = this.Fan_CPU.D3--;
					}
				}
				this.Fan_CPU.D2_Default = this.Fan_CPU.D2;
				this.Fan_CPU.D3_Default = this.Fan_CPU.D3;
				this.Fan_GPU1.T1 = array[24];
				this.Fan_GPU1.D1 = (byte)Math.Round((double)array[25] / 255.0 * 100.0, 0);
				this.Fan_GPU1.T2 = (this.Fan_GPU1.T2_Default = array[26]);
				this.Fan_GPU1.D2 = (this.Fan_GPU1.D2_Default = (byte)Math.Round((double)array[27] / 255.0 * 100.0, 0));
				this.Fan_GPU1.T3 = (this.Fan_GPU1.T3_Default = array[28]);
				this.Fan_GPU1.D3 = (this.Fan_GPU1.D3_Default = (byte)Math.Round((double)array[29] / 255.0 * 100.0, 0));
				this.Fan_GPU1.T4 = 100;
				this.Fan_GPU1.D4 = 100;
				bool flag11 = this.Fan_GPU1.D1 >= this.Fan_GPU1.D2;
				if (flag11)
				{
					this.Fan_GPU1.D2 = this.Fan_GPU1.D1++;
					bool flag12 = this.Fan_GPU1.D2 >= this.Fan_GPU1.D3;
					if (flag12)
					{
						this.Fan_GPU1.D3 = this.Fan_GPU1.D2++;
					}
					bool flag13 = this.Fan_GPU1.D3 >= this.Fan_GPU1.D4;
					if (flag13)
					{
						this.Fan_GPU1.D3 = this.Fan_GPU1.D4--;
					}
				}
				bool flag14 = this.Fan_GPU1.D2 >= this.Fan_GPU1.D3;
				if (flag14)
				{
					this.Fan_GPU1.D3 = this.Fan_GPU1.D2++;
					bool flag15 = this.Fan_GPU1.D3 >= this.Fan_GPU1.D4;
					if (flag15)
					{
						this.Fan_GPU1.D3 = this.Fan_GPU1.D4--;
					}
					bool flag16 = this.Fan_GPU1.D2 >= this.Fan_GPU1.D3;
					if (flag16)
					{
						this.Fan_GPU1.D2 = this.Fan_GPU1.D3--;
					}
				}
				bool flag17 = this.Fan_GPU1.D3 >= this.Fan_GPU1.D4;
				if (flag17)
				{
					Global.FanArgs fan_GPU = this.Fan_GPU1;
					fan_GPU.D3 -= 1;
					bool flag18 = this.Fan_GPU1.D2 >= this.Fan_GPU1.D3;
					if (flag18)
					{
						this.Fan_GPU1.D2 = this.Fan_GPU1.D3--;
					}
				}
				this.Fan_GPU1.D2_Default = this.Fan_GPU1.D2;
				this.Fan_GPU1.D3_Default = this.Fan_GPU1.D3;
				this.Fan_GPU2.T1 = array[32];
				this.Fan_GPU2.D1 = (byte)Math.Round((double)array[33] / 255.0 * 100.0, 0);
				this.Fan_GPU2.T2 = (this.Fan_GPU2.T2_Default = array[34]);
				this.Fan_GPU2.D2 = (this.Fan_GPU2.D2_Default = (byte)Math.Round((double)array[35] / 255.0 * 100.0, 0));
				this.Fan_GPU2.T3 = (this.Fan_GPU2.T3_Default = array[36]);
				this.Fan_GPU2.D3 = (this.Fan_GPU2.D3_Default = (byte)Math.Round((double)array[37] / 255.0 * 100.0, 0));
				this.Fan_GPU2.T4 = 100;
				this.Fan_GPU2.D4 = 100;
				bool flag19 = this.Fan_GPU2.D1 >= this.Fan_GPU2.D2;
				if (flag19)
				{
					this.Fan_GPU2.D2 = this.Fan_GPU2.D1++;
					bool flag20 = this.Fan_GPU2.D2 >= this.Fan_GPU2.D3;
					if (flag20)
					{
						this.Fan_GPU2.D3 = this.Fan_GPU2.D2++;
					}
					bool flag21 = this.Fan_GPU2.D3 >= this.Fan_GPU2.D4;
					if (flag21)
					{
						this.Fan_GPU2.D3 = this.Fan_GPU2.D4--;
					}
				}
				bool flag22 = this.Fan_GPU2.D2 >= this.Fan_GPU2.D3;
				if (flag22)
				{
					this.Fan_GPU2.D3 = this.Fan_GPU2.D2++;
					bool flag23 = this.Fan_GPU2.D3 >= this.Fan_GPU2.D4;
					if (flag23)
					{
						this.Fan_GPU2.D3 = this.Fan_GPU2.D4--;
					}
					bool flag24 = this.Fan_GPU2.D2 >= this.Fan_GPU2.D3;
					if (flag24)
					{
						this.Fan_GPU2.D2 = this.Fan_GPU2.D3--;
					}
				}
				bool flag25 = this.Fan_GPU2.D3 >= this.Fan_GPU2.D4;
				if (flag25)
				{
					Global.FanArgs fan_GPU2 = this.Fan_GPU2;
					fan_GPU2.D3 -= 1;
					bool flag26 = this.Fan_GPU2.D2 >= this.Fan_GPU2.D3;
					if (flag26)
					{
						this.Fan_GPU2.D2 = this.Fan_GPU2.D3--;
					}
				}
				this.Fan_GPU2.D2_Default = this.Fan_GPU2.D2;
				this.Fan_GPU2.D3_Default = this.Fan_GPU2.D3;
				DebugLog.WriteDbgLog2Outlog(string.Concat(new string[]
				{
					"Get WMI 13 \nFan_CPU.T1=",
					this.Fan_CPU.T1.ToString(),
					"\nFan_CPU.T2=",
					this.Fan_CPU.T2.ToString(),
					"\nFan_CPU.T3=",
					this.Fan_CPU.T3.ToString(),
					"\nFan_CPU.T4=",
					this.Fan_CPU.T4.ToString(),
					"\nFan_CPU.T2_Default=",
					this.Fan_CPU.T2_Default.ToString(),
					"\nFan_CPU.T3_Default=",
					this.Fan_CPU.T3_Default.ToString(),
					"\nFan_CPU.D1=",
					this.Fan_CPU.D1.ToString(),
					"\nFan_CPU.D2=",
					this.Fan_CPU.D2.ToString(),
					"\nFan_CPU.D3=",
					this.Fan_CPU.D3.ToString(),
					"\nFan_CPU.D4=",
					this.Fan_CPU.D4.ToString(),
					"\nFan_CPU.D2_Default=",
					this.Fan_CPU.D2_Default.ToString(),
					"\nFan_CPU.D3_Default=",
					this.Fan_CPU.D3_Default.ToString(),
					"\n-------------------------------------------------\nFan_GPU1.T1=",
					this.Fan_GPU1.T1.ToString(),
					"\nFan_GPU1.T2=",
					this.Fan_GPU1.T2.ToString(),
					"\nFan_GPU1.T3=",
					this.Fan_GPU1.T3.ToString(),
					"\nFan_GPU1.T4=",
					this.Fan_GPU1.T4.ToString(),
					"\nFan_GPU1.T2_Default=",
					this.Fan_GPU1.T2_Default.ToString(),
					"\nFan_GPU1.T3_Default=",
					this.Fan_GPU1.T3_Default.ToString(),
					"\nFan_GPU1.D1=",
					this.Fan_GPU1.D1.ToString(),
					"\nFan_GPU1.D2=",
					this.Fan_GPU1.D2.ToString(),
					"\nFan_GPU1.D3=",
					this.Fan_GPU1.D3.ToString(),
					"\nFan_GPU1.D4=",
					this.Fan_GPU1.D4.ToString(),
					"\nFan_GPU1.D2_Default=",
					this.Fan_GPU1.D2_Default.ToString(),
					"\n Fan_GPU1.D3_Default=",
					this.Fan_GPU1.D3_Default.ToString(),
					"\n-------------------------------------------------\nFan_GPU2.T1=",
					this.Fan_GPU2.T1.ToString(),
					"\nFan_GPU2.T2=",
					this.Fan_GPU2.T2.ToString(),
					"\nFan_GPU2.T3=",
					this.Fan_GPU2.T3.ToString(),
					"\nFan_GPU2.T4=",
					this.Fan_GPU2.T4.ToString(),
					"\nFan_GPU2.T2_Default=",
					this.Fan_GPU2.T2_Default.ToString(),
					"\nFan_GPU2.T3_Default=",
					this.Fan_GPU2.T3_Default.ToString(),
					"\nFan_GPU2.D1=",
					this.Fan_GPU2.D1.ToString(),
					"\nFan_GPU2.D2=",
					this.Fan_GPU2.D2.ToString(),
					"\nFan_GPU2.D3=",
					this.Fan_GPU2.D3.ToString(),
					"\nFan_GPU2.D4=",
					this.Fan_GPU2.D4.ToString(),
					"\nFan_GPU2.D2_Default=",
					this.Fan_GPU2.D2_Default.ToString(),
					"\n Fan_GPU2.D3_Default=",
					this.Fan_GPU2.D3_Default.ToString()
				}));
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00003044 File Offset: 0x00001244
		public void Write_WMI14()
		{
			this.Load_Rxx();
			byte[] array = new byte[256];
			array[2] = this.Fan_CPU.T2;
			array[3] = (byte)Math.Round((double)this.Fan_CPU.D2 / 100.0 * 255.0, 0);
			array[4] = this.Fan_CPU.T3;
			array[5] = (byte)Math.Round((double)this.Fan_CPU.D3 / 100.0 * 255.0, 0);
			array[6] = this.Fan_GPU1.T2;
			array[7] = (byte)Math.Round((double)this.Fan_GPU1.D2 / 100.0 * 255.0, 0);
			array[8] = this.Fan_GPU1.T3;
			array[9] = (byte)Math.Round((double)this.Fan_GPU1.D3 / 100.0 * 255.0, 0);
			array[10] = this.Fan_GPU2.T2;
			array[11] = (byte)Math.Round((double)this.Fan_GPU2.D2 / 100.0 * 255.0, 0);
			array[12] = this.Fan_GPU2.T3;
			array[13] = (byte)Math.Round((double)this.Fan_GPU2.D3 / 100.0 * 255.0, 0);
			array[14] = (byte)(this.Fan_CPU.R12 >> 8);
			array[15] = (byte)(this.Fan_CPU.R12 & 255);
			array[16] = (byte)(this.Fan_CPU.R23 >> 8);
			array[17] = (byte)(this.Fan_CPU.R23 & 255);
			array[18] = (byte)(this.Fan_CPU.R34 >> 8);
			array[19] = (byte)(this.Fan_CPU.R34 & 255);
			array[20] = (byte)(this.Fan_GPU1.R12 >> 8);
			array[21] = (byte)(this.Fan_GPU1.R12 & 255);
			array[22] = (byte)(this.Fan_GPU1.R23 >> 8);
			array[23] = (byte)(this.Fan_GPU1.R23 & 255);
			array[24] = (byte)(this.Fan_GPU1.R34 >> 8);
			array[25] = (byte)(this.Fan_GPU1.R34 & 255);
			array[26] = (byte)(this.Fan_GPU2.R12 >> 8);
			array[27] = (byte)(this.Fan_GPU2.R12 & 255);
			array[28] = (byte)(this.Fan_GPU2.R23 >> 8);
			array[29] = (byte)(this.Fan_GPU2.R23 & 255);
			array[30] = (byte)(this.Fan_GPU2.R34 >> 8);
			array[31] = (byte)(this.Fan_GPU2.R34 & 255);
			Global.insyde.SetWMIPackage(14, array);
			Global.fan.FanMode = 6;
			this.SetFanInfo();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00003344 File Offset: 0x00001544
		public void Read_FanInfo()
		{
			byte[] array = new byte[256];
			array = Global.insyde.GetAPPData(4, 0, 256);
			this.InitFanMode = (int)array[4];
			bool flag = this.InitFanMode == 5;
			if (flag)
			{
				Global.support_bit.MaxQ = true;
			}
			this.FanMode = (int)array[5];
			this.FanOffset = (int)array[7];
			byte[] array2 = new byte[256];
			array2 = Global.insyde.GetWMIPackage(13);
			this.FanCount = (int)array2[12];
			byte b = array2[43];
			bool flag2 = this.FanCount <= 1 || ((b >> 1) & 1) == 1;
			if (flag2)
			{
				this.SupportCustomFan = false;
			}
			bool supportCustomFan = this.SupportCustomFan;
			if (supportCustomFan)
			{
				this.Fan_CPU.D1 = array[16];
				this.Fan_CPU.D2 = array[17];
				this.Fan_CPU.D3 = array[18];
				this.Fan_CPU.D4 = 100;
				this.Fan_CPU.D2_Default = array[20];
				this.Fan_CPU.D3_Default = array[21];
				this.Fan_CPU.T1 = array[22];
				this.Fan_CPU.T2 = array[23];
				this.Fan_CPU.T3 = array[24];
				this.Fan_CPU.T4 = 100;
				this.Fan_CPU.T2_Default = array[26];
				this.Fan_CPU.T3_Default = array[27];
				this.Fan_CPU.R12 = ((int)array[29] << 8) + (int)array[28];
				this.Fan_CPU.R23 = ((int)array[31] << 8) + (int)array[30];
				this.Fan_CPU.R34 = ((int)array[33] << 8) + (int)array[32];
				this.Fan_GPU1.D1 = array[34];
				this.Fan_GPU1.D2 = array[35];
				this.Fan_GPU1.D3 = array[36];
				this.Fan_GPU1.D4 = 100;
				this.Fan_GPU1.D2_Default = array[38];
				this.Fan_GPU1.D3_Default = array[39];
				this.Fan_GPU1.T1 = array[40];
				this.Fan_GPU1.T2 = array[41];
				this.Fan_GPU1.T3 = array[42];
				this.Fan_GPU1.T4 = 100;
				this.Fan_GPU1.T2_Default = array[44];
				this.Fan_GPU1.T3_Default = array[45];
				this.Fan_GPU1.R12 = ((int)array[47] << 8) + (int)array[46];
				this.Fan_GPU1.R23 = ((int)array[49] << 8) + (int)array[48];
				this.Fan_GPU1.R34 = ((int)array[51] << 8) + (int)array[50];
				this.Fan_GPU2.D1 = array[52];
				this.Fan_GPU2.D2 = array[53];
				this.Fan_GPU2.D3 = array[54];
				this.Fan_GPU2.D4 = 100;
				this.Fan_GPU2.D2_Default = array[56];
				this.Fan_GPU2.D3_Default = array[57];
				this.Fan_GPU2.T1 = array[58];
				this.Fan_GPU2.T2 = array[59];
				this.Fan_GPU2.T3 = array[60];
				this.Fan_GPU2.T4 = 100;
				this.Fan_GPU2.T2_Default = array[62];
				this.Fan_GPU2.T3_Default = array[63];
				this.Fan_GPU2.R12 = ((int)array[65] << 8) + (int)array[64];
				this.Fan_GPU2.R23 = ((int)array[67] << 8) + (int)array[66];
				this.Fan_GPU2.R34 = ((int)array[69] << 8) + (int)array[68];
				DebugLog.WriteDbgLog2Outlog(string.Concat(new string[]
				{
					"Get Reg APP Data \nFan_CPU.T1=",
					this.Fan_CPU.T1.ToString(),
					"\nFan_CPU.T2=",
					this.Fan_CPU.T2.ToString(),
					"\nFan_CPU.T3=",
					this.Fan_CPU.T3.ToString(),
					"\nFan_CPU.T4=",
					this.Fan_CPU.T4.ToString(),
					"\nFan_CPU.T2_Default=",
					this.Fan_CPU.T2_Default.ToString(),
					"\nFan_CPU.T3_Default=",
					this.Fan_CPU.T3_Default.ToString(),
					"\nFan_CPU.D1=",
					this.Fan_CPU.D1.ToString(),
					"\nFan_CPU.D2=",
					this.Fan_CPU.D2.ToString(),
					"\nFan_CPU.D3=",
					this.Fan_CPU.D3.ToString(),
					"\nFan_CPU.D4=",
					this.Fan_CPU.D4.ToString(),
					"\nFan_CPU.D2_Default=",
					this.Fan_CPU.D2_Default.ToString(),
					"\nFan_CPU.D3_Default=",
					this.Fan_CPU.D3_Default.ToString(),
					"\n-------------------------------------------------\nFan_GPU1.T1=",
					this.Fan_GPU1.T1.ToString(),
					"\nFan_GPU1.T2=",
					this.Fan_GPU1.T2.ToString(),
					"\nFan_GPU1.T3=",
					this.Fan_GPU1.T3.ToString(),
					"\nFan_GPU1.T4=",
					this.Fan_GPU1.T4.ToString(),
					"\nFan_GPU1.T2_Default=",
					this.Fan_GPU1.T2_Default.ToString(),
					"\nFan_GPU1.T3_Default=",
					this.Fan_GPU1.T3_Default.ToString(),
					"\nFan_GPU1.D1=",
					this.Fan_GPU1.D1.ToString(),
					"\nFan_GPU1.D2=",
					this.Fan_GPU1.D2.ToString(),
					"\nFan_GPU1.D3=",
					this.Fan_GPU1.D3.ToString(),
					"\nFan_GPU1.D4=",
					this.Fan_GPU1.D4.ToString(),
					"\nFan_GPU1.D2_Default=",
					this.Fan_GPU1.D2_Default.ToString(),
					"\n Fan_GPU1.D3_Default=",
					this.Fan_GPU1.D3_Default.ToString(),
					"\n-------------------------------------------------\nFan_GPU2.T1=",
					this.Fan_GPU2.T1.ToString(),
					"\nFan_GPU2.T2=",
					this.Fan_GPU2.T2.ToString(),
					"\nFan_GPU2.T3=",
					this.Fan_GPU2.T3.ToString(),
					"\nFan_GPU2.T4=",
					this.Fan_GPU2.T4.ToString(),
					"\nFan_GPU2.T2_Default=",
					this.Fan_GPU2.T2_Default.ToString(),
					"\nFan_GPU2.T3_Default=",
					this.Fan_GPU2.T3_Default.ToString(),
					"\nFan_GPU2.D1=",
					this.Fan_GPU2.D1.ToString(),
					"\nFan_GPU2.D2=",
					this.Fan_GPU2.D2.ToString(),
					"\nFan_GPU2.D3=",
					this.Fan_GPU2.D3.ToString(),
					"\nFan_GPU2.D4=",
					this.Fan_GPU2.D4.ToString(),
					"\nFan_GPU2.D2_Default=",
					this.Fan_GPU2.D2_Default.ToString(),
					"\n Fan_GPU2.D3_Default=",
					this.Fan_GPU2.D3_Default.ToString()
				}));
			}
			bool antiDust_Fan = Global.support_bit.AntiDust_Fan;
			if (antiDust_Fan)
			{
				this.UpdateSystemTime();
				this.Read_AntiDust_Status();
				this.AntiDust_Scheduler_status = (int)array[81];
				bool flag3 = this.AntiDust_Scheduler_status == 0;
				if (flag3)
				{
					this.SetAntiDust_Scheduler(1, 127, 0);
				}
				else
				{
					this.AntiDust_S_Day = (int)(array[82] & 127);
					this.AntiDust_S_Resume = array[82] >> 7;
					this.AntiDust_S_Hour = (int)array[83];
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00003B5C File Offset: 0x00001D5C
		public void Load_Rxx()
		{
			this.Fan_CPU.R12 = (int)Math.Round((double)(this.Fan_CPU.D2 - this.Fan_CPU.D1) / (double)(this.Fan_CPU.T2 - this.Fan_CPU.T1) * 2.55 * 16.0, 0);
			this.Fan_CPU.R23 = (int)Math.Round((double)(this.Fan_CPU.D3 - this.Fan_CPU.D2) / (double)(this.Fan_CPU.T3 - this.Fan_CPU.T2) * 2.55 * 16.0, 0);
			this.Fan_CPU.R34 = (int)Math.Round((double)(this.Fan_CPU.D4 - this.Fan_CPU.D3) / (double)(this.Fan_CPU.T4 - this.Fan_CPU.T3) * 2.55 * 16.0, 0);
			this.Fan_GPU1.R12 = (int)Math.Round((double)(this.Fan_GPU1.D2 - this.Fan_GPU1.D1) / (double)(this.Fan_GPU1.T2 - this.Fan_GPU1.T1) * 2.55 * 16.0, 0);
			this.Fan_GPU1.R23 = (int)Math.Round((double)(this.Fan_GPU1.D3 - this.Fan_GPU1.D2) / (double)(this.Fan_GPU1.T3 - this.Fan_GPU1.T2) * 2.55 * 16.0, 0);
			this.Fan_GPU1.R34 = (int)Math.Round((double)(this.Fan_GPU1.D4 - this.Fan_GPU1.D3) / (double)(this.Fan_GPU1.T4 - this.Fan_GPU1.T3) * 2.55 * 16.0, 0);
			this.Fan_GPU2.R12 = (int)Math.Round((double)(this.Fan_GPU2.D2 - this.Fan_GPU2.D1) / (double)(this.Fan_GPU2.T2 - this.Fan_GPU2.T1) * 2.55 * 16.0, 0);
			this.Fan_GPU2.R23 = (int)Math.Round((double)(this.Fan_GPU2.D3 - this.Fan_GPU2.D2) / (double)(this.Fan_GPU2.T3 - this.Fan_GPU2.T2) * 2.55 * 16.0, 0);
			this.Fan_GPU2.R34 = (int)Math.Round((double)(this.Fan_GPU2.D4 - this.Fan_GPU2.D3) / (double)(this.Fan_GPU2.T4 - this.Fan_GPU2.T3) * 2.55 * 16.0, 0);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00003E7C File Offset: 0x0000207C
		public void SetFanInfo()
		{
			byte[] array = new byte[256];
			array[0] = (byte)Global.fan.FanAPPVersion_Major;
			array[1] = (byte)Global.fan.FanAPPVersion_Minor;
			array[2] = (byte)Global.fan.FanAPPVersion_Build;
			array[3] = (byte)Global.fan.FanAPPVersion_Revision;
			array[4] = (byte)this.InitFanMode;
			array[5] = (byte)this.FanMode;
			array[6] = (byte)this.FanCount;
			array[7] = (byte)this.FanOffset;
			array[16] = this.Fan_CPU.D1;
			array[17] = this.Fan_CPU.D2;
			array[18] = this.Fan_CPU.D3;
			array[19] = 100;
			array[20] = this.Fan_CPU.D2_Default;
			array[21] = this.Fan_CPU.D3_Default;
			array[22] = this.Fan_CPU.T1;
			array[23] = this.Fan_CPU.T2;
			array[24] = this.Fan_CPU.T3;
			array[25] = 100;
			array[26] = this.Fan_CPU.T2_Default;
			array[27] = this.Fan_CPU.T3_Default;
			array[28] = (byte)(this.Fan_CPU.R12 & 255);
			array[29] = (byte)(this.Fan_CPU.R12 >> 8);
			array[30] = (byte)(this.Fan_CPU.R23 & 255);
			array[31] = (byte)(this.Fan_CPU.R23 >> 8);
			array[32] = (byte)(this.Fan_CPU.R34 & 255);
			array[33] = (byte)(this.Fan_CPU.R34 >> 8);
			array[34] = this.Fan_GPU1.D1;
			array[35] = this.Fan_GPU1.D2;
			array[36] = this.Fan_GPU1.D3;
			array[37] = 100;
			array[38] = this.Fan_GPU1.D2_Default;
			array[39] = this.Fan_GPU1.D3_Default;
			array[40] = this.Fan_GPU1.T1;
			array[41] = this.Fan_GPU1.T2;
			array[42] = this.Fan_GPU1.T3;
			array[43] = 100;
			array[44] = this.Fan_GPU1.T2_Default;
			array[45] = this.Fan_GPU1.T3_Default;
			array[46] = (byte)(this.Fan_GPU1.R12 & 255);
			array[47] = (byte)(this.Fan_GPU1.R12 >> 8);
			array[48] = (byte)(this.Fan_GPU1.R23 & 255);
			array[49] = (byte)(this.Fan_GPU1.R23 >> 8);
			array[50] = (byte)(this.Fan_GPU1.R34 & 255);
			array[51] = (byte)(this.Fan_GPU1.R34 >> 8);
			array[52] = this.Fan_GPU2.D1;
			array[53] = this.Fan_GPU2.D2;
			array[54] = this.Fan_GPU2.D3;
			array[55] = 100;
			array[56] = this.Fan_GPU2.D2_Default;
			array[57] = this.Fan_GPU2.D3_Default;
			array[58] = this.Fan_GPU2.T1;
			array[59] = this.Fan_GPU2.T2;
			array[60] = this.Fan_GPU2.T3;
			array[61] = 100;
			array[62] = this.Fan_GPU2.T2_Default;
			array[63] = this.Fan_GPU2.T3_Default;
			array[64] = (byte)(this.Fan_GPU2.R12 & 255);
			array[65] = (byte)(this.Fan_GPU2.R12 >> 8);
			array[66] = (byte)(this.Fan_GPU2.R23 & 255);
			array[67] = (byte)(this.Fan_GPU2.R23 >> 8);
			array[68] = (byte)(this.Fan_GPU2.R34 & 255);
			array[69] = (byte)(this.Fan_GPU2.R34 >> 8);
			Global.insyde.SetAPPData(4, 0, 256, array);
			DebugLog.WriteDbgLog2Outlog(string.Concat(new string[]
			{
				"Set APP Data \nFan_CPU.T1=",
				this.Fan_CPU.T1.ToString(),
				"\nFan_CPU.T2=",
				this.Fan_CPU.T2.ToString(),
				"\nFan_CPU.T3=",
				this.Fan_CPU.T3.ToString(),
				"\nFan_CPU.T4=",
				this.Fan_CPU.T4.ToString(),
				"\nFan_CPU.T2_Default=",
				this.Fan_CPU.T2_Default.ToString(),
				"\nFan_CPU.T3_Default=",
				this.Fan_CPU.T3_Default.ToString(),
				"\nFan_CPU.D1=",
				this.Fan_CPU.D1.ToString(),
				"\nFan_CPU.D2=",
				this.Fan_CPU.D2.ToString(),
				"\nFan_CPU.D3=",
				this.Fan_CPU.D3.ToString(),
				"\nFan_CPU.D4=",
				this.Fan_CPU.D4.ToString(),
				"\nFan_CPU.D2_Default=",
				this.Fan_CPU.D2_Default.ToString(),
				"\nFan_CPU.D3_Default=",
				this.Fan_CPU.D3_Default.ToString(),
				"\n-------------------------------------------------\nFan_GPU1.T1=",
				this.Fan_GPU1.T1.ToString(),
				"\nFan_GPU1.T2=",
				this.Fan_GPU1.T2.ToString(),
				"\nFan_GPU1.T3=",
				this.Fan_GPU1.T3.ToString(),
				"\nFan_GPU1.T4=",
				this.Fan_GPU1.T4.ToString(),
				"\nFan_GPU1.T2_Default=",
				this.Fan_GPU1.T2_Default.ToString(),
				"\nFan_GPU1.T3_Default=",
				this.Fan_GPU1.T3_Default.ToString(),
				"\nFan_GPU1.D1=",
				this.Fan_GPU1.D1.ToString(),
				"\nFan_GPU1.D2=",
				this.Fan_GPU1.D2.ToString(),
				"\nFan_GPU1.D3=",
				this.Fan_GPU1.D3.ToString(),
				"\nFan_GPU1.D4=",
				this.Fan_GPU1.D4.ToString(),
				"\nFan_GPU1.D2_Default=",
				this.Fan_GPU1.D2_Default.ToString(),
				"\n Fan_GPU1.D3_Default=",
				this.Fan_GPU1.D3_Default.ToString(),
				"\n-------------------------------------------------\nFan_GPU2.T1=",
				this.Fan_GPU2.T1.ToString(),
				"\nFan_GPU2.T2=",
				this.Fan_GPU2.T2.ToString(),
				"\nFan_GPU2.T3=",
				this.Fan_GPU2.T3.ToString(),
				"\nFan_GPU2.T4=",
				this.Fan_GPU2.T4.ToString(),
				"\nFan_GPU2.T2_Default=",
				this.Fan_GPU2.T2_Default.ToString(),
				"\nFan_GPU2.T3_Default=",
				this.Fan_GPU2.T3_Default.ToString(),
				"\nFan_GPU2.D1=",
				this.Fan_GPU2.D1.ToString(),
				"\nFan_GPU2.D2=",
				this.Fan_GPU2.D2.ToString(),
				"\nFan_GPU2.D3=",
				this.Fan_GPU2.D3.ToString(),
				"\nFan_GPU2.D4=",
				this.Fan_GPU2.D4.ToString(),
				"\nFan_GPU2.D2_Default=",
				this.Fan_GPU2.D2_Default.ToString(),
				"\n Fan_GPU2.D3_Default=",
				this.Fan_GPU2.D3_Default.ToString()
			}));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00004674 File Offset: 0x00002874
		public void SetFanMode(byte mode)
		{
			Global.insyde.SetWMI(121, 1, (uint)mode);
			bool flag = mode == 7;
			if (flag)
			{
				byte[] data = new byte[] { 1 };
				Global.insyde.SetAPPData(4, 8, 1, data);
				DebugLog.SendOutlog_Event("113");
			}
			else
			{
				byte[] data2 = new byte[] { mode };
				Global.insyde.SetAPPData(4, 5, 1, data2);
				bool flag2 = this.CurrentSituationalMode == 2;
				if (flag2)
				{
					byte[] data3 = new byte[] { 0 };
					Global.insyde.SetAPPData(4, 8, 1, data3);
					DebugLog.SendOutlog_Event("113");
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00004718 File Offset: 0x00002918
		public void SetFanOffset(byte offset)
		{
			int data = (int)Math.Round(255.0 * ((double)offset / 100.0), 0);
			Global.insyde.SetWMI(121, 14, (uint)data);
			byte[] data2 = new byte[] { offset };
			Global.insyde.SetAPPData(4, 7, 1, data2);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000476E File Offset: 0x0000296E
		public void WMI13_LoadDefault()
		{
			Global.insyde.SetWMI(121, 34, 1U);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00004781 File Offset: 0x00002981
		public void RunAntiDustMode()
		{
			Global.insyde.SetWMI(121, 41, 1U);
			this.AntiDust_Now();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000479C File Offset: 0x0000299C
		public void AntiDust_Now()
		{
			this.AntiDust_RunStatus = 1;
			byte[] data = new byte[] { 1 };
			Global.insyde.SetAPPData(4, 80, 1, data);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000047CC File Offset: 0x000029CC
		public void AntiDust_Done()
		{
			this.AntiDust_RunStatus = 0;
			byte[] data = new byte[] { 0 };
			Global.insyde.SetAPPData(4, 80, 1, data);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000047FC File Offset: 0x000029FC
		public void SetAntiDust_Scheduler(int Resume, int Day, int Hour)
		{
			int data = Day + (Resume << 7) + (Hour << 8);
			Global.insyde.SetWMI(121, 40, (uint)data);
			this.AntiDust_S_Day = Day;
			this.AntiDust_S_Resume = Resume;
			this.AntiDust_S_Hour = Hour;
			byte[] data2 = new byte[]
			{
				1,
				(byte)(Day + (Resume << 7)),
				(byte)Hour,
				0,
				0
			};
			Global.insyde.SetAPPData(4, 81, 5, data2);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000486C File Offset: 0x00002A6C
		public void UpdateSystemTime()
		{
			int dayOfWeek = (int)DateTime.Now.DayOfWeek;
			int data = DateTime.Now.Second + (DateTime.Now.Minute << 6) + (DateTime.Now.Hour << 12) + (dayOfWeek << 17);
			Global.insyde.SetWMI(118, 1, (uint)data);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000048CC File Offset: 0x00002ACC
		public void Read_ThermalStatus()
		{
			byte[] array = new byte[256];
			byte[] array2 = new byte[256];
			array[0] = 1;
			array[1] = 9;
			array[2] = 0;
			array[3] = 0;
			array[4] = 0;
			array[5] = 0;
			array[6] = 192;
			int num = Global.insyde.SetWMIPackageEx(4, array, ref array2);
			int num2 = (int)array2[2];
			bool flag = ((num2 >> 4) & 1) == 1;
			if (flag)
			{
				this.LoadStorm = true;
			}
			else
			{
				this.LoadStorm = false;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00004944 File Offset: 0x00002B44
		public void Read_AntiDust_Status()
		{
			byte[] array = new byte[256];
			byte[] array2 = new byte[256];
			array[0] = 1;
			array[1] = 163;
			array[2] = 0;
			array[3] = 0;
			array[4] = 0;
			array[5] = 0;
			array[6] = 184;
			int num = Global.insyde.SetWMIPackageEx(4, array, ref array2);
			this.AntiDust_RunStatus = (int)array2[1];
			DebugLog.WriteDbgLog2Outlog(string.Concat(new string[]
			{
				"AntiDust_RunStatus=",
				this.AntiDust_RunStatus.ToString(),
				" byte0=",
				array2[0].ToString(),
				" byte2=",
				array2[2].ToString(),
				" byte3=",
				array2[3].ToString()
			}));
			byte[] data = new byte[1];
			array[0] = (byte)this.AntiDust_RunStatus;
			Global.insyde.SetAPPData(4, 80, 1, data);
		}

		// Token: 0x04000001 RID: 1
		public Global.FanArgs Fan_CPU = new Global.FanArgs();

		// Token: 0x04000002 RID: 2
		public Global.FanArgs Fan_GPU1 = new Global.FanArgs();

		// Token: 0x04000003 RID: 3
		public Global.FanArgs Fan_GPU2 = new Global.FanArgs();

		// Token: 0x04000004 RID: 4
		public int FanCount = 0;

		// Token: 0x04000005 RID: 5
		public int InitFanMode = 0;

		// Token: 0x04000006 RID: 6
		public int FanMode = 0;

		// Token: 0x04000007 RID: 7
		public int FanOffset = 0;

		// Token: 0x04000008 RID: 8
		public int TurboFanStatus = 0;

		// Token: 0x04000009 RID: 9
		public int FanAPPVersion_Major = 0;

		// Token: 0x0400000A RID: 10
		public int FanAPPVersion_Minor = 0;

		// Token: 0x0400000B RID: 11
		public int FanAPPVersion_Revision = 0;

		// Token: 0x0400000C RID: 12
		public int FanAPPVersion_Build = 0;

		// Token: 0x0400000D RID: 13
		public int AntiDust_RunStatus = 0;

		// Token: 0x0400000E RID: 14
		public int AntiDust_Scheduler_status = 0;

		// Token: 0x0400000F RID: 15
		public int AntiDust_S_Day = 255;

		// Token: 0x04000010 RID: 16
		public int AntiDust_S_Resume = 1;

		// Token: 0x04000011 RID: 17
		public int AntiDust_S_Hour = 0;

		// Token: 0x04000012 RID: 18
		public int CurrentSituationalMode = 3;

		// Token: 0x04000013 RID: 19
		public bool Support_MaxQ = false;

		// Token: 0x04000014 RID: 20
		public bool FanRusume = false;

		// Token: 0x04000015 RID: 21
		public bool SupportCustomFan = true;

		// Token: 0x04000016 RID: 22
		public bool LoadStorm = true;
	}
}
