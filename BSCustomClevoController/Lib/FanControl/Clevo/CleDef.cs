using System;

namespace Clevo
{
	// Token: 0x02000003 RID: 3
	internal class CleDef
	{
		// Token: 0x02000016 RID: 22
		public enum MessageType
		{
			// Token: 0x04000172 RID: 370
			System,
			// Token: 0x04000173 RID: 371
			Function,
			// Token: 0x04000174 RID: 372
			HotkeyEvent,
			// Token: 0x04000175 RID: 373
			WMI_Execture
		}

		// Token: 0x02000017 RID: 23
		public enum KbPart
		{
			// Token: 0x04000177 RID: 375
			all,
			// Token: 0x04000178 RID: 376
			left,
			// Token: 0x04000179 RID: 377
			mid,
			// Token: 0x0400017A RID: 378
			right,
			// Token: 0x0400017B RID: 379
			touchpad,
			// Token: 0x0400017C RID: 380
			none,
			// Token: 0x0400017D RID: 381
			volume,
			// Token: 0x0400017E RID: 382
			logo
		}

		// Token: 0x02000018 RID: 24
		public enum KB_Mode
		{
			// Token: 0x04000180 RID: 384
			random,
			// Token: 0x04000181 RID: 385
			custom,
			// Token: 0x04000182 RID: 386
			breath,
			// Token: 0x04000183 RID: 387
			cycle,
			// Token: 0x04000184 RID: 388
			wave,
			// Token: 0x04000185 RID: 389
			dance,
			// Token: 0x04000186 RID: 390
			tempo,
			// Token: 0x04000187 RID: 391
			flash,
			// Token: 0x04000188 RID: 392
			all
		}

		// Token: 0x02000019 RID: 25
		public enum LEDKB_Command
		{
			// Token: 0x0400018A RID: 394
			SendDataArray = 22,
			// Token: 0x0400018B RID: 395
			SetKBLEDSleepTimer,
			// Token: 0x0400018C RID: 396
			SetLEDKBOnOff,
			// Token: 0x0400018D RID: 397
			SetKbBrightness,
			// Token: 0x0400018E RID: 398
			SetKB_Mode,
			// Token: 0x0400018F RID: 399
			WriteToReg,
			// Token: 0x04000190 RID: 400
			LoadDefault
		}

		// Token: 0x0200001A RID: 26
		public enum Kb_Cmd_Index
		{
			// Token: 0x04000192 RID: 402
			L_R = 1,
			// Token: 0x04000193 RID: 403
			L_G,
			// Token: 0x04000194 RID: 404
			L_B,
			// Token: 0x04000195 RID: 405
			M_R,
			// Token: 0x04000196 RID: 406
			M_G,
			// Token: 0x04000197 RID: 407
			M_B,
			// Token: 0x04000198 RID: 408
			R_R,
			// Token: 0x04000199 RID: 409
			R_G,
			// Token: 0x0400019A RID: 410
			R_B,
			// Token: 0x0400019B RID: 411
			TP_R,
			// Token: 0x0400019C RID: 412
			TP_G,
			// Token: 0x0400019D RID: 413
			TP_B,
			// Token: 0x0400019E RID: 414
			Logo_R,
			// Token: 0x0400019F RID: 415
			Logo_G,
			// Token: 0x040001A0 RID: 416
			Logo_B,
			// Token: 0x040001A1 RID: 417
			LightBar_R,
			// Token: 0x040001A2 RID: 418
			LightBar_G,
			// Token: 0x040001A3 RID: 419
			LightBar_B,
			// Token: 0x040001A4 RID: 420
			Left,
			// Token: 0x040001A5 RID: 421
			Mid,
			// Token: 0x040001A6 RID: 422
			Right,
			// Token: 0x040001A7 RID: 423
			Tp,
			// Token: 0x040001A8 RID: 424
			Logo,
			// Token: 0x040001A9 RID: 425
			AutoLightBar,
			// Token: 0x040001AA RID: 426
			Status,
			// Token: 0x040001AB RID: 427
			Brightness,
			// Token: 0x040001AC RID: 428
			Mode,
			// Token: 0x040001AD RID: 429
			SleepLEDTimer,
			// Token: 0x040001AE RID: 430
			All_R = 30,
			// Token: 0x040001AF RID: 431
			All_G,
			// Token: 0x040001B0 RID: 432
			All_B
		}

