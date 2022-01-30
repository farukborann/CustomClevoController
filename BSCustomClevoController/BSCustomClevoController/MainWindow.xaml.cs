using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
using System.IO;
using System.Reflection;
using BSCustomClevoController.Utility;

namespace BSCustomClevoController
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly Frame _frame;

        public MainWindow()
        {
            InitializeComponent();

            //Set Notify Icon
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BSCustomClevoController.Assets.main_icon.ico");
            NotifyIcon notifyIcon = new NotifyIcon { Icon = new Icon(stream), Visible = true };

            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Performans Ayarları", null, notifyIconContext_ExitClick));
            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Klavye Ayarları", null, notifyIconContext_ExitClick));
            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Fan Ayarları", null, notifyIconContext_ExitClick));
            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Çıkış", null, notifyIconContext_ExitClick));

            notifyIcon.DoubleClick += delegate (object sender, EventArgs args)
                        {
                            Show();
                            WindowState = WindowState.Normal;
                        };

            //Set Hamburger Navigation
            _frame = new Frame { NavigationUIVisibility = NavigationUIVisibility.Hidden };
            _frame.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            _frame.VerticalAlignment = VerticalAlignment.Stretch;
            _frame.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;

            HamburgerMenuControl.Content = _frame;
            _frame.Navigate(new Uri("Views\\Page_PerformanceSettings.xaml", UriKind.Relative));
        }

        private void notifyIconContext_ExitClick(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();

            base.OnStateChanged(e);
        }

        private void HamburgerMenuControl_ItemClick(object sender, ItemClickEventArgs args)
        {
            _frame.Navigate(new Uri(string.Format("Views\\Page_{0}.xaml", ((HamburgerMenuIconItem)args.ClickedItem).Tag), UriKind.Relative));//((MahApps.Metro.Controls.HamburgerMenu)sender).SelectedItem ((HamburgerMenuIconItem)args.ClickedItem).Tag
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
