using System;
using System.Collections.Generic;
using System.Text;

namespace MetinGo.Common
{
    public class MonsterNameResolver
    {
        public string GetMonsterName(MonsterType monsterType)
        {
            switch (monsterType)
            {
                case MonsterType.HungryWolf:
                    return "Hungry Wolf";
                case MonsterType.WildDog:
                    return "Wild Dog";
                default:
                    throw new ArgumentException();
            }
        }
    }
}
