using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public static class RandomUtils
    {
        static Random random = new Random();
        
        public static int OneToHundred()
        {
            return random.Next(1, 101);
        }
        public static int OneToCustomRnd(int inputMax)
        {
            return random.Next(1, inputMax);
        }
    }
}