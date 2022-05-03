using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherP
{
    internal class CWeather
    {
        public enum Weather
        {
            Clear = 0,
            Sunny = 1,
            Cloudy = 2,
            Rainy = 3,
            Thunder = 4,
            Snowy = 5,
            Foggy = 6
        }
        public struct AWeather
        {
            public Weather Weather;
            public int LowTemp;
            public int HighTemp;
        }
        public static AWeather GetWeather()
        {
            if (File.Exists("C:\\IDS\\wapi.html"))
                File.Delete("C:\\IDS\\wapi.html");
            if (File.Exists("C:\\IDS\\lapi.html"))
                File.Delete("C:\\IDS\\lapi.html");
            Syscmd.ExecutePwsh("wget ip.tool.lu -o C:\\IDS\\lapi.html", 0);
            string[] lapi = File.ReadAllLines("C:\\IDS\\lapi.html");
            string[] json = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\citycode.json");
            char[] delimiterChar = { '\"', ':' };
            string ret = "";
            bool isend = false;
            for (int i = 0; i < 1952; i++)
            {
                if (json[i].Contains("\"Province\""))
                {
                    string[] words = json[i].Split(delimiterChar);
                    for (int j = 0; j < words.Length; j++)
                    {
                        if ((lapi[1].Contains(words[j])) && (words[j] != "") && (words[j] != " ") && (words[j] != ","))
                        {
                            for (int k = i + 1; k < 1952; k++)
                            {
                                if (json[k].Contains("\"city\""))
                                {
                                    string[] ct = json[k].Split(delimiterChar);
                                    for (int l = 0; l < ct.Length; l++)
                                    {
                                        if ((lapi[1].Contains(ct[l])) && (ct[l] != "") && (ct[l] != " ") && (ct[l] != ","))
                                        {
                                            isend = true;
                                            string[] words2 = json[k + 1].Split(delimiterChar);
                                            for (int m = 0; m < words2.Length; m++)
                                            {
                                                if (words2[m].Contains("10"))
                                                    ret = words2[m];
                                            }
                                            break;
                                        }
                                    }
                                }
                                if (isend)
                                    break;
                            }
                        }
                        if (isend)
                            break;
                    }
                }
                if (isend)
                    break;
            }
            Console.WriteLine(ret);
            string weauri = "http://www.weather.com.cn/weather1d/" + ret + ".shtml";
            Syscmd.ExecutePwsh("wget " + weauri + " -o C:\\IDS\\wapi.html", 1000);
            string[] awp = File.ReadAllLines("C:\\IDS\\wapi.html");
            int target;
            for (target = 0; target < awp.Length; target++)
            {
                if (awp[target].Contains("hour3data"))
                    break;
            }
            string wapi = awp[target];
            Console.WriteLine(wapi);
            string[] vs = wapi.Split('\"');
            int sn, cl, rn, th, sw, fg;//sunny, cloudy, rainy, thunder, snow and foggy
            sn = cl = rn = th = sw = fg = 2147483647;
            for (int i = 0; i < (vs.Length / 6); i++)
            {
                if (vs[i].Contains("晴") || vs[i].Contains("少云"))
                {
                    sn = i;
                    break;
                }
            }
            for (int i = 0; i < (vs.Length / 6); i++)
            {
                if (vs[i].Contains("阴") || vs[i].Contains("多云"))
                {
                    cl = i;
                    break;
                }
            }
            for (int i = 0; i < (vs.Length / 6); i++)
            {
                if (vs[i].Contains("雷"))
                {
                    th = i;
                    break;
                }
            }
            for (int i = 0; i < (vs.Length / 6); i++)
            {
                if (vs[i].Contains("雨"))
                {
                    rn = i;
                    break;
                }
            }
            for (int i = 0; i < (vs.Length / 6); i++)
            {
                if (vs[i].Contains("雪"))
                {
                    sw = i;
                    break;
                }
            }
            for (int i = 0; i < (vs.Length / 6); i++)
            {
                if (vs[i].Contains("雾"))
                {
                    fg = i;
                    break;
                }
            }
            Weather w = Weather.Clear;
            th >>= 1;
            rn >>= 1;
            sw >>= 1;
            int[] arr = new int[] { sn, cl, th, rn, sw, fg };
            if (arr.Min() == sn)
                w = Weather.Sunny;
            if (arr.Min() == cl)
                w = Weather.Cloudy;
            if (arr.Min() == rn)
                w = Weather.Rainy;
            if (arr.Min() == th)
                w = Weather.Thunder;
            if (arr.Min() == sw)
                w = Weather.Snowy;
            if (arr.Min() == fg)
                w = Weather.Foggy;
            AWeather aw = new AWeather();
            aw.Weather = w;
            aw.HighTemp = -10000;
            aw.LowTemp = 10000;
            for (int i = 0; i < (vs.Length / 6); i++)
            {
                if (vs[i].Contains("℃"))
                {
                    string[] tt = vs[i].Split(',');
                    for (int j = 0; j < tt.Length; j++)
                    {
                        if (tt[j].Contains('℃'))
                        {
                            string[] tp = tt[j].Split('℃');
                            try
                            {
                                if (Convert.ToInt32(tp[0]) > aw.HighTemp)
                                    aw.HighTemp = Convert.ToInt32(tp[0]);
                                if (Convert.ToInt32(tp[0]) < aw.LowTemp)
                                    aw.LowTemp = Convert.ToInt32(tp[0]);
                            }
                            catch { }
                        }
                    }
                }
            }
            return aw;
        }
    }
}