		// Token: 0x0200001B RID: 27
		public enum Kb_Sleep
		{
			// Token: 0x040001B2 RID: 434
			OFF,
			// Token: 0x040001B3 RID: 435
			sec_15,
			// Token: 0x040001B4 RID: 436
			sec_30,
			// Token: 0x040001B5 RID: 437
			min_1,
			// Token: 0x040001B6 RID: 438
			min_5,
			// Token: 0x040001B7 RID: 439
			custom = 255
		}

		// Token: 0x0200001C RID: 28
		public enum Kb_Type
		{
			// Token: 0x040001B9 RID: 441
			NoLed,
			// Token: 0x040001BA RID: 442
			White,
			// Token: 0x040001BB RID: 443
			RGB,
			// Token: 0x040001BC RID: 444
			Perkey,
			// Token: 0x040001BD RID: 445
			RGBFakeWhile = 6,
			// Token: 0x040001BE RID: 446
			RGBFakeWhileCustom = 22
		}

		// Token: 0x0200001D RID: 29
		public enum Brightness
		{
			// Token: 0x040001C0 RID: 448
			Off,
			// Token: 0x040001C1 RID: 449
			Lv1,
			// Token: 0x040001C2 RID: 450
			Lv2,
			// Token: 0x040001C3 RID: 451
			Lv3,
			// Token: 0x040001C4 RID: 452
			Lv4,
			// Token: 0x040001C5 RID: 453
			Lv5
		}

		// Token: 0x0200001E RID: 30
		public enum HkeyTrayEvent
		{
			// Token: 0x040001C7 RID: 455
			HKTrayOnInit = 256,
			// Token: 0x040001C8 RID: 456
			HKTrayReady,
			// Token: 0x040001C9 RID: 457
			SituationMode,
			// Token: 0x040001CA RID: 458
			TouchPad,
			// Token: 0x040001CB RID: 459
			CCD,
			// Token: 0x040001CC RID: 460
			Airplane,
			// Token: 0x040001CD RID: 461
			FlexiKey,
			// Token: 0x040001CE RID: 462
			CapsLk,
			// Token: 0x040001CF RID: 463
			ScrLk,
			// Token: 0x040001D0 RID: 464
			NumLk,
			// Token: 0x040001D1 RID: 465
			Fan,
			// Token: 0x040001D2 RID: 466
			Changed,
			// Token: 0x040001D3 RID: 467
			BatteryFull,
			// Token: 0x040001D4 RID: 468
			Docking,
			// Token: 0x040001D5 RID: 469
			ACOut,
			// Token: 0x040001D6 RID: 470
			FN_3,
			// Token: 0x040001D7 RID: 471
			BatteryFullDischarge = 273,
			// Token: 0x040001D8 RID: 472
			KeyboardOnInit = 512,
			// Token: 0x040001D9 RID: 473
			KeyboardReady,
			// Token: 0x040001DA RID: 474
			KeyboardStatus,
			// Token: 0x040001DB RID: 475
			KbBrightness,
			// Token: 0x040001DC RID: 476
			WhiteKbBrightness,
			// Token: 0x040001DD RID: 477
			KbStatus,
			// Token: 0x040001DE RID: 478
			PerKbBrightness,
			// Token: 0x040001DF RID: 479
			FA_Close = 576,
			// Token: 0x040001E0 RID: 480
			FA_Start,
			// Token: 0x040001E1 RID: 481
			FA_Connect,
			// Token: 0x040001E2 RID: 482
			FA_Disconnect,
			// Token: 0x040001E3 RID: 483
			FA_UpdateData
		}

