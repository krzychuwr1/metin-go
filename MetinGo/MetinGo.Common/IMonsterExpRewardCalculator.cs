namespace MetinGo.Common
{
    public interface IMonsterExpRewardCalculator
    {
        int GetLoseExp(MonsterType type, int playerLevel, int monsterLevel);
        int GetWinExp(MonsterType type, int playerLevel, int monsterLevel);
    }
}