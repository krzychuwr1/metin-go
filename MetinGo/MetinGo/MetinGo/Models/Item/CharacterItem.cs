﻿using System;
using MetinGo.Common;
using Realms;

namespace MetinGo.Models.Item
{
    public class CharacterItem : RealmObject, IItemWithLevel
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string CharacterId { get; set; }
        public int Level { get; set; }
        public Item Item { get; set; }
        public bool IsEquipped { get; set; }
        [Ignored]
        IItem IItemWithLevel.Item => Item;
    }
}
