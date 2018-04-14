using System;

namespace MetinGo.ApiModel.Item
{
    public class CharacterItem
    {
        public Guid Id { get; set; }
        public Guid CharacterId { get; set; }
        public int Level { get; set; }
        public int ItemId { get; set; }
    }
}