		// Token: 0x0200001F RID: 31
		public enum CCToHKTray
		{
			// Token: 0x040001E5 RID: 485
			CameraStatus = 257,
			// Token: 0x040001E6 RID: 486
			TouchPadStatus,
			// Token: 0x040001E7 RID: 487
			MSHybridStatus,
			// Token: 0x040001E8 RID: 488
			LcdOff,
			// Token: 0x040001E9 RID: 489
			FanMode,
			// Token: 0x040001EA RID: 490
			WinKey,
			// Token: 0x040001EB RID: 491
			Situation,
			// Token: 0x040001EC RID: 492
			AirplaneMode,
			// Token: 0x040001ED RID: 493
			CapsLck,
			// Token: 0x040001EE RID: 494
			ScrLck,
			// Token: 0x040001EF RID: 495
			NumLck,
			// Token: 0x040001F0 RID: 496
			UpdateLiveInfoOn,
			// Token: 0x040001F1 RID: 497
			UpdateLiveInfoOff,
			// Token: 0x040001F2 RID: 498
			HeadPhone,
			// Token: 0x040001F3 RID: 499
			CcReady,
			// Token: 0x040001F4 RID: 500
			CcClose,
			// Token: 0x040001F5 RID: 501
			MemoryVoltage,
			// Token: 0x040001F6 RID: 502
			BatteryRefresh = 285,
			// Token: 0x040001F7 RID: 503
			HeadPhonePollingOn = 288,
			// Token: 0x040001F8 RID: 504
			HeadPhonePollingOff,
			// Token: 0x040001F9 RID: 505
			KbBrightness = 526,
			// Token: 0x040001FA RID: 506
			KbSpeed,
			// Token: 0x040001FB RID: 507
			SleepLEDTimer,
			// Token: 0x040001FC RID: 508
			KbPerkeyClearAll,
			// Token: 0x040001FD RID: 509
			KbPerkeyBrightness,
			// Token: 0x040001FE RID: 510
			KbPerkeyStatic,
			// Token: 0x040001FF RID: 511
			KbPerkeyBreath,
			// Token: 0x04000200 RID: 512
			KbPerkeyBreathRandom,
			// Token: 0x04000201 RID: 513
			KbPerkeyBlink,
			// Token: 0x04000202 RID: 514
			KbPerkeyBlinkRandom,
			// Token: 0x04000203 RID: 515
			KbPerkeyWave,
			// Token: 0x04000204 RID: 516
			KbPerkeyRandom,
			// Token: 0x04000205 RID: 517
			KbPerkeyScan,
			// Token: 0x04000206 RID: 518
			KbPerkeySnake,
			// Token: 0x04000207 RID: 519
			KbPerkeyRipple,
			// Token: 0x04000208 RID: 520
			KbPerkeyRippleRandom,
			// Token: 0x04000209 RID: 521
			KbSendDataArray,
			// Token: 0x0400020A RID: 522
			KbRgbMode,
			// Token: 0x0400020B RID: 523
			KbRgbLoadDefault,
			// Token: 0x0400020C RID: 524
			KbPerkeyReactive,
			// Token: 0x0400020D RID: 525
			KbPerkeyReactiveRandom,
			// Token: 0x0400020E RID: 526
			KbSelectBootEffect,
			// Token: 0x0400020F RID: 527
			FAClose = 576,
			// Token: 0x04000210 RID: 528
			FAStart,
			// Token: 0x04000211 RID: 529
			FAConnect,
			// Token: 0x04000212 RID: 530
			FADisconnect,
			// Token: 0x04000213 RID: 531
			FAUpdateData
		}

		// Token: 0x02000020 RID: 32
		public enum Customer
		{
			// Token: 0x04000215 RID: 533
			Standard,
			// Token: 0x04000216 RID: 534
			Tronic5,
			// Token: 0x04000217 RID: 535
			MCJ,
			// Token: 0x04000218 RID: 536
			Seager,
			// Token: 0x04000219 RID: 537
			Gigabyte,
			// Token: 0x0400021A RID: 538
			EPSON,
			// Token: 0x0400021B RID: 539
			Santech
		}
	}
}
