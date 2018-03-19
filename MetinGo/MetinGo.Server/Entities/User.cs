using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MetinGo.Server.Entities
{
    public class User
    {
	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    [Key]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string PasswordHash { get; set; }
    }
}
