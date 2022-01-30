using System;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static BSCustomClevoController.Utility.Structs;

namespace BSCustomClevoController.Utility
{
    class KeyboardBackLight
    {
        [DllImport("InsydeDCHU.dll")]
        static extern int SetDCHU_Data(int command, byte[] buffer, int length);

        [DllImport("InsydeDCHU.dll")]
        static extern int WriteAppSettings(int page, int offset, int length, ref byte buffer);

        [DllImport("InsydeDCHU.dll")]
        public static extern int ReadAppSettings(int page, int offset, int length, ref byte buffer);

        //Set Effects

        public static KeyboardEffect Random = new KeyboardEffect() { Name = "Random", UniqId = 112, Id = 0 }; /// Goes every ~0.5 sec to a different random colour (with the current brightness)
        public static KeyboardEffect Breath = new KeyboardEffect() { Name = "Nefes", UniqId = 16, Id = 2 }; /// Breaths with the current colour (cycles from 0 to brightness, around 2 sec)
        public static KeyboardEffect Cycle = new KeyboardEffect() { Name = "Döngü", UniqId = 51, Id = 3 }; /// Cycles through primary and secondary colours (going from 0 to brightness in between, around 2sec). Blue, Green, Red, Blue?, Cyan, Yellow, Pink  
        public static KeyboardEffect Wave = new KeyboardEffect() { Name = "Dalga", UniqId = 176, Id = 4 }; /// Slowely (around every 10 sec) shows a random colour slowly increasing and then decreasing the brightness (goes from 0 to brightness)
        public static KeyboardEffect Dance = new KeyboardEffect() { Name = "Dans", UniqId = 128, Id = 5 }; /// Flashes every 0.5 sec with a different random colour (goes from 0 to brightness)
        public static KeyboardEffect Tempo = new KeyboardEffect() { Name = "Tempo", UniqId = 144, Id = 6 }; /// Flashes a colour, then goes off shortly, then flashes the same colour, then goes off longer ~1sec, then starts over with a different colour (goes from 0 to brightness)
        public static KeyboardEffect Flash = new KeyboardEffect() { Name = "Flash", UniqId = 160, Id = 7 }; /// Flashes every 1 sec with a different random colour (goes from 0 to brightness)
        public static KeyboardEffect SoftwareBreath = new KeyboardEffect() { Name = "Nefes", UniqId = 0, Id = 8, Func = KeyboardSofwareEffects.BreathEffect, Milisecons = 0 }; // , Milisecons=40
        public static KeyboardEffect SoftwareCycle = new KeyboardEffect() { Name = "Döngü", UniqId = 0, Id = 9, Func = KeyboardSofwareEffects.CycleEffect, Milisecons = 100 };

        public static List<KeyboardEffect> KeyboardEffects = new List<KeyboardEffect>() { Random, Breath, Cycle, Wave, Dance, Tempo, Flash, SoftwareBreath, SoftwareCycle };

        public static KeyboardStatus GetStatus()
        {
            byte brightness = 0;
            ReadAppSettings(2, 35, 1, ref brightness);
            byte[] colour = new byte[3];
            ReadAppSettings(2, 81, 3, ref colour[0]);
            byte mode = 0;
            ReadAppSettings(2, 32, 1, ref mode);
            byte status = 0;
            ReadAppSettings(2, 84, 1, ref status);
            byte booteffect = 0;
            ReadAppSettings(2, 7, 1, ref booteffect);
            byte[] sleep = new byte[3];
            ReadAppSettings(2, 37, 3, ref sleep[0]);
            int sleepsec = sleep[0] * 3600 + sleep[1] * 60 + sleep[2];
            byte sleepstatus = 0;
            ReadAppSettings(2, 36, 1, ref sleepstatus);

            return new KeyboardStatus() { BackLight = Color.FromArgb(brightness, colour[0], colour[1], colour[2]),
                                          Effect = KeyboardEffect.LookUpEffect(mode), LightState = Convert.ToBoolean(status), BootEffect = Convert.ToBoolean(booteffect),
                                          SleepSecond = sleepsec, SleepState = Convert.ToBoolean(sleepstatus)
            };
        }

