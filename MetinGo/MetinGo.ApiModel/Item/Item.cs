﻿using System;
using System.Collections.Generic;
using System.Text;
using MetinGo.Common;

namespace MetinGo.ApiModel.Item
{
    public class Item
    {
        public int Id { get; set; }
        public ItemType ItemType { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int MaxHP { get; set; }
        public int PerLevelAttack { get; set; }
        public int PerLevelDefence { get; set; }
        public int PerLevelMaxHP { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Rarity Rarity { get; set; }
    }
}
