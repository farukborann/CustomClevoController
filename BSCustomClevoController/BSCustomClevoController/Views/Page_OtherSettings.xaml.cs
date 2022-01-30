using BSCustomClevoController.Utility;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BSCustomClevoController.Views
{
    /// <summary>
    /// Page_Performance.xaml etkileşim mantığı
    /// </summary>
    public partial class Page_OtherSettings : Page
    {
        public Page_OtherSettings()
        {
            InitializeComponent();
            FeaturesListBox.ItemsSource = Capabilities.Check();

            List<string> monitorModes = new List<string>() { "Açık", "Kapalı", "StandBy" };
            monitorModesComboBox.ItemsSource = monitorModes;
        }

        private void monitorModesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (monitorModesComboBox.SelectedIndex)
            {
                case 0:
                    MonitorControl.ChangeMonitorState(MonitorControl.MonitorMode.MONITOR_ON);
                    break;
                case 1:
                    MonitorControl.ChangeMonitorState(MonitorControl.MonitorMode.MONITOR_OFF);
                    break;
                case 2:
                    MonitorControl.ChangeMonitorState(MonitorControl.MonitorMode.MONITOR_STANBY);
                    break;

            }
        }

        private void TouchPadToggle_Click(object sender, RoutedEventArgs e)
        {
            Touchpad.ToggleTouchpad();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
