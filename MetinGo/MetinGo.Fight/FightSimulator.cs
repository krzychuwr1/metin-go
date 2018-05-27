using System;
using System.Collections.Generic;
using MetinGo.Common;
using MetinGo.Fight.Model;

namespace MetinGo.Fight
{
    public class FightSimulator : IFightSimulator
    {
        private readonly IMonsterTypeStatsCalculator _monsterTypeStatsCalculator;
        private readonly ICharacterStatsCalculator _characterStatsCalculator;

        public FightSimulator(IMonsterTypeStatsCalculator monsterTypeStatsCalculator, ICharacterStatsCalculator characterStatsCalculator)
        {
            _monsterTypeStatsCalculator = monsterTypeStatsCalculator;
            _characterStatsCalculator = characterStatsCalculator;
        }

        public FightResult Fight(Character character, Monster monster, IEnumerable<IItemWithLevel> characterItems)
        {
            var monsterStats = _monsterTypeStatsCalculator.GetStats(monster.MonsterType, monster.Level);

            var characterStats = _characterStatsCalculator.GetStats(character, characterItems);

            var playerCoefficient =  Math.Max(1, characterStats.Attack - monsterStats.Defence) / (decimal)monsterStats.MaxHP;

            var monsterCoefficient = Math.Max(1, monsterStats.Attack - characterStats.Defence) / (decimal)characterStats.Defence;

            return playerCoefficient >= monsterCoefficient ? new FightResult {PlayerWon = true} : new FightResult {PlayerWon = false};
        }
    }
}
