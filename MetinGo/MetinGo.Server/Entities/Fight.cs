using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MetinGo.Server.Entities
{
    public class Fight
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid CharacterId { get; set; }
        public Character Character { get; set; }
        public Guid MonsterId { get; set; }
        public Monster Monster { get; set; }
        
        public bool PlayerWon { get; set; }
        public int Experience { get; set; }
    }
}
