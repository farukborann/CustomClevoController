using BSCustomClevoController.Utility;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
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
            EffectsListBox.ItemsSource = KeyboardBackLight.KeyboardEffects;
            if (!status.Effect.Equals(default(KeyboardEffect))) 
            {
                ModeStatus.IsChecked = true;
                EffectsListBox.SelectedIndex = KeyboardBackLight.KeyboardEffects.FindIndex(x => x.Equals(status.Effect));
            }


            //Set color controls
            if (status.LightState) { LedStatusCheckBox.IsChecked = true; }

            //Set booteffect
            if(status.BootEffect) { BootEffectCheckBox.IsChecked = true; }

            //Set timer
            if(status.SleepState) {
                SleepTimerCheckBox.IsChecked = true;
                KeyboarSleepTimerApplyButton.IsEnabled = true;
                KeyboarSleepTimerHours.IsEnabled = true;
                KeyboarSleepTimerMunites.IsEnabled = true;
                KeyboarSleepTimerSeconds.IsEnabled = true;

                KeyboarSleepTimerHours.Text = Convert.ToString(status.SleepSecond / 3600);
                KeyboarSleepTimerMunites.Text = Convert.ToString(status.SleepSecond % 3600 / 60);
                KeyboarSleepTimerSeconds.Text = Convert.ToString(status.SleepSecond % 3600 % 60);
            }
        }

        private void LedColorCanvas_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
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
        }

        private void LedStatusCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            KeyboardStatus status = KeyboardBackLight.GetStatus();
            if (status.LightState == false)
            {
                LedColorCanvas.IsEnabled = true;

                KeyboardBackLight.TurnOn();
                KeyboardBackLight.SetColour(255,0,0);
                KeyboardBackLight.SetBrightness(255);
                LedColorCanvas.SelectedColor = Color.FromRgb(255,0,0);
            }
            else
            {
                LedColorCanvas.IsEnabled = true;
                LedColorCanvas.SelectedColor = new Color() { A = status.BackLight.A, R = status.BackLight.R, G = status.BackLight.G, B = status.BackLight.B };
            }

        }

        private void LedStatusCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            LedColorCanvas.IsEnabled= false;
            KeyboardBackLight.TurnOff();
        }

        private void ApplyEffectButton_Click(object sender, RoutedEventArgs e)
        {
            KeyboardBackLight.SetBrightness(255);
            KeyboardBackLight.KeyboardEffects[EffectsListBox.SelectedIndex].SetEffect();
        }

        private void BootEffectCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            KeyboardBackLight.OverrideBootEffect(true);
        }

        private void BootEffectCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            KeyboardBackLight.OverrideBootEffect(false);
        }

        private void SleepTimerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            KeyboarSleepTimerApplyButton.IsEnabled = true;
            KeyboarSleepTimerHours.IsEnabled = true;
            KeyboarSleepTimerMunites.IsEnabled = true;
            KeyboarSleepTimerSeconds.IsEnabled = true;
        }

        private void SleepTimerCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            KeyboardBackLight.TurnSleepTimerOff();
            KeyboarSleepTimerApplyButton.IsEnabled = false;
            KeyboarSleepTimerHours.IsEnabled = false;
            KeyboarSleepTimerMunites.IsEnabled = false;
            KeyboarSleepTimerSeconds.IsEnabled = false;
        }

        private void KeyboarSleepTimer_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int time = Convert.ToInt32(((TextBox)sender).Text);
                if(time > 59) ((TextBox)sender).Text = "59";
            }
            catch (FormatException)
            {
                if(((TextBox)sender).Text != "") ((TextBox)sender).Text = "0";
            }
        }

        private void KeyboarSleepTimerApplyButton_Click(object sender, RoutedEventArgs e)
        {
            int time = 0;
            if (KeyboarSleepTimerHours.Text != "") time += Convert.ToInt32(KeyboarSleepTimerHours.Text) * 3600;
            if (KeyboarSleepTimerMunites.Text != "") time += Convert.ToInt32(KeyboarSleepTimerMunites.Text) * 60;
            if (KeyboarSleepTimerSeconds.Text != "") time += Convert.ToInt32(KeyboarSleepTimerSeconds.Text);
            KeyboardBackLight.SetSleepTimer(time);
        }
    }
}
