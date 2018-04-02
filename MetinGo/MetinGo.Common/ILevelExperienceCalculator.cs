namespace MetinGo.Common
{
    public interface ILevelExperienceCalculator
    {
        int GetExpOnLevel(int level);

        int GetFullExpOnLevel(int level);
    }
}