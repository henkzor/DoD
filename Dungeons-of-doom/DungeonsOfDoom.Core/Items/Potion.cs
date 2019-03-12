using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Core
{
    public class Potion : Item
    {
        public Potion() : base("Potion")
        {
        }

        public override void UseItem(Player character)
        {
            character.Health += 20;
            if (character.Health > 100)
            {
                character.Health = 100;
            }
        }
    }
}
