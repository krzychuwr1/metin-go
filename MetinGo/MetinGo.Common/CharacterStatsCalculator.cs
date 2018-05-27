using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetinGo.Common
{
    public class CharacterStatsCalculator : ICharacterStatsCalculator
    {
        private readonly IItemWithLevelStatsCalculator _itemWithLevelStatsCalculator;

        public CharacterStatsCalculator(IItemWithLevelStatsCalculator itemWithLevelStatsCalculator)
        {
            _itemWithLevelStatsCalculator = itemWithLevelStatsCalculator;
        }

        public CharacterStats GetStats(Character character, IEnumerable<IItemWithLevel> items)
        {
             var stats = items.Select(i => _itemWithLevelStatsCalculator.Calculate(i)).ToList();

            var itemsAttack = stats.Sum(s => s.Attack);
            var itemsDefence = stats.Sum(s => s.Defence);
            var itemsMaxHp = stats.Sum(s => s.MaxHp);
            var characterStats = new CharacterStats(
                character.BaseAttack + itemsAttack,
                character.BaseDefence + itemsDefence,
                character.BaseMaxHP + itemsMaxHp
            );
            return characterStats;
        }
    }
}
