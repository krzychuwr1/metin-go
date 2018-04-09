using MetinGo.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetinGo.ApiModel.Monster
{
    public class Monster
    {
        public Guid Id { get; set; }

        public int Level { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public MonsterType MonsterType { get; set; }
    }
}
