using System;
using System.Collections.Generic;
using System.Text;
using MetinGo.Common;

namespace MetinGo.ApiModel.Item
{
    public class Item
    {
        public Guid Id { get; set; }
        public ItemType ItemType { get; set; }
        public int Level { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int MaxHP { get; set; }
        public Rarity Rarity { get; set; }
    }
}
