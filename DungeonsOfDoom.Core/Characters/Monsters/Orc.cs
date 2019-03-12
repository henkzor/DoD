using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Core
{
    public class Orc : Monster, IBackpackAble
    {
        public Orc(int inputHealth) : base(inputHealth, "Orc", 10)
        {

        }
        
    }
}
