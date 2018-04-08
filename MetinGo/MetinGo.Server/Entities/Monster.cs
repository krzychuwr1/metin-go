using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.ApiModel.Monster;

namespace MetinGo.Server.Entities
{
    public class Monster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public int Level { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public MonsterType MonsterType { get; set; }
        public bool IsAlive { get; set; }
    }
}
