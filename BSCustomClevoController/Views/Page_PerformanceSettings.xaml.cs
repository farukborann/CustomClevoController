using BSCustomClevoController.Utility;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BSCustomClevoController.Views
{
    /// <summary>
    /// Page_Performance.xaml etkileşim mantığı
    /// </summary>
    public partial class Page_PerformanceSettings : Page
    {
        SolidColorBrush colorRed = new SolidColorBrush(Color.FromArgb(255, 217, 25, 25));
        SolidColorBrush colorGray = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));

        string QuietModeDes = "The fan is slowed down enough to be silent, which may lead to overheating during prolonged use.";
        string PowerSaveModeDes = "Performance is sacrificed to reduce battery usage, but it increases usage time.";
        string PerformanceModeDes = "All hardware is powered at maximum energy, pushing the computer to its highest performance capacity. Battery life is decreased when not plugged in."; // My mistake
        string EntertainmentModeDes = "Performance and power consumption are balanced; intended for high-performance needs such as gaming, design, editing, etc. (Recommended when plugged in.)";

        public Page_PerformanceSettings()
        {
            InitializeComponent();
            switch (PowerMode.GetStatus())
            {
                case PowerMode.Mode.Quiet:
                    SetButtonsColors(QuietModeBtnText);
                    modeDetais.Text = QuietModeDes;
                    break;
                case PowerMode.Mode.Powersaving:
                    SetButtonsColors(PowerSaveModeBtnText);
                    modeDetais.Text = PowerSaveModeDes;
                    break;
                case PowerMode.Mode.Performance:
                    SetButtonsColors(PerformanceModeBtnText);
                    modeDetais.Text = PerformanceModeDes;
                    break;
                case PowerMode.Mode.Entertainment:
                    SetButtonsColors(EntertainmentModeBtnText);
                    modeDetais.Text = EntertainmentModeDes;
                    break;
            }

        }

        public void SetButtonsColors(TextBlock text)
        {
            if (text == QuietModeBtnText) { QuietModeBtnText.Foreground = colorRed; }
            else QuietModeBtnText.Foreground = colorGray;

            if (text == PowerSaveModeBtnText) { PowerSaveModeBtnText.Foreground = colorRed; }
            else PowerSaveModeBtnText.Foreground = colorGray;

            if (text == PerformanceModeBtnText) { PerformanceModeBtnText.Foreground = colorRed; }
            else PerformanceModeBtnText.Foreground = colorGray;

            if (text == EntertainmentModeBtnText) { EntertainmentModeBtnText.Foreground = colorRed; }
            else EntertainmentModeBtnText.Foreground = colorGray;
        }

        private void QuietModeBtn_Click(object sender, RoutedEventArgs e)
        {
            PowerMode.SetPowerMode(PowerMode.Mode.Quiet);
            SetButtonsColors(QuietModeBtnText);
        }

        private void PowerSaveModeBtn_Click(object sender, RoutedEventArgs e)
        {
            PowerMode.SetPowerMode(PowerMode.Mode.Powersaving);
            SetButtonsColors(PowerSaveModeBtnText);
        }

        private void PerformanceModeBtn_Click(object sender, RoutedEventArgs e)
        {
            PowerMode.SetPowerMode(PowerMode.Mode.Performance);
            SetButtonsColors(PerformanceModeBtnText);
        }

        private void EntertainmentModeBtn_Click(object sender, RoutedEventArgs e)
        {
            PowerMode.SetPowerMode(PowerMode.Mode.Entertainment);
            SetButtonsColors(EntertainmentModeBtnText);
        }

        private void QuietModeBtn_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            modeDetais.Text = QuietModeDes;
        }

        private void PowerSaveModeBtn_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            modeDetais.Text = PowerSaveModeDes;
        }

        private void EntertainmentModeBtn_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            modeDetais.Text = EntertainmentModeDes;
        }

        private void PerformanceModeBtn_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            modeDetais.Text = PerformanceModeDes;
        }
    }
}
