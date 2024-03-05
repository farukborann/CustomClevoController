using BSCustomClevoController.Utility;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using static BSCustomClevoController.Utility.Structs;

namespace BSCustomClevoController.Views
{
    /// <summary>
    /// Page_Performance.xaml etkileşim mantığı
    /// </summary>
    public partial class Page_FanSettings : Page
    {
        string AutoDesc = "Fan hızı otomatik ayarlanır, gündelik kullanım içindir.";
        string MaxDesc = "Fan hızı en yükseğe ayarlanır, bilgisayarı hızlı soğutmak içindir.";
        string MaxQDesc = "Fan MaxQ moduna alınır, her model bilgisayar desteklemeyebilir.";
        string AntiDustDesc = "Bilgisayar bileşenlerindeki tozu çıkartmak içindir, her model bilgisayar desteklemeyebilir.";
        string CustomDesc = "Fan hızı yapacağınız özel ayara göre ayarlanır, belirli derecelerde belirli hızlara ayarlayabilirsiniz.";

        public Page_FanSettings()
        {
            InitializeComponent();

            FanStatus fanStatus = Fan.GetStatus();

            DispatcherTimer rpmTimer = new DispatcherTimer();
            rpmTimer.Tick += new EventHandler(rpmTimer_Tick);
            rpmTimer.Interval = new TimeSpan(0, 0, 1);
            rpmTimer.Start();

            OffsetSlider.Value = fanStatus.Offset;
            OffsetValueText.Text = ((int)OffsetSlider.Value).ToString();

            SetCustomFanValues(fanStatus.Fan_CPU, fanStatus.Fan_GPU1);
            switch (fanStatus.Mode)
            {
                case Fan.Mode.Auto:
                    FanModeDesText.Text = AutoDesc;
                    SetCanvas(mainCanvas);
                    break;
                case Fan.Mode.MaxQ:
                    FanModeDesText.Text = MaxQDesc;
                    SetCanvas(rightCanvas);
                    break;
                case Fan.Mode.Max:
                    FanModeDesText.Text = MaxDesc;
                    SetCanvas(upCanvas);
                    break;
                case Fan.Mode.Custom:
                    FanModeDesText.Text = CustomDesc;
                    SetCanvas(downCanvas);
                    SetCustomFanUIElements(true);
                    break;
                case Fan.Mode.Unknown:
                    FanModeDesText.Text = "Bilinmeyen Fan Modu";
                    break;
            }
        }

        private void SetCustomFanUIElements(bool active)
        {
            ApplyCustomFan.IsEnabled = active;

            CPU_Temp1.IsEnabled = active;
            CPU_Temp2.IsEnabled = active;
            CPU_Speed1.IsEnabled = active;
            CPU_Speed2.IsEnabled = active;

            GPU_Temp1.IsEnabled = active;
            GPU_Temp2.IsEnabled = active;
            GPU_Speed1.IsEnabled = active;
            GPU_Speed2.IsEnabled = active;
        }

        private void SetCustomFanValues(FanInfo CPU, FanInfo GPU)
        {
            CPU_Temp1.Text = CPU.T2.ToString();
            CPU_Temp2.Text = CPU.T3.ToString();
            CPU_Speed1.Text = CPU.D2.ToString();
            CPU_Speed2.Text = CPU.D3.ToString();

            GPU_Temp1.Text = GPU.T2.ToString();
            GPU_Temp2.Text = GPU.T3.ToString();
            GPU_Speed1.Text = GPU.D2.ToString();
            GPU_Speed2.Text = GPU.D3.ToString();
        }

        private void rpmTimer_Tick(object sender, EventArgs e)
        {
            int rpm_CPU = 0;
            int percent_CPU = 0;
            int remote_CPU = 0;
            int rpm_GPU = 0;
            int percent_GPU = 0;
            int remote_GPU = 0;

            Fan.Read_FanSpeed(ref rpm_CPU, ref percent_CPU, ref remote_CPU, ref rpm_GPU, ref percent_GPU, ref remote_GPU);

            RpmCpuText.Text = string.Format("{0} RPM {1}% {2}°C", rpm_CPU > 0 ? rpm_CPU : 0, percent_CPU > 0 ? percent_CPU : 0, remote_CPU > 0 ? remote_CPU : 0);
            RpmGpuText.Text = string.Format("{0} RPM {1}% {2}°C", rpm_GPU > 0 ? rpm_GPU : 0, percent_GPU > 0 ? percent_CPU : 0, remote_GPU > 0 ? remote_GPU : 0);

        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            FanModeDesText.Text = AutoDesc;
        }

