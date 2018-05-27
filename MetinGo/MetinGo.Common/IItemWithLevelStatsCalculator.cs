namespace MetinGo.Common
{
    public interface IItemWithLevelStatsCalculator
    {
        ItemWithLevelStats Calculate(IItemWithLevel itemWithLevel);
        ItemWithLevelStats Calculate(IItem item, int level);
    }
}