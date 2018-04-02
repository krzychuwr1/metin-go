using System;
using System.Collections.Generic;
using System.Text;

namespace MetinGo.ApiModel.Fight
{
    public class FightResponse
    {
        public bool PlayerWon { get; set; }
        public int Experience { get; set; }
        public int LevelAfterFight { get; set; }
    }
}
