using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace MPEventFramework
{
    class Utils : BaseScript
    {
        public static void Log(string data)
        {
            Debug.WriteLine(data);
        }
    }
}
