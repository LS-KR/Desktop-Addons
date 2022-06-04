using System;
using System.Collections.Generic;
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

namespace OnStart
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
            this.Left = (System.Windows.SystemParameters.PrimaryScreenWidth - this.Width) / 2;
            this.Top = (System.Windows.SystemParameters.PrimaryScreenHeight - this.Height) / 2;
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void BConfirm_Click(object sender, RoutedEventArgs e)
        {
            List<string> vs = new List<string>();
            if (this.ChkClock.IsChecked == true)
                vs.Add("Clock");
            if (this.ChkPanel.IsChecked == true)
                vs.Add("Panel");
            if (this.ChkSystm.IsChecked == true)
                vs.Add("SystemMonitor");
            if (this.ChkWeath.IsChecked == true)
                vs.Add("WeatherP");
            var putline = vs.ToArray();
            try
            {
                File.WriteAllLines(Directory.GetCurrentDirectory() + "\\addon.ini", putline);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message);
            }
            Application.Current.MainWindow.Close();
        }
    }
}
