using System;
using System.Drawing;
using static BSCustomClevoController.Utility.Structs;

namespace BSCustomClevoController.Utility
{
    internal static class KeyboardEffects
    {
        //Set Effects
        public static KeyboardEffect Random = new KeyboardEffect() { Name = "Random", UniqId = 112, Id = 0 }; /// Goes every ~0.5 sec to a different random colour (with the current brightness)
        public static KeyboardEffect Breath = new KeyboardEffect() { Name = "Breath", UniqId = 16, Id = 2 }; /// Breathes with the current colour (cycles from 0 to brightness, around 2 sec)
        public static KeyboardEffect Cycle = new KeyboardEffect() { Name = "Cycle", UniqId = 51, Id = 3 }; /// Cycles through primary and secondary colours (going from 0 to brightness in between, around 2 sec). Blue, Green, Red, Blue?, Cyan, Yellow, Pink  
        public static KeyboardEffect Wave = new KeyboardEffect() { Name = "Wave", UniqId = 176, Id = 4 }; /// Slowly (around every 10 sec) shows a random colour slowly increasing and then decreasing the brightness (goes from 0 to brightness)
        public static KeyboardEffect Dance = new KeyboardEffect() { Name = "Dance", UniqId = 128, Id = 5 }; /// Flashes every 0.5 sec with a different random colour (goes from 0 to brightness)
        public static KeyboardEffect Tempo = new KeyboardEffect() { Name = "Tempo", UniqId = 144, Id = 6 }; /// Flashes a colour, then goes off shortly, then flashes the same colour, then goes off longer ~1 sec, then starts over with a different colour (goes from 0 to brightness)
        public static KeyboardEffect Flash = new KeyboardEffect() { Name = "Flash", UniqId = 160, Id = 7 }; /// Flashes every 1 sec with a different random colour (goes from 0 to brightness)
        public static KeyboardEffect SoftwareBreath = new KeyboardEffect() { Name = "Breath", UniqId = 0, Id = 8, Func = BreathEffect, Milisecons = 1 }; // , Milisecons=40
        public static KeyboardEffect SoftwareCycle = new KeyboardEffect() { Name = "Cycle", UniqId = 0, Id = 9, Func = CycleEffect, Milisecons = 100 };

        public static KeyboardEffect[] Effects = new KeyboardEffect[] { Random, Breath, Cycle, Wave, Dance, Tempo, Flash, SoftwareBreath, SoftwareCycle };
        static bool lowA = false;
        public static async void BreathEffect(object sender, EventArgs e)
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                KeyboardStatus status = KeyboardBackLight.GetStatus();

                if (status.BackLight.A == 255) { lowA = true; }
                else if (status.BackLight.A == 0) { lowA = false; }

                int newAlpha = lowA ? status.BackLight.A - 5 : status.BackLight.A + 5;
                if (newAlpha > 255) newAlpha = 255;
                if (newAlpha < 0) newAlpha = 0;

                KeyboardBackLight.SetBrightness(Convert.ToByte(newAlpha));
            });
        }

        static Color targetColor = Color.Blue;
        public static Color Interpolate(this Color source, Color target)
        {
            int clamp(int value) => Math.Max(0, Math.Min(255, value)); // Refactor

            int r = clamp(source.R + Math.Sign(target.R - source.R) * 5);
            int g = clamp(source.G + Math.Sign(target.G - source.G) * 5);
            int b = clamp(source.B + Math.Sign(target.B - source.B) * 5);

            return Color.FromArgb(255, r, g, b);
        }

        public static async void CycleEffect(object sender, EventArgs e) // Fix Crash
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    KeyboardStatus status = KeyboardBackLight.GetStatus();
                    Color current = status.BackLight; // Assuming BackLight is a Color struct

                    if (current.ToArgb() != targetColor.ToArgb())
                    {
                        Color newColor = Interpolate(current, targetColor);
                        KeyboardBackLight.SetColour(newColor.R, newColor.G, newColor.B);
                    }
                    else
                    {
                        if (current.ToArgb() == Color.Blue.ToArgb()) targetColor = Color.FromArgb(255, 0, 125, 0); // Green
                        else if (current.ToArgb() == Color.FromArgb(255, 0, 125, 0).ToArgb()) targetColor = Color.Red;
                        else if (current.ToArgb() == Color.Red.ToArgb()) targetColor = Color.Cyan;
                        else if (current.ToArgb() == Color.Cyan.ToArgb()) targetColor = Color.Yellow;
                        else if (current.ToArgb() == Color.Yellow.ToArgb()) targetColor = Color.FromArgb(255, 255, 190, 205); // Pink
                        else if (current.ToArgb() == Color.FromArgb(255, 255, 190, 205).ToArgb()) targetColor = Color.Blue;

                        Color newColor = Interpolate(current, targetColor);
                        KeyboardBackLight.SetColour(newColor.R, newColor.G, newColor.B);
                    }
                }
            });
        }
    }
}
