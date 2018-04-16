using System;
using System.Collections.Generic;
using System.Text;
using MetinGo.ApiModel.Item;

namespace MetinGo.ApiModel.Fight
{
    public class FightResponse
    {
        public bool PlayerWon { get; set; }
        public int Experience { get; set; }
        public int LevelAfterFight { get; set; }
        public List<CharacterItem> Loot { get; set; }
    }
}
