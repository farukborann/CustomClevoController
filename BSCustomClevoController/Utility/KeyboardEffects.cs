using System;
using System.Drawing;
using static BSCustomClevoController.Utility.Structs;

namespace BSCustomClevoController.Utility
{
    internal static class KeyboardEffects
    {
        public static int updateRate = 16; // update rate
        public static int Duration = 1000; // fallback

        // Hardware effects
        public static KeyboardEffect Random = new KeyboardEffect() { Name = "Random", UniqId = 112, Id = 0 };
        public static KeyboardEffect Breath = new KeyboardEffect() { Name = "Breath", UniqId = 16, Id = 2 };
        public static KeyboardEffect Cycle = new KeyboardEffect() { Name = "Cycle", UniqId = 51, Id = 3 };
        public static KeyboardEffect Wave = new KeyboardEffect() { Name = "Wave", UniqId = 176, Id = 4 };
        public static KeyboardEffect Dance = new KeyboardEffect() { Name = "Dance", UniqId = 128, Id = 5 };
        public static KeyboardEffect Tempo = new KeyboardEffect() { Name = "Tempo", UniqId = 144, Id = 6 };
        public static KeyboardEffect Flash = new KeyboardEffect() { Name = "Flash", UniqId = 160, Id = 7 };

        // Software effects
        public static KeyboardEffect SoftwareBreath = new KeyboardEffect() { Name = "Breath", UniqId = 0, Id = 8, Func = BreathEffect, Milisecons = updateRate };
        public static KeyboardEffect SoftwareCycle = new KeyboardEffect() { Name = "Cycle", UniqId = 0, Id = 9, Func = CycleEffect, Milisecons = updateRate };
        public static KeyboardEffect SoftwareBreathColor = new KeyboardEffect() { Name = "Colorful Breath", UniqId = 0, Id = 10, Func = ColorfulBreathEffect, Milisecons = updateRate };

        public static KeyboardEffect[] Effects = new KeyboardEffect[]
        {
            Random, Breath, Cycle, Wave, Dance, Tempo, Flash, SoftwareBreath, SoftwareCycle, SoftwareBreathColor
        };

        private static readonly Color[] Colors = new Color[]
        {
            Color.Red,
            Color.Blue,
            Color.Green,
            Color.Yellow,
            Color.Cyan,
            Color.Magenta
        };

        // states
        private static bool breathDimming = false;
        private static int breathColorIndex = 0;

        public static async void BreathEffect(object sender, EventArgs e) // refactor
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                int timer = Duration;
                float alphaStep = 255f * updateRate / timer;

                KeyboardStatus status = KeyboardBackLight.GetStatus();

                if (status.BackLight.A >= 255)
                {
                    breathDimming = true;
                }
                else if (status.BackLight.A <= 0)
                {
                    breathDimming = false;
                }

                float alpha = status.BackLight.A;
                alpha = breathDimming ? alpha - alphaStep : alpha + alphaStep;

                alpha = Math.Max(0f, Math.Min(255f, alpha));

                KeyboardBackLight.SetBrightness(Convert.ToByte(alpha));
                await System.Threading.Tasks.Task.Delay(updateRate);
            });
        }

        public static async void ColorfulBreathEffect(object sender, EventArgs e) //refactor from breatheffect code to use colors
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                int timer = Duration;
                float alphaStep = 255f * updateRate / timer;

                KeyboardStatus status = KeyboardBackLight.GetStatus();
                float alpha = status.BackLight.A;

                if (alpha >= 255f)
                {
                    breathDimming = true;
                }
                else if (alpha <= 0f)
                {
                    breathDimming = false;
                    breathColorIndex = (breathColorIndex + 1) % Colors.Length; // change color after brightness reaches 0
                }

                alpha = breathDimming ? alpha - alphaStep : alpha + alphaStep;
                alpha = Math.Max(0f, Math.Min(255f, alpha));

                Color baseColor = Colors[breathColorIndex];
                float brightness = alpha / 255f;

                int r = (int)(baseColor.R);
                int g = (int)(baseColor.G);
                int b = (int)(baseColor.B);

                KeyboardBackLight.SetColour((byte)Clamp(r), (byte)Clamp(g), (byte)Clamp(b));
                KeyboardBackLight.SetBrightness(Convert.ToByte(alpha));

                await System.Threading.Tasks.Task.Delay(updateRate);
            });
        }

        private static Color InterpolateColor(Color start, Color end, float t) // smooth color transition for cycle effect
        {
            int r = (int)(start.R + (end.R - start.R) * t);
            int g = (int)(start.G + (end.G - start.G) * t);
            int b = (int)(start.B + (end.B - start.B) * t);
            return Color.FromArgb(255, Clamp(r), Clamp(g), Clamp(b));
        }

        private static int currentColorIndex = 0;
        private static DateTime transitionStart = DateTime.Now;
        public static async void CycleEffect(object sender, EventArgs e) // refactor
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                double currentTime = (DateTime.Now - transitionStart).TotalMilliseconds;
                float timer = (float)(currentTime / Duration);
                timer = Math.Min(1f, timer);

                Color from = Colors[currentColorIndex];
                Color to = Colors[(currentColorIndex + 1) % Colors.Length];

                if (timer >= 1f)
                {
                    currentColorIndex = (currentColorIndex + 1) % Colors.Length;
                    transitionStart = DateTime.Now;
                    timer = 0f;

                    from = Colors[currentColorIndex];
                    to = Colors[(currentColorIndex + 1) % Colors.Length];
                }

                Color interpolated = InterpolateColor(from, to, timer);
                KeyboardBackLight.SetColour(interpolated.R, interpolated.G, interpolated.B);

                await System.Threading.Tasks.Task.Delay(updateRate);
            });
        }

        private static int Clamp(int value) => Math.Max(0, Math.Min(255, value)); // limit everything
    }
}
