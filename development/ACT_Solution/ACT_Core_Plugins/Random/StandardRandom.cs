using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Plugins.Random
{
    public class ACT_StandardRandom : ACT.Core.Interfaces.Random.I_Randomization
    {
        public static System.Random _R = new System.Random();

        public int Next(int minValue, int maxValue)
        {
            return _R.Next(minValue, maxValue);
        }

        public int Next()
        {
            return _R.Next();
        }

        public int Next(int maxValue)
        {
            return _R.Next(maxValue);
        }

        public double NextDouble()
        {
            return _R.NextDouble();
        }
    }
}
