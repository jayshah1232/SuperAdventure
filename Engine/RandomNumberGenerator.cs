using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class RandomNumberGenerator
    {
        private static Random generator = new Random();

        public static int numberBetween(int minValue, int maxValue)
        {
            return generator.Next(minValue, maxValue + 1);
        }
    }
}
