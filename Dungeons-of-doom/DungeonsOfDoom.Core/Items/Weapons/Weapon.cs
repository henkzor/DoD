using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Core
{
    public abstract class Weapon : Item, IEquipable
    {
        public int StrengthIncrease { get; set; }

        protected Weapon(string inputName, int inputStrength) : base(inputName)
        {
            StrengthIncrease = inputStrength;
        }

        public override void UseItem(Player character)
        {
            character.equipItem(this);
        }
        public void ApplyEffect(Player character, bool PlusOrMinus)
        {
            if (PlusOrMinus)
            {
                character.Strength += StrengthIncrease;
            }
            else
            {
                character.Strength -= StrengthIncrease;
            } 
        }
        
    }
}