        /// <summary>
        /// Sets the brightness of the LEDs to the given value
        /// </summary>
        /// <param name="value">The value [0-255]</param>
        public static void SetBrightness(byte value)
        {
            SetDCHU_Data(103, new byte[4] { value, 0, 0, 244 }, 4);
            //TODO: express the setting as a value in [0-4] as they do in their code
            WriteAppSettings(2, 35, 1, ref new byte[1] { value }[0]);
        }

        /// <summary>
        /// Sets the color of the LEDs to the given colour
        /// </summary>
        /// <param name="R">The red component [0-255]</param>
        /// <param name="G">The green component [0-255]</param>
        /// <param name="B">The blue component [0-255]</param>
        public static void SetColour(byte R, byte G, byte B)
        {
            SetDCHU_Data(103, new byte[4] { G, R, B, 240 }, 4);
            WriteAppSettings(2, 81, 3, ref new byte[3] { R, G, B }[0]);
            WriteAppSettings(2, 32, 1, ref new byte[1] { 8 }[0]); //All colour mode
        }

        public static void SetColour(Color color)
        {
            SetDCHU_Data(103, new byte[4] { color.G, color.R, color.B, 240 }, 4);
            WriteAppSettings(2, 81, 3, ref new byte[3] { color.R, color.G, color.B }[0]);
            WriteAppSettings(2, 32, 1, ref new byte[1] { 8 }[0]); //All colour mode
        }

        /// <summary>
        /// Sets the LEDs to the given Mode
        /// </summary>
        /// <param name="mode">The mode</param>
        public static void SetMode(KeyboardEffect mode)
        {
            SetDCHU_Data(103, new byte[4] { 0, 0, 0, mode.UniqId }, 4);
            WriteAppSettings(2, 32, 1, ref new byte[1] { (byte)mode.Id }[0]);
        }

        /// <summary>
        /// Turns the LEDs off
        /// </summary>
        public static void TurnOff()
        {
            SetDCHU_Data(103, new byte[4] { 0, 0, 0, 224 }, 4);
            WriteAppSettings(2, 84, 1, ref new byte[1] { 0 }[0]);
        }

        /// <summary>
        /// Turn the LEDs on
        /// </summary>
        public static void TurnOn()
        {
            SetDCHU_Data(103, new byte[4] { 0, 0, 1, 224 }, 4);
            WriteAppSettings(2, 84, 1, ref new byte[1] { 1 }[0]);
        }

        /// <summary>
        /// Whether or not the boot effect (cycling through colours) should be overridden
        /// </summary>
        /// <param name="value">True = no boot effect, False = default boot effect</param>
        public static void OverrideBootEffect(bool value)
        {
            byte num = (byte)(value ? 1 : 0);
            SetDCHU_Data(121, new byte[4] { num, 0, 0, 30 }, 4);
            WriteAppSettings(2, 7, 1, ref new byte[1] { num }[0]);
        }

        /// <summary>
        /// Sets the time before the keyboard LEDs switch off (also sets the trigger on if it was switched off)
        /// </summary>
        /// <param name="value"></param>
        public static void SetSleepTimer(int value)
        {
            WriteAppSettings(2, 36, 1, ref new byte[1] { 1 }[0]);
            WriteAppSettings(2, 37, 3, ref new byte[3] { (byte)(value / 3600), (byte)(value / 60 % 60), (byte)(value % 60) }[0]);
            byte[] bytes = BitConverter.GetBytes(402653184 + (value << 8) | (int)byte.MaxValue);
            SetDCHU_Data(121, bytes, bytes.Length);
        }

        public static void WriteAcpiSleepTimer(int value)
        {
            WriteAppSettings(2, 37, 3, ref new byte[3]
            {
        (byte) (value / 3600),
        (byte) (value / 60 % 60),
        (byte) (value % 60)
            }[0]);
        }

        /// <summary>
        /// Removes the trigger of the keyboard LED off switching, effectively setting it to infinity.
        /// </summary>
        public static void TurnSleepTimerOff()
        {
            SetDCHU_Data(121, new byte[4] { 0, 0, 0, 24 }, 4);
            WriteAppSettings(2, 36, 1, ref new byte[1] { 0 }[0]);
        }
    }
}