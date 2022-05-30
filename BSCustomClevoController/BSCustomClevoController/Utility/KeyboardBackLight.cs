using System;
using System.Drawing;
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

            return new KeyboardStatus()
            {
                BackLight = Color.FromArgb(brightness, colour[0], colour[1], colour[2]),
                Effect = KeyboardEffect.LookUpEffect(mode),
                LightState = Convert.ToBoolean(status),
                BootEffect = Convert.ToBoolean(booteffect),
                SleepSecond = sleepsec,
                SleepState = Convert.ToBoolean(sleepstatus)
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