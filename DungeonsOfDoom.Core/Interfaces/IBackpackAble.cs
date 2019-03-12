using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Core
{
    public interface IBackpackAble
    {
        string Name { get; set; }

        void UseItem(Player Character);

    }
}
