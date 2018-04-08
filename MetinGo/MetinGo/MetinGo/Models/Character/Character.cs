using System;
using System.Collections.Generic;
using System.Text;
using MetinGo.ViewModels;

namespace MetinGo.Models.Character
{
    public class Character
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public int Level { get; set; }

        public int Experience { get; set; }
    }
}
