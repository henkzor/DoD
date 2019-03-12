using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Core
{
    public interface IEquipable : IBackpackAble
    {
        void ApplyEffect(Player character, bool PlusOrMinus);
    }
}
