using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.Common;

namespace MetinGo.Server.Entities
{
    public class Item : IItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public ItemType ItemType { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int MaxHP { get; set; }
        public int PerLevelAttack { get; set; }
        public int PerLevelDefence { get; set; }
        public int PerLevelMaxHP { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public Rarity Rarity { get; set; }
    }
}
