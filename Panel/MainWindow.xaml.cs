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

namespace Panel
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer TimeTimer = new DispatcherTimer();
            TimeTimer.Interval = TimeSpan.FromSeconds(1);
            TimeTimer.Tick += TimeTimer_Tick;
            TimeTimer.Start();
        }
        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string h = dt.Hour < 10 ? "0" + Convert.ToString(dt.Hour) : Convert.ToString(dt.Hour);
            string m = dt.Minute < 10 ? "0" + Convert.ToString(dt.Minute) : Convert.ToString(dt.Minute);
            string[] chnmonth = { "日", "一", "二", "三", "四", "五", "六" };
            this.LTime.Content = h + ":" + m;
            this.LDate.Content = Convert.ToString(dt.Year) + "年" + Convert.ToString(dt.Month) + "月" + Convert.ToString(dt.Day) + "日 星期" + chnmonth[(int)dt.DayOfWeek];
            this.LLunar.Content = "农历" + Lunar.GetString(Lunar.Lang.Chinese_PRC, dt.Year, dt.Month, dt.Day);
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\Panel.ini", Convert.ToString(this.Left));
            File.AppendAllText(Directory.GetCurrentDirectory() + "\\Panel.ini", "\n" + Convert.ToString(this.Top));
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ShowInTaskbar = false;
            if (File.Exists(Directory.GetCurrentDirectory() + "\\Panel.ini"))
            {
                string[] locate = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Panel.ini");
                this.Left = Convert.ToInt32(locate[0]);
                this.Top = Convert.ToInt32(locate[1]);
            }
            DateTime dt = DateTime.Now;
            Lunar.LunarDate ld = Lunar.GetLunarDate();
            Bitmap tile = null;
            if ((dt.Month == 9) && (dt.Day == 2))
            {
                tile = Properties.Resources.Anilovr;
                this.ITile.Source = BitmapToBitmapImage(tile);
            }
            else if ((ld.Month == 5) && (ld.Day == 8))
            {
                tile = Properties.Resources.Elihuso_caffe;
                this.ITile.Source = BitmapToBitmapImage(tile);
            }
            else if ((dt.Month == 6) && (dt.Day == 14))
            {
                tile = Properties.Resources.Hiroko_caffe;
                this.ITile.Source = BitmapToBitmapImage(tile);
            }
            else
                this.ITile.Source = API.GetUserTileImage(null);
            LName.Content = System.Environment.UserName;
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
