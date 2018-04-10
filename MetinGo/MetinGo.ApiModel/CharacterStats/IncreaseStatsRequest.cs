using System;
using System.Collections.Generic;
using System.Text;

namespace MetinGo.ApiModel.CharacterStats
{
    public class IncreaseStatsRequest
    {
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int HP { get; set; }
    }
}
