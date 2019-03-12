using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Core
{
    public abstract class Monster : Character, IBackpackAble
    {
        public string Name { get; set; }
        public static int remainingMonsters { get; set; }
        public static int counter { get; set; } 
        
        protected Monster(int inputHealth, string inputName, int inputStrength) : base(inputHealth, inputStrength)
        {
            Name = inputName;
            remainingMonsters++;
        }

        public void UseItem(Player Character)       
        {

        }
        
    }
}
