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
            {"City Bank", new List<float>{237.3873f, 217.7851f, 106.2868f}},
        };

        public static Dictionary<string, List<float>> bankerMetas = new Dictionary<string, List<float>>()
        {
            {"City Bank - Banker 1", new List<float>{248.2292f, 222.4073f, 105.2868f}},
            {"City Bank - Banker 2", new List<float>{253.3571f, 220.5587f, 105.2868f}},
            {"City Bank - Banker 3", new List<float>{243.0168f, 224.2468f, 105.2865f}},
        };


        public static Dictionary<string, List<float>> bankerPeds = new Dictionary<string, List<float>>()
        {
            {"City Bank - Banker 1", new List<float>{254.7044f, 222.3103f, 104.7868f}},
            {"City Bank - Banker 2", new List<float>{249.5318f, 224.1367f, 104.7868f}},
            {"City Bank - Banker 3", new List<float>{244.5397f, 226.0168f, 104.7868f}},
        };

        static public bool pedsAreLoaded = false;


        static public bool playerHaveAccount = false;

        static public bool atmOp = false;
    }
}