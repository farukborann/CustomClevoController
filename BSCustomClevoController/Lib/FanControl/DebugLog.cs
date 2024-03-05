using System;
using System.Diagnostics;

namespace FanSpeedSetting
{
	// Token: 0x02000004 RID: 4
	internal class DebugLog
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020F4 File Offset: 0x000002F4
		public static void WriteDbgLog2Outlog(string log)
		{
			try
			{
				EventLog eventLog = new EventLog();
				eventLog.Source = "PowerBIOSServer_Out";
				eventLog.Log = "OutLog";
				log = "DebugFan:" + log;
				eventLog.WriteEntry(log);
			}
			catch
			{
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002150 File Offset: 0x00000350
		public static void SendOutlog_Event(string log)
		{
			try
			{
				EventLog eventLog = new EventLog();
				eventLog.Source = "PowerBIOSServer_Out";
				eventLog.Log = "OutLog";
				log = "clevofan^" + log;
				eventLog.WriteEntry(log);
			}
			catch
			{
			}
		}
	}
}
