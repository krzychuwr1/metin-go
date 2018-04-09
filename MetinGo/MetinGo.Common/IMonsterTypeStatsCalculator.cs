namespace MetinGo.Common
{
    public interface IMonsterTypeStatsCalculator
    {
        MonsterStats GetStats(MonsterType type, int level);
    }
}