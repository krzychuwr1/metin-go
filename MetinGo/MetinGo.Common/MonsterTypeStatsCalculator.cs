using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MetinGo.Common
{
    public class MonsterTypeStatsCalculator : IMonsterTypeStatsCalculator
    {
        public MonsterStats GetStats(MonsterType type, int level)
        {
            switch (type)
            {
                case MonsterType.WildDog:
                    return new MonsterStats()
                    {
                        Attack = 10 + (level * 3 / 2),
                        Defence = 4 + (level / 2),
                        MaxHP = 15 + level
                    };
                case MonsterType.HungryWolf:
                    return new MonsterStats()
                    {
                        Attack = 8 + level,
                        Defence = 5 + level,
                        MaxHP = 20 + level
                    };
            }
            throw new ArgumentException("Invalid monster type");
        }
    }
}
