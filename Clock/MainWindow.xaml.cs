using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Clock
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker bgWorker = new BackgroundWorker();
        public MainWindow()
        {
            InitializeComponent();
            bgWorker.WorkerReportsProgress = true;//支持报告进度更新
            bgWorker.WorkerSupportsCancellation = true;//支持异步取消
            bgWorker.DoWork += BgWorker_DoWork;
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.125);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            double degh = (dt.Hour % 12) * 30.0 + dt.Minute * 0.5;
            double degm = dt.Minute * 6.0 + dt.Second * 0.1;
            double degs = dt.Second * 6.0;
            double radh = (degh / 180.0) * Math.PI;
            double radm = (degm / 180.0) * Math.PI;
            double rads = (degs / 180.0) * Math.PI;
            double xh, yh, xm, ym, xs, ys;
            xh = Math.Sin(radh) * 35.5 + 95.5;
            yh = -Math.Cos(radh) * 35.5 + 95.5;
            xm = Math.Sin(radm) * 63.5 + 95.5;
            ym = -Math.Cos(radm) * 63.5 + 95.5;
            xs = Math.Sin(rads) * 83.5 + 95.5;
            ys = -Math.Cos(rads) * 83.5 + 95.5;
            this.LHour.X2 = xh;
            this.LHour.Y2 = yh;
            this.LMinute.X2 = xm;
            this.LMinute.Y2 = ym;
            this.LSecond.X2 = xs;
            this.LSecond.Y2 = ys;
        }
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {

        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            File.WriteAllText(Directory.GetCurrentDirectory() + "Clock.ini", Convert.ToString(this.Left));
            File.AppendAllText(Directory.GetCurrentDirectory() + "Clock.ini", "\n" + Convert.ToString(this.Top));
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ShowInTaskbar = false;
            if (File.Exists(Directory.GetCurrentDirectory() + "Clock.ini"))
            {
                string[] locate = File.ReadAllLines(Directory.GetCurrentDirectory() + "Clock.ini");
                this.Left = Convert.ToInt32(locate[0]);
                this.Top = Convert.ToInt32(locate[1]);
            }
        }
    }
}
