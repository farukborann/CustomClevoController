using System;
using System.Drawing;
using System.Windows.Threading;

namespace BSCustomClevoController.Utility
{
    internal class Structs
    {
        public struct KeyboardStatus
        {
            public Color BackLight;
            public KeyboardEffect Effect;
            public bool LightState;
            public bool BootEffect;
            public int SleepSecond;
            public bool SleepState;
        }

        public struct KeyboardEffect
        {
            public byte UniqId;
            public int Id;
            public string Name;
            public int Milisecons;
            public Action<object, EventArgs> Func;

            private DispatcherTimer _Timer;
            public DispatcherTimer Timer
            {
                get
                {
                    if (_Timer == null)
                    {
                        _Timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(Milisecons) };
                        _Timer.Tick += new EventHandler(Func);
                    }
                    return _Timer;
                }
            }

            public static KeyboardEffect LookUpEffect(byte mode)
            {
                if (mode == 8) return FindSoftwareEffect();
                else
                {
                    foreach (KeyboardEffect effect in KeyboardEffects.Effects)
                    {
                        if (effect.UniqId == mode) { return effect; }
                    }
                    return default;
                }
            }

            public static KeyboardEffect FindSoftwareEffect()
            {
                foreach (KeyboardEffect effect in KeyboardEffects.Effects)
                {
                    if (effect.UniqId == 0 && effect.Timer != null && effect.Timer.IsEnabled) return effect;
                }
                return default;
            }

            public override string ToString()
            {
                return string.Format("{0} {1}", Name, UniqId == 0 ? " (Software)" : " (Driver)");
            }

            public void SetEffect()
            {
                if (UniqId != 0) { KeyboardBackLight.SetMode(this); }
                else 
                {
                    if (FindSoftwareEffect()._Timer != null) 
                    { 
                        FindSoftwareEffect().Timer.Stop(); 
                    }
                    Timer.Start();
                }
            }
        }

        public struct FanStatus
        {
            public Fan.Mode Mode;
            public int Offset;
            public FanInfo Fan_CPU;
            public FanInfo Fan_GPU1;
        }

        public struct FanInfo
        {
            public byte T1;
            public byte T2;
            public byte T3;
            public byte T4;
            public byte T2_Default;
            public byte T3_Default;
            public byte D1;
            public byte D2;
            public byte D3;
            public byte D4;
            public byte D2_Default;
            public byte D3_Default;
            public int R12;
            public int R23;
            public int R34;
            public int rpm;
            public int remote;
            public int percent;

            public FanInfo(int T1, int D1, int T2, int D2, int T3, int D3)
            {
                this.T1 = (byte)T1;
                this.T2 = (byte)T2;
                this.T3 = (byte)T3;
                this.D1 = (byte)D1;
                this.D2 = (byte)D2;
                this.D3 = (byte)D3;

                T4 = 0;
                T2_Default = 0;
                T3_Default = 0;
                D4 = 0;
                D2_Default = 0;
                D3_Default = 0;
                R12 = 0;
                R23 = 0;
                R34 = 0;
                rpm = 0;
                remote = 0;
                percent = 0;
            }
        }
    }
}
