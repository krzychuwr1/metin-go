﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MetinGo.ApiModel.Character
{
    public class Character
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
        public int Level { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Experience { get; set; }
        public int StatPoints { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefence { get; set; }
        public int BaseMaxHP { get;set; }
    }
}
