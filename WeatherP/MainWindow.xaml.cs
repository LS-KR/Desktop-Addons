using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
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
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WeatherP
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
            if (File.Exists(Directory.GetCurrentDirectory() + "\\WeatherP.ini"))
            {
                string[] locate = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\WeatherP.ini");
                this.Left = Convert.ToInt32(locate[0]);
                this.Top = Convert.ToInt32(locate[1]);
            }
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromHours(3);
            timer.Tick += Timer_Tick;
            timer.Start();
            Timer_Tick(sender, e);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            CWeather.AWeather weather = new CWeather.AWeather();
            weather = CWeather.GetWeather();
            Bitmap img;
            switch (weather.Weather)
            {
                case CWeather.Weather.Sunny:
                    img = Properties.Resources.sunny;
                    break;
                case CWeather.Weather.Cloudy:
                    img = Properties.Resources.cloudy;
                    break;
                case CWeather.Weather.Rainy:
                    img = Properties.Resources.rainy;
                    break;
                case CWeather.Weather.Thunder:
                    img = Properties.Resources.thunder;
                    break;
                case CWeather.Weather.Snowy:
                    img = Properties.Resources.snowy;
                    break;
                case CWeather.Weather.Foggy:
                    img = Properties.Resources.foggy;
                    break;
                default:
                    img = Properties.Resources.sunny;
                    break;
            }
            this.IWeather.Source = BitmapToBitmapImage(img);
            this.LTemp.Content = Convert.ToString(weather.LowTemp) + "~" + Convert.ToString(weather.HighTemp) + "℃";
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\WeatherP.ini", Convert.ToString(this.Left));
            File.AppendAllText(Directory.GetCurrentDirectory() + "\\WeatherP.ini", "\n" + Convert.ToString(this.Top));
        }
        private BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
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
