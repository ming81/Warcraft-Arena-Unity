using System;
using UnityEngine;

namespace Core
{
    [Flags]
    public enum UnitCategoryFlags
    {
        All = 1 << 0,
        Player = 1 << 2,
        Creature = 1 << 3,        
        Item = 1 << 4,
        Architecture = 1 << 10,
        Home = 1 << 11,
        ItemBox = 1 << 12,
        ItemCrystal = 1 << 13,
        Door = 1 << 14,

    }
}
