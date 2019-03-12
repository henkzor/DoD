using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace DungeonsOfDoom.Core
{
    public class Skeleton : Monster, IBackpackAble
    {
        public Skeleton(int inputHealth) : base(inputHealth, "Skeleton", 5)
        {
        }

        public override void Attack(IAttackable enemy)
        {
            //if (enemy.Strength >= this.Strength *2)
            //{
            //    enemy.Health -= 2;
            //}
            //else
            //{
                enemy.Health -= this.Strength;
            //}
        }

        public override bool Encounter(IEncounterable enemy)
        {
            bool willThereBeAFight = true;
            if (RandomUtils.OneToHundred() < 50)
            {
                Console.WriteLine("\nYou find a skeleton slumped against the wall.\n " +
                    "However, it does not rise to fight you...\nYou feel sick from the rotten corpse...");
                willThereBeAFight = false;
                Console.ReadKey();
            }

            return willThereBeAFight;
        }

        
    }
}
