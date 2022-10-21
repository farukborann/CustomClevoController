using System;
using System.Windows.Media;

namespace FanSpeedSetting
{
	// Token: 0x0200000F RID: 15
	public class Global
	{
		// Token: 0x040000EE RID: 238
		public static RWReg RW_REG;

		// Token: 0x040000EF RID: 239
		public static Support_BIT support_bit;

		// Token: 0x040000F0 RID: 240
		public static Clevo_MSG clevo_msg;

		// Token: 0x040000F1 RID: 241
		public static InsydeDriver insyde;

		// Token: 0x040000F2 RID: 242
		public static FAN fan;

		// Token: 0x040000F3 RID: 243
		public static byte[] kled_kb_array = new byte[255];

		// Token: 0x040000F4 RID: 244
		public static Global.KB kb_color = new Global.KB();

		// Token: 0x040000F5 RID: 245
		public static language MultiLang;

		// Token: 0x040000F6 RID: 246
		public static RWIni RW_ini;

		// Token: 0x040000F7 RID: 247
		public static int Flexikey_LED_Language;

		// Token: 0x02000023 RID: 35
		public class KB
		{
			// Token: 0x04000221 RID: 545
			public Color all = default(Color);

			// Token: 0x04000222 RID: 546
			public Color left = default(Color);

			// Token: 0x04000223 RID: 547
			public Color mid = default(Color);

			// Token: 0x04000224 RID: 548
			public Color right = default(Color);

			// Token: 0x04000225 RID: 549
			public Color TP = default(Color);

			// Token: 0x04000226 RID: 550
			public Color LOGO = default(Color);

			// Token: 0x04000227 RID: 551
			public Color LightBar = default(Color);

			// Token: 0x04000228 RID: 552
			public int _iKbBrightness = 0;

			// Token: 0x04000229 RID: 553
			public int _iKbStatus = 0;

			// Token: 0x0400022A RID: 554
			public int _iKbMode = 0;

			// Token: 0x0400022B RID: 555
			public int _iKbLeft = 0;

			// Token: 0x0400022C RID: 556
			public int _iKbMid = 0;

			// Token: 0x0400022D RID: 557
			public int _iKbRight = 0;

			// Token: 0x0400022E RID: 558
			public int _iKbTp = 0;

			// Token: 0x0400022F RID: 559
			public int _iVolume = 0;

			// Token: 0x04000230 RID: 560
			public int _iKbLogo = 0;

			// Token: 0x04000231 RID: 561
			public int _iKbAutoLightBar = 0;

			// Token: 0x04000232 RID: 562
			public int _iKbSleepLEDTimer = 0;
		}

		// Token: 0x02000024 RID: 36
		public class FanArgs
		{
			// Token: 0x04000233 RID: 563
			public byte T1;

			// Token: 0x04000234 RID: 564
			public byte T2;

			// Token: 0x04000235 RID: 565
			public byte T3;

			// Token: 0x04000236 RID: 566
			public byte T4;

			// Token: 0x04000237 RID: 567
			public byte T2_Default;

			// Token: 0x04000238 RID: 568
			public byte T3_Default;

			// Token: 0x04000239 RID: 569
			public byte D1;

			// Token: 0x0400023A RID: 570
			public byte D2;

			// Token: 0x0400023B RID: 571
			public byte D3;

			// Token: 0x0400023C RID: 572
			public byte D4;

			// Token: 0x0400023D RID: 573
			public byte D2_Default;

			// Token: 0x0400023E RID: 574
			public byte D3_Default;

			// Token: 0x0400023F RID: 575
			public int R12;

			// Token: 0x04000240 RID: 576
			public int R23;

			// Token: 0x04000241 RID: 577
			public int R34;

			// Token: 0x04000242 RID: 578
			public int rpm;

			// Token: 0x04000243 RID: 579
			public int local;

			// Token: 0x04000244 RID: 580
			public int remote;

			// Token: 0x04000245 RID: 581
			public byte duty;
		}
	}
}
