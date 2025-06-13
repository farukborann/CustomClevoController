using BSCustomClevoController.Utility;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static BSCustomClevoController.Utility.Structs;

namespace BSCustomClevoController.Views
{
    /// <summary>
    /// Page_Performance.xaml etkileşim mantığı
    /// </summary>
    public partial class Page_KeyboardSettings : Page
    {
        public Page_KeyboardSettings()
        {
            InitializeComponent();

            KeyboardStatus status = KeyboardBackLight.GetStatus();

            //Set effects controls
            EffectsListBox.ItemsSource = KeyboardEffects.Effects;
            if (!status.Effect.Equals(default(KeyboardEffect)))
            {
                ModeStatus.IsChecked = true;
                for(int i=0; i<KeyboardEffects.Effects.Length; i++)
                {
                    if (KeyboardEffects.Effects[i].UniqId == status.Effect.UniqId) EffectsListBox.SelectedIndex = i;
                }
                //EffectsListBox.SelectedIndex = KeyboardEffects.Effects.FindIndex(status.Effect);
            }


            //Set color controls
            if (status.LightState) { LedStatusCheckBox.IsChecked = true; }

            //Set booteffect
            if (status.BootEffect) { BootEffectCheckBox.IsChecked = true; }

            //Set timer
            if (!status.SleepState)
            {
                KeyboarSleepTimerHours.Text = "0";
                KeyboarSleepTimerMunites.Text = "0";
                KeyboarSleepTimerSeconds.Text = "0";
            }
            else
            {
                KeyboarSleepTimerHours.Text = Convert.ToString(status.SleepSecond / 3600);
                KeyboarSleepTimerMunites.Text = Convert.ToString(status.SleepSecond % 3600 / 60);
                KeyboarSleepTimerSeconds.Text = Convert.ToString(status.SleepSecond % 3600 % 60);
            }
            KeyboarSleepTimerHours.TextChanged += KeyboarSleepTimer_TextChanged;
            KeyboarSleepTimerMunites.TextChanged += KeyboarSleepTimer_TextChanged;
            KeyboarSleepTimerSeconds.TextChanged += KeyboarSleepTimer_TextChanged;


        }

        private void LedColorCanvas_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            System.Threading.Thread.Sleep(100); //reduce cpu usage
            KeyboardBackLight.SetColour(Convert.ToByte(LedColorCanvas.R), Convert.ToByte(LedColorCanvas.G), Convert.ToByte(LedColorCanvas.B));
            KeyboardBackLight.SetBrightness(Convert.ToByte(LedColorCanvas.A));
        }

        private void ModeStatus_Checked(object sender, RoutedEventArgs e)
        {
            EffectsListBox.IsEnabled = true;
            ApplyEffectButton.IsEnabled = true;
        }

        private void ModeStatus_Unchecked(object sender, RoutedEventArgs e)
        {
            EffectsListBox.IsEnabled = false;
            ApplyEffectButton.IsEnabled = false;
            if (KeyboardBackLight.GetStatus().Effect.Timer != null)
            {
                KeyboardBackLight.GetStatus().Effect.Timer.Stop();
            }
        }

        private void LedStatusCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            KeyboardStatus status = KeyboardBackLight.GetStatus();
            if (status.LightState == false)
            {
                LedColorCanvas.IsEnabled = true;

                KeyboardBackLight.TurnOn();
                KeyboardBackLight.SetColour(255, 255, 255);
                KeyboardBackLight.SetBrightness(255);
                LedColorCanvas.SelectedColor = Color.FromRgb(255, 255, 255);
            }
            else
            {
                LedColorCanvas.IsEnabled = true;
                LedColorCanvas.SelectedColor = new Color() { A = status.BackLight.A, R = status.BackLight.R, G = status.BackLight.G, B = status.BackLight.B };
            }

        }

        private void LedStatusCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            LedColorCanvas.IsEnabled = false;
            KeyboardBackLight.TurnOff();
        }

        private void ApplyEffectButton_Click(object sender, RoutedEventArgs e)
        {
            KeyboardBackLight.SetBrightness(255);
            KeyboardEffects.Effects[EffectsListBox.SelectedIndex].SetEffect();

            if (int.TryParse(EffectDurationBox.Text, out int duration))
            {
                if (duration < 1000)
                {
                    duration = 1000;
                    EffectDurationBox.Text = "1000"; // update the box
                    MessageBox.Show("Minimum duration is 1000 ms (1 sec)", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                KeyboardEffects.Duration = duration;
            }
            else
            {
                KeyboardEffects.Duration = 3000; // fallback
                EffectDurationBox.Text = "3000";
                MessageBox.Show("Invalid input. Duration has been reset to 3000 ms.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BootEffectCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            KeyboardBackLight.OverrideBootEffect(true);
        }

        private void BootEffectCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            KeyboardBackLight.OverrideBootEffect(false);
        }

        private void KeyboarSleepTimer_TextChanged(object sender, TextChangedEventArgs e)
        {
            int time = 0;
            try
            {
                int value = Convert.ToInt32(((TextBox)sender).Text);
                if (value > 59) ((TextBox)sender).Text = "59";
            }
            catch (FormatException)
            {
                if (((TextBox)sender).Text != "") ((TextBox)sender).Text = "0";
            }

            time += Convert.ToInt32(KeyboarSleepTimerHours != null && KeyboarSleepTimerHours.Text != "" ? KeyboarSleepTimerHours.Text : "0") * 3600 +
                    Convert.ToInt32(KeyboarSleepTimerMunites != null && KeyboarSleepTimerMunites.Text != "" ? KeyboarSleepTimerMunites.Text : "0") * 60 +
                    Convert.ToInt32(KeyboarSleepTimerSeconds != null && KeyboarSleepTimerSeconds.Text != "" ? KeyboarSleepTimerSeconds.Text : "0");
            if (time != 0) { KeyboardBackLight.SetSleepTimer(time); }
            else { KeyboardBackLight.TurnSleepTimerOff(); }
        }
    }
}
