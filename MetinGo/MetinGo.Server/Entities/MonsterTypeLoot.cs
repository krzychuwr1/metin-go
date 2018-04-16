using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.Common;

namespace MetinGo.Server.Entities
{
    public class MonsterTypeLoot
    {
        public MonsterType MonsterType { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public decimal Probability { get; set; }
    }
}
