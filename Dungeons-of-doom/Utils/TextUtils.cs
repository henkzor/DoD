using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Utils
{
    public static class TextUtils
    {
        public static void AnimateText(string inputText, int delay)
        {
            foreach (char letter in inputText)
            {
                Console.Write(letter);
                Thread.Sleep(delay);
            }
        }
    }
}
