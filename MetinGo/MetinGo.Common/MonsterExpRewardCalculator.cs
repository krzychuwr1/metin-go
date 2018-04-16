using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MetinGo.Common
{
    public class MonsterExpRewardCalculator : IMonsterExpRewardCalculator
    {
        public int GetWinExp(MonsterType type, int playerLevel, int monsterLevel)
        {
            switch (type)
            {
                case MonsterType.WildDog:
                    return Math.Max((monsterLevel - playerLevel + 2) * 5, 1);
                case MonsterType.HungryWolf:
                    return Math.Max((monsterLevel - playerLevel + 2) * 7, 1);
            }
            throw new ArgumentException("Invalid monster type");
        }

        public int GetLoseExp(MonsterType type, int playerLevel, int monsterLevel)
        {
            switch (type)
            {
                case MonsterType.WildDog:
                    return Math.Max((playerLevel - monsterLevel) * 2, 1);
                case MonsterType.HungryWolf:
                    return Math.Max((playerLevel - monsterLevel) * 3, 1);
            }
            throw new ArgumentException("Invalid monster type");
        }
    }
}
