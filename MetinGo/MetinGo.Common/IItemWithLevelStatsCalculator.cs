namespace MetinGo.Common
{
    public interface IItemWithLevelStatsCalculator
    {
        ItemWithLevelStats Calculate(IItem item, int level);
    }
}