        private void leftCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            FanModeDesText.Text = AntiDustDesc;
        }

        private void upCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            FanModeDesText.Text = MaxDesc;
        }

        private void rightCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            FanModeDesText.Text = MaxQDesc;
        }

        private void downCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            FanModeDesText.Text = CustomDesc;
        }

        public void SetCanvas(Canvas can)
        {
            if (can == mainCanvas) mainCanvas.Opacity = 0.6;
            else mainCanvas.Opacity = 1;

            if (can == leftCanvas) leftCanvas.Opacity = 0.6;
            else leftCanvas.Opacity = 1;

            if (can == upCanvas) upCanvas.Opacity = 0.6;
            else upCanvas.Opacity = 1;

            if (can == rightCanvas) rightCanvas.Opacity = 0.6;
            else rightCanvas.Opacity = 1;

            if (can == downCanvas) downCanvas.Opacity = 0.6;
            else downCanvas.Opacity = 1;
        }

        private void mainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SetCustomFanUIElements(false);
            Fan.SetMode(Fan.Mode.Auto);
            SetCanvas(mainCanvas);
        }

        private void leftCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SetCustomFanUIElements(false);
            SetCanvas(leftCanvas);
        }

        private void upCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SetCustomFanUIElements(false);
            Fan.SetMode(Fan.Mode.Max);
            SetCanvas(upCanvas);
        }

        private void rightCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SetCustomFanUIElements(false);
            Fan.SetMode(Fan.Mode.MaxQ);
            SetCanvas(rightCanvas);
        }

        private void downCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SetCustomFanUIElements(true);
            SetCanvas(downCanvas);
        }

        private void OffsetSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            Fan.SetOffset(Convert.ToByte((int)OffsetSlider.Value));
        }

        private void OffsetSlider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            OffsetValueText.Text = ((int)OffsetSlider.Value).ToString();
        }

        private void OffsetSlider_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OffsetValueText.Text = ((int)OffsetSlider.Value).ToString();
        }

        private void ApplyCustomFan_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                int CPU_T1 = Convert.ToInt32(CPU_Temp1.Text);
                int CPU_T2 = Convert.ToInt32(CPU_Temp2.Text);
                int CPU_D1 = Convert.ToInt32(CPU_Speed1.Text);
                int CPU_D2 = Convert.ToInt32(CPU_Speed2.Text);

                int GPU_T1 = Convert.ToInt32(GPU_Temp1.Text);
                int GPU_T2 = Convert.ToInt32(GPU_Temp2.Text);
                int GPU_D1 = Convert.ToInt32(GPU_Speed1.Text);
                int GPU_D2 = Convert.ToInt32(GPU_Speed2.Text);

                if (CPU_T1 >= CPU_T2 || 40 >= CPU_T1 || CPU_T2 >= 100) MessageBox.Show("Sıcaklık artacak şekilde ayarlanmalıdır !!! (CPU)");
                else if (CPU_D1 >= CPU_D2 || 35 >= CPU_D1 || CPU_D2 >= 100) MessageBox.Show("Sıcaklık arttıkça hızda artmalıdır !!! (CPU)");

                if (GPU_T1 >= GPU_T2 || 40 >= GPU_T1 || GPU_T2 >= 100) MessageBox.Show("Sıcaklık artacak şekilde ayarlanmalıdır !!! (GPU)");
                else if (GPU_D1 >= GPU_D2 || 35 >= GPU_D1 || GPU_D2 >= 100) MessageBox.Show("Sıcaklık arttıkça hızda artmalıdır !!! (GPU)");
                else
                {
                    Fan.SetCustomFanTable(new FanInfo(40, 35, CPU_T1, CPU_D1, CPU_T2, CPU_D2),
                                          new FanInfo(40, 35, GPU_T1, GPU_D1, GPU_T2, GPU_D2));
                }

            }
            catch (FormatException)
            {
                MessageBox.Show("Lütfen Sadece Sayı Giriniz !!!");
            }
        }


    }
}
