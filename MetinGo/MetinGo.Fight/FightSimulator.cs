using System;
using MetinGo.Common;
using MetinGo.Fight.Model;

namespace MetinGo.Fight
{
    public class FightSimulator : IFightSimulator
    {
        private readonly IMonsterTypeStatsCalculator _monsterTypeStatsCalculator;

        public FightSimulator(IMonsterTypeStatsCalculator monsterTypeStatsCalculator)
        {
            _monsterTypeStatsCalculator = monsterTypeStatsCalculator;
        }

        public FightResult Fight(Character character, Monster monster)
        {
            var monsterStats = _monsterTypeStatsCalculator.GetStats(monster.MonsterType, monster.Level);

            var playerCoefficient =  Math.Max(1, character.BaseAttack - monsterStats.Defence) / (decimal)monsterStats.MaxHP;

            var monsterCoefficient = Math.Max(1, monsterStats.Attack - monsterStats.Defence) / (decimal)character.BaseMaxHP;

            return playerCoefficient >= monsterCoefficient ? new FightResult {PlayerWon = true} : new FightResult {PlayerWon = false};
        }
    }
}
