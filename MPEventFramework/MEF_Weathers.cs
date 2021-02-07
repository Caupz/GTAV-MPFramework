using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPEventFramework
{
    class MEF_Weathers
    {
        public static List<string> weathersWithoutSnow = new List<string>()
        {
            "CLEAR",
            "EXTRASUNNY",
            "CLOUDS",
            "OVERCAST",
            "RAIN",
            "CLEARING",
            "THUNDER",
            "SMOG",
            "FOGGY",
        };
        public static List<string> weathersWithSnow = new List<string>()
        {
            "CLEAR",
            "EXTRASUNNY",
            "CLOUDS",
            "OVERCAST",
            "RAIN",
            "CLEARING",
            "THUNDER",
            "SMOG",
            "FOGGY",
            "XMAS",
            "SNOWLIGHT",
            "BLIZZARD"
        };
        public static List<string> snowWeathers = new List<string>()
        {
            "XMAS",
            "SNOWLIGHT",
            "BLIZZARD"
        };
    }
}
