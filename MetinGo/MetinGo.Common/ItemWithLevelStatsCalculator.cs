using System;
using System.Collections.Generic;
using System.Text;

namespace MetinGo.Common
{
    public class ItemWithLevelStatsCalculator : IItemWithLevelStatsCalculator
    {
        public ItemWithLevelStats Calculate(IItem item, int level)
        {
            return new ItemWithLevelStats()
            {
                Attack = item.Attack + level * item.PerLevelAttack,
                Defence = item.Defence + level * item.PerLevelDefence,
                MaxHp = item.MaxHP + level * item.PerLevelMaxHP
            };
        }

        public ItemWithLevelStats Calculate(IItemWithLevel itemWithLevel) => Calculate(itemWithLevel.Item, itemWithLevel.Level);
    }
}
