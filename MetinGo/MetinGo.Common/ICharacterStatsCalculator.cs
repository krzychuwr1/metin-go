using System.Collections.Generic;

namespace MetinGo.Common
{
    public interface ICharacterStatsCalculator
    {
        CharacterStats GetStats(Character character, IEnumerable<IItemWithLevel> items);
    }
}