using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Core
{
    public abstract class Character : IAttackable, IEncounterable
    {
        public int Health { get; set; }
        public int Strength { get; internal set; }

        protected Character(int inputHealth, int inputStrength)
        {
            Health = inputHealth;
            Strength = inputStrength;
        }

        public virtual void Attack(IAttackable enemy)
        { 
            enemy.Health -= this.Strength;
            if (enemy.Health < 0)
            {
                enemy.Health = 0;
            }
        }
        public virtual bool Encounter(IEncounterable enemy)
        {
            bool willThereBeAFight = true;
            return willThereBeAFight;
        }
    }
}
