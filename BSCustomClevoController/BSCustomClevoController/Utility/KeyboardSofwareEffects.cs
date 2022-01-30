using System;
using System.Drawing;
using static BSCustomClevoController.Utility.Structs;

namespace BSCustomClevoController.Utility
{
    internal static class KeyboardSofwareEffects
    {
        static bool lowA = false;
        public static async void BreathEffect(object sender, EventArgs e)
        {
            await System.Threading.Tasks.Task.Run(() => {
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
            int r()
            {
                if (source.R > target.R) return source.R - 5;
                else if (source.R < target.R) return source.R + 5;
                else return source.R;
            }
            int g()
            {
                if (source.G > target.G) return source.G - 5;
                else if (source.G < target.G) return source.G + 5;
                else return source.G;
            }
            int b()
            {
                if (source.B > target.B) return source.B - 5;
                else if (source.B < target.B) return source.B + 5;
                else return source.B;
            }

            return Color.FromArgb(255, r(), g(), b());
        }
        public static async void CycleEffect(object sender, EventArgs e)// Blue Green Red Cyan Yellow Pink
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                KeyboardStatus status = KeyboardBackLight.GetStatus();

                if (targetColor.ToArgb() != status.BackLight.ToArgb()) 
                {
                    Color newColor = Interpolate(status.BackLight, targetColor);
                    KeyboardBackLight.SetColour(newColor.R, newColor.G, newColor.B); 
                }
                else
                {
                    if (status.BackLight.ToArgb() == Color.Blue.ToArgb()) { targetColor = Color.FromArgb(255, 0, 125, 0); }
                    else if (status.BackLight.ToArgb() == Color.FromArgb(255,0,125,0).ToArgb()) targetColor = Color.Red;
                    else if (status.BackLight.ToArgb() == Color.Red.ToArgb()) targetColor = Color.Cyan;
                    else if (status.BackLight.ToArgb() == Color.Cyan.ToArgb()) targetColor = Color.Yellow;
                    else if (status.BackLight.ToArgb() == Color.Yellow.ToArgb()) targetColor = Color.FromArgb(255, 255, 190, 205);
                    else if (status.BackLight.ToArgb() == Color.FromArgb(255, 255, 190, 205).ToArgb()) targetColor = Color.Blue;

                    KeyboardBackLight.SetColour(Interpolate(status.BackLight, targetColor));
                }
            });
        }
    }
}
