using System;
using MetinGo.Common;
using Realms;

namespace MetinGo.Models.Item
{
    public class Item : RealmObject, IItem
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int ItemTypeId { get; set; }
        [Ignored]
        public ItemType ItemType => (ItemType) ItemTypeId;
        public int Level { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int MaxHP { get; set; }
        public int PerLevelAttack { get; set; }
        public int PerLevelDefence { get; set; }
        public int PerLevelMaxHP { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RarityId { get; set; }
        [Ignored]
        public Rarity Rarity => (Rarity)RarityId;
    }
}