using System;
using System.Collections.Generic;
using System.Text;

namespace MetinGo.ApiModel.EquippedItem
{
    public class EquipItemResponse
    {
        public Guid CharacterItemId { get; set; }
        public Guid? UnequippedCharacterItemId { get; set; }
    }
}
