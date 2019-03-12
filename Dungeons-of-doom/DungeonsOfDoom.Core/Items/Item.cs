using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Core
{
    public abstract class Item : IBackpackAble
    {
        protected Item(string inputName)
        {
            Name = inputName;
        }
        public virtual void UseItem(Player character)
        {
        }

        public string Name { get; set; }
    }
}
