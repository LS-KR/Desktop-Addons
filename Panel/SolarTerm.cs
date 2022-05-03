using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panel
{
    internal class SolarTerm
    {
        public enum SolarTerms
        {
            Beginning_Of_Spring = 0,
            Rainwater = 1,
            JingZhe = 2,
            Spring_Equinox = 3,
            Spring_Solstice = 3,
            QingMing = 4,
            Guyu = 5,
            Beginning_Of_Summer = 10,
            Xiaoman = 11,
            Awn = 12,
            Summer_Equinox = 13,
            Summer_Solstice = 13,
            Little_Heat = 14,
            Great_Heat = 15,
            Beginning_Of_Autumn = 20,
            Dissipate_Heat = 21,
            Bailu = 22,
            Autumn_Equinox = 23,
            Autumn_Solstice = 23,
            Cold_Dew = 24,
            FrostFall = 25,
            Beginning_Of_Winter = 30,
            Little_Snow = 31,
            Snow_Freeze = 32,
            Winter_Equinox = 33,
            Winter_Solstice = 33,
            Little_Freeze = 34,
            Great_Freeze = 35,
        }
        public struct SolarDate
        {
            public int Month;
            public int Day;
            public override string ToString()
            {
                return Convert.ToString(Month) + "-" + Convert.ToString(Day);
            }
        }
        public static SolarDate GetSolarTermDate(SolarTerms st, int year)
        {
            float cc = 0;
            SolarDate sd = new SolarDate();
            int yy = year - 2000;
            float[,] cctable = { { 3.87f, 18.73f, 5.63f, 20.64f, 4.81f, 20.10f }, { 5.52f, 21.04f, 5.678f, 21.37f, 7.108f, 22.83f }, { 7.50f, 23.13f, 7.646f, 23.042f, 8.318f, 23.438f }, { 7.438f, 22.36f, 7.18f, 21.94f, 5.0455f, 20.12f } };
            int[,] mth = { { 2, 2, 3, 3, 4, 4 }, { 5, 5, 6, 6, 7, 7 }, { 8, 8, 9, 9, 10, 10 }, { 11, 11, 12, 12, 1, 1 } };
            cc = cctable[((int)st) / 10, ((int)st) % 10];
            sd.Day = (int)(yy * 0.2422f + cc) - (int)((yy - 1) / 4);
            sd.Month = mth[((int)st) / 10, ((int)st) % 10];
            return sd;
        }
    }
}
