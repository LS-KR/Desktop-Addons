using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SystemMonitor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ShowInTaskbar = false;
            if (File.Exists(Directory.GetCurrentDirectory() + "SystemMonitor.ini"))
            {
                string[] locate = File.ReadAllLines(Directory.GetCurrentDirectory() + "SystemMonitor.ini");
                this.Left = Convert.ToInt32(locate[0]);
                this.Top = Convert.ToInt32(locate[1]);
            }
            Bitmap bitmapcpu = Properties.Resources.cpu;
            Bitmap bitmapmem = Properties.Resources.ram;
            this.ICPU.Source = BitmapToBitmapImage(bitmapcpu);
            this.IRAM.Source = BitmapToBitmapImage(bitmapmem);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            float cpuusage = API.CPU.GetCPUUsage();
            float ramusage = (float)((API.RAM.GetUsedPhys() / (float)API.RAM.GetTotalPhys()) * 100.0);
            string cpus = string.Format("{0:0.0}", cpuusage) + "%";
            string rams = string.Format("{0:0.0}", ramusage) + "%";
            this.LCPU.Content = cpus;
            this.LRAM.Content = rams;
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            File.WriteAllText(Directory.GetCurrentDirectory() + "SystemMonitor.ini", Convert.ToString(this.Left));
            File.AppendAllText(Directory.GetCurrentDirectory() + "SystemMonitor.ini", "\n" + Convert.ToString(this.Top));
        }
        private BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, bitmap.RawFormat);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }
    }
}
