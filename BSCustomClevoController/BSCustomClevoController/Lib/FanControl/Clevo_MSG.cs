using System;
using System.Runtime.InteropServices;

namespace FanSpeedSetting
{
	// Token: 0x02000008 RID: 8
	public class Clevo_MSG
	{
		// Token: 0x06000035 RID: 53
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int RegisterWindowMessage(string lpString);

		// Token: 0x06000036 RID: 54
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool SendNotifyMessage(int hWnd, int Msg, int wParam, int lParam);

		// Token: 0x06000037 RID: 55
		[DllImport("User32.dll")]
		private static extern int SendMessage(int hWnd, int Msg, uint wParam, uint lParam);

		// Token: 0x06000038 RID: 56
		[DllImport("User32.dll", EntryPoint = "SendMessage")]
		private static extern int SendMessageData(int hWnd, int Msg, int wParam, ref Clevo_MSG.COPYDATASTRUCT lParam);

		// Token: 0x06000039 RID: 57
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		// Token: 0x0600003A RID: 58
		[DllImport("DataAddress.dll")]
		private static extern uint DataAddress(IntPtr data1);

		// Token: 0x0600003B RID: 59 RVA: 0x0000547D File Offset: 0x0000367D
		public Clevo_MSG()
		{
			this.Init();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000548E File Offset: 0x0000368E
		private void Init()
		{
			this.msgName = Clevo_MSG.RegisterWindowMessage("ClevoMsg_ControlCenter");
			this.msgName1 = Clevo_MSG.RegisterWindowMessage("ClevoMsg_ControlCenter_WMI");
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000054B1 File Offset: 0x000036B1
		public void SendMsg(int CMD, int DATA)
		{
			Clevo_MSG.SendNotifyMessage(65535, this.msgName, CMD, DATA);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000054C8 File Offset: 0x000036C8
		public void SendMsg_WMI(int CMD, int DATA)
		{
			Console.WriteLine("SendMsg_WMI CMD" + CMD.ToString() + " Data=" + DATA.ToString());
			Clevo_MSG.COPYDATASTRUCT copydatastruct = default(Clevo_MSG.COPYDATASTRUCT);
			int hWnd = (int)Clevo_MSG.FindWindow(null, "HkeyTray");
			byte[] array = new byte[]
			{
				Convert.ToByte(CMD),
				Convert.ToByte(DATA & 255),
				(byte)((DATA >> 8) & 255),
				(byte)((DATA >> 16) & 255),
				(byte)((DATA >> 24) & 255)
			};
			copydatastruct.cbData = array.GetUpperBound(0) + 1;
			copydatastruct.lpData = Clevo_MSG.DataAddress(Marshal.UnsafeAddrOfPinnedArrayElement<byte>(array, 0));
			Clevo_MSG.SendMessageData(hWnd, 74, 2, ref copydatastruct);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000558C File Offset: 0x0000378C
		public void GetMsg_WMI(int CMD)
		{
			Clevo_MSG.COPYDATASTRUCT copydatastruct = default(Clevo_MSG.COPYDATASTRUCT);
			int hWnd = (int)Clevo_MSG.FindWindow(null, "HkeyTray");
			byte[] array = new byte[] { Convert.ToByte(CMD) };
			copydatastruct.cbData = array.GetUpperBound(0) + 1;
			copydatastruct.lpData = Clevo_MSG.DataAddress(Marshal.UnsafeAddrOfPinnedArrayElement<byte>(array, 0));
			Clevo_MSG.SendMessageData(hWnd, 74, 2, ref copydatastruct);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000055F4 File Offset: 0x000037F4
		public void SendArrayToHkeyTray()
		{
			Clevo_MSG.COPYDATASTRUCT copydatastruct = default(Clevo_MSG.COPYDATASTRUCT);
			copydatastruct.cbData = Global.kled_kb_array.GetUpperBound(0) + 1;
			copydatastruct.lpData = Clevo_MSG.DataAddress(Marshal.UnsafeAddrOfPinnedArrayElement<byte>(Global.kled_kb_array, 0));
			int hWnd = (int)Clevo_MSG.FindWindow(null, "HkeyTray");
			Clevo_MSG.SendMessageData(hWnd, 74, 3, ref copydatastruct);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00005654 File Offset: 0x00003854
		public void SendToHKTray(int cmd)
		{
			byte dwData = (byte)((cmd >> 8) & 255);
			Clevo_MSG.COPYDATASTRUCT copydatastruct = default(Clevo_MSG.COPYDATASTRUCT);
			int hWnd = (int)Clevo_MSG.FindWindow(null, "HkeyTray");
			byte[] array = new byte[5];
			array[0] = (byte)(cmd & 255);
			copydatastruct.dwData = (int)dwData;
			copydatastruct.cbData = array.GetUpperBound(0) + 1;
			copydatastruct.lpData = Clevo_MSG.DataAddress(Marshal.UnsafeAddrOfPinnedArrayElement<byte>(array, 0));
			Clevo_MSG.SendMessageData(hWnd, 74, 2, ref copydatastruct);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000056D0 File Offset: 0x000038D0
		public void SendToHKTray(int cmd, byte arr1)
		{
			byte dwData = (byte)((cmd >> 8) & 255);
			Clevo_MSG.COPYDATASTRUCT copydatastruct = default(Clevo_MSG.COPYDATASTRUCT);
			int hWnd = (int)Clevo_MSG.FindWindow(null, "HkeyTray");
			byte[] array = new byte[5];
			array[0] = (byte)(cmd & 255);
			array[1] = arr1;
			copydatastruct.dwData = (int)dwData;
			copydatastruct.cbData = array.GetUpperBound(0) + 1;
			copydatastruct.lpData = Clevo_MSG.DataAddress(Marshal.UnsafeAddrOfPinnedArrayElement<byte>(array, 0));
			Clevo_MSG.SendMessageData(hWnd, 74, 2, ref copydatastruct);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00005750 File Offset: 0x00003950
		public void SendToHKTray(int cmd, byte arr1, byte arr2)
		{
			byte dwData = (byte)((cmd >> 8) & 255);
			Clevo_MSG.COPYDATASTRUCT copydatastruct = default(Clevo_MSG.COPYDATASTRUCT);
			int hWnd = (int)Clevo_MSG.FindWindow(null, "HkeyTray");
			byte[] array = new byte[5];
			array[0] = (byte)(cmd & 255);
			array[1] = arr1;
			array[2] = arr2;
			copydatastruct.dwData = (int)dwData;
			copydatastruct.cbData = array.GetUpperBound(0) + 1;
			copydatastruct.lpData = Clevo_MSG.DataAddress(Marshal.UnsafeAddrOfPinnedArrayElement<byte>(array, 0));
			Clevo_MSG.SendMessageData(hWnd, 74, 2, ref copydatastruct);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000057D4 File Offset: 0x000039D4
		public void SendToHKTray(int cmd, byte arr1, byte arr2, byte arr3)
		{
			byte dwData = (byte)((cmd >> 8) & 255);
			Clevo_MSG.COPYDATASTRUCT copydatastruct = default(Clevo_MSG.COPYDATASTRUCT);
			int hWnd = (int)Clevo_MSG.FindWindow(null, "HkeyTray");
			byte[] array = new byte[5];
			array[0] = (byte)(cmd & 255);
			array[1] = arr1;
			array[2] = arr2;
			array[3] = arr3;
			copydatastruct.dwData = (int)dwData;
			copydatastruct.cbData = array.GetUpperBound(0) + 1;
			copydatastruct.lpData = Clevo_MSG.DataAddress(Marshal.UnsafeAddrOfPinnedArrayElement<byte>(array, 0));
			Clevo_MSG.SendMessageData(hWnd, 74, 2, ref copydatastruct);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000585C File Offset: 0x00003A5C
		public void SendToHKTray(int cmd, byte arr1, byte arr2, byte arr3, byte arr4)
		{
			byte dwData = (byte)((cmd >> 8) & 255);
			Clevo_MSG.COPYDATASTRUCT copydatastruct = default(Clevo_MSG.COPYDATASTRUCT);
			int hWnd = (int)Clevo_MSG.FindWindow(null, "HkeyTray");
			byte[] array = new byte[]
			{
				(byte)(cmd & 255),
				arr1,
				arr2,
				arr3,
				arr4
			};
			copydatastruct.dwData = (int)dwData;
			copydatastruct.cbData = array.GetUpperBound(0) + 1;
			copydatastruct.lpData = Clevo_MSG.DataAddress(Marshal.UnsafeAddrOfPinnedArrayElement<byte>(array, 0));
			Clevo_MSG.SendMessageData(hWnd, 74, 2, ref copydatastruct);
		}

		// Token: 0x0400001C RID: 28
		private const int HWND_BROADCAST = 65535;

		// Token: 0x0400001D RID: 29
		private int msgName;

		// Token: 0x0400001E RID: 30
		private int msgName1;

		// Token: 0x02000021 RID: 33
		public struct COPYDATASTRUCT
		{
			// Token: 0x0400021C RID: 540
			public int dwData;

			// Token: 0x0400021D RID: 541
			public int cbData;

			// Token: 0x0400021E RID: 542
			public uint lpData;
		}
	}
}
