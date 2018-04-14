using System;
using System.Collections.Generic;
using System.Text;
using MetinGo.Common;
using MetinGo.Models.Item;

namespace MetinGo.ViewModels.Item
{
    public class CharacterItemViewModel
    {
        public ItemWithLevelStats ItemWithLevelStats { get; set; }
        public CharacterItem CharacterItem { get; set; }
    }
}
