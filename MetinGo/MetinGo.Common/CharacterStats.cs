using System;
using System.Collections.Generic;
using System.Text;

namespace MetinGo.Common
{
    public class CharacterStats
    {
        public CharacterStats(int attack, int defence, int maxHP)
        {
            Attack = attack;
            Defence = defence;
            MaxHP = maxHP;
        }

        public int Attack { get; }
        public int Defence { get; }
        public int MaxHP { get; }
    }
}
