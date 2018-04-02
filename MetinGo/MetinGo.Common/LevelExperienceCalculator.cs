using System;

namespace MetinGo.Common
{
    public class LevelExperienceCalculator : ILevelExperienceCalculator
    {
        public int GetExpOnLevel(int level)
        {
            return level * 10;
        }

        public int GetFullExpOnLevel(int level)
        {
            return (level * 5) * (level + 1);
        }
    }
}
