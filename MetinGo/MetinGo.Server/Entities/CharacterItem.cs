using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MetinGo.Server.Entities
{
    public class CharacterItem
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
    }
}
