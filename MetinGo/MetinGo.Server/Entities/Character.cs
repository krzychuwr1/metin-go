using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MetinGo.Server.Entities
{
    public class Character
    {
	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    [Key]
	    public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Experience { get; set; }
        public int StatPoints { get; set; }
    }
}
