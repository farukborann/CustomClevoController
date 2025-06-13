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
