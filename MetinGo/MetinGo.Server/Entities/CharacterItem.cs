using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.Common;
using MetinGo.Server.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MetinGo.Server.Entities
{
    public class CharacterItem : IItemWithLevel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public Guid? CharacterId { get; set; }
        public Character Character { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int Level { get; set; }
        public Guid FightId { get; set; }
        public Fight Fight { get; set; } //fight in which the item has been obtained
        public bool IsEquipped { get; set; }
        [NotMapped]
        IItem IItemWithLevel.Item => Item;

        public async Task<(Guid EquippedItem, Guid? UnequippedItem)> Equip(MetinGoDbContext db)
        {
            await db.Entry(this).Reference(c => c.Item).LoadAsync();
            var otherCurrentlyEquippedItemsOfSameType = await db.CharacterItems.Where(i =>
                    i.IsEquipped && i.CharacterId == CharacterId && i.Item != null && i.Item.ItemType == Item.ItemType && i.Id != Id)
                .SingleOrDefaultAsync();
            if (otherCurrentlyEquippedItemsOfSameType != null)
                otherCurrentlyEquippedItemsOfSameType.IsEquipped = false;
            this.IsEquipped = true;
            return (this.Id, otherCurrentlyEquippedItemsOfSameType?.Id);
        }
    }
}
