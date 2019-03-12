using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsOfDoom.Core
{
    public interface IAttackable : IEncounterable
    {
        int Health { get; set; }    
            

    }
}
