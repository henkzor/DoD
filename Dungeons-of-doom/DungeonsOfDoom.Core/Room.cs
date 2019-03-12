using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Core
{
    public class Room
    {
        public Monster Monster { get; set; }
        public Item Item { get; set; }
        public Obstacle Obstacle { get; set; }
        
        public void ResetRoom()
        {
            if (Monster != null)
            {
                Monster.remainingMonsters--;
            }
            Item = null;
            Monster = null;
            Obstacle = null;
        }
    }
}
