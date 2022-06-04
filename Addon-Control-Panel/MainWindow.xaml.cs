using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Addon_Control_Panel
{
    internal class API
    {
        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_FORCEMINIMIZE = 11;
        public const int SW_MAX = 11;
        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);
    }
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
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\addon.ini"))
                File.Create(Directory.GetCurrentDirectory() + "\\addon.ini");
            string[] vs = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\addon.ini");
            foreach (string v in vs)
            {
                if (v == "Clock")
                {
                    this.ClockOn.IsChecked = true;
                    this.ClockOff.IsChecked = false;
                }
                if (v == "Panel")
                {
                    this.PanelOn.IsChecked = true;
                    this.PanelOff.IsChecked = false;
                }
                if (v == "SystemMonitor")
                {
                    this.SysOn.IsChecked = true;
                    this.SysOff.IsChecked = false;
                }
                if (v == "WeatherP")
                {
                    this.WeatherOn.IsChecked = true;
                    this.WeatherOff.IsChecked= false;
                }
            }
        }
        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            List<string> config = new List<string>();
            if (this.ClockOn.IsChecked == true)
                config.Add("Clock");
            if (this.PanelOn.IsChecked == true)
                config.Add("Panel");
            if (this.SysOn.IsChecked == true)
                config.Add("SystemMonitor");
            if (this.WeatherOn.IsChecked == true)
                config.Add("WeatherP");
            string[] v = config.ToArray();
            File.WriteAllLines(Directory.GetCurrentDirectory() + "\\addon.ini", v);
        }
        private void BDo_Click(object sender, RoutedEventArgs e)
        {
            BSave_Click(sender, e);
            API.ShellExecute(IntPtr.Zero, "open", Directory.GetCurrentDirectory() + "\\Addon-Control-Console.exe", null, Directory.GetCurrentDirectory(), API.SW_HIDE);
            Application.Current.Shutdown();
        }
    }
}
