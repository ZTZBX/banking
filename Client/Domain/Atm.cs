using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace banking.Client
{
    static public class Atm
    {
        // firt element is the name of the atm
        // elem 0 of list: x
        // elem 1 of list: y
        // elem 2 of list: z 
        public static Dictionary<string, List<float>> atmMetas = new Dictionary<string, List<float>>()
        {
            { "City Bank", new List<float>{237.3873f, 217.7851f, 106.2868f} },
        };

        static public bool atmOp = false;
    }
